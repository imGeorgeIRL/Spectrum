using System;
using System.ComponentModel;
using System.Linq;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.UI.Views.Popups;
using AdvancedSceneManager.Editor.Utility;
using AdvancedSceneManager.Models;
using AdvancedSceneManager.Utility;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views.ItemTemplates
{

    class CollectionItem : ViewModel
    {

        VisualTreeAsset m_sceneTemplate;
        public VisualTreeAsset sceneTemplate => m_sceneTemplate ? m_sceneTemplate : m_sceneTemplate = viewHandler.FindViewAsset("SceneItem");

        public ISceneCollection collection { get; private set; }

        bool isSearchMode => (collection is SceneCollection && search.isSearching);
        bool shouldOpenInSearchMode => isSearchMode && search.lastSearchScenes;

        public CollectionItem(ISceneCollection collection) =>
            this.collection = collection;

        public override void OnAdded()
        {

            if (collection is SceneCollection c)
                view.Bind(new(c));

            SetupHeader();
            SetupContent();

        }

        public override void OnRemoved()
        {

            if (views is not null)
                foreach (var view in views)
                    view.OnRemoved();

            views = Array.Empty<SceneItem>();

        }

        bool isExpanded
        {
            get => SceneManager.settings.user.m_expandedCollections.Contains(collection.id);
            set
            {

                if (value == SceneManager.settings.user.m_expandedCollections.Contains(collection.id))
                    return;

                SceneManager.settings.user.m_expandedCollections.Remove(collection.id);
                if (value == true)
                    SceneManager.settings.user.m_expandedCollections.Add(collection.id);

                SceneManager.settings.user.Save();

            }
        }

        #region Header

        #region Button callbacks

        void Play(bool openAll)
        {
            if (collection is SceneCollection c)
                SceneManager.app.Restart(new() { openCollection = c, forceOpenAllScenesOnCollection = openAll });
        }

        void Open(bool openAll)
        {
            if (collection is SceneCollection c)
                SceneManager.runtime.Open(c, openAll).CloseAll();
        }

        void OpenAdditive(bool openAll)
        {

            if (collection is not SceneCollection c)
                return;

            if (c.isOpen)
                SceneManager.runtime.Close(c);
            else
                SceneManager.runtime.OpenAdditive(c, openAll);

        }

        #endregion

        void SetupHeader()
        {

            view.Q<Label>(name: "label-title").BindText(collection, nameof(collection.title));

            SetupContextMenu();

            SetupOpenButtons();

            SetupExpander();
            SetupCollectionDrag();
            SetupSceneHeaderDrop();
            SetupStartupIndicator();

            SetupMenu();
            SetupRemove();
            SetupAdd();

            ApplyAppearanceSettings(view);
            SetupLocking();

        }

        void SetupLocking()
        {

            if (collection is not SceneCollection c)
                return;

            view.SetupLockBindings(c);

            var menuButton = view.Q<Button>("button-menu");
            var lockButton = view.Q<Button>("button-collection-header-unlock");
            lockButton.clicked += () => c.Unlock(prompt: true);

            BindingHelper lockBinding = null;
            BindingHelper menuBinding = null;

            ReloadButtons();

            void ReloadButtons()
            {

                lockBinding?.Unbind();
                menuButton?.Unbind();
                lockButton.SetVisible(false);
                menuButton.SetVisible(true);

                if (!SceneManager.settings.project.allowCollectionLocking)
                    return;

                lockBinding = lockButton.BindVisibility(c, nameof(c.isLocked), false);
                menuBinding = menuButton.BindVisibility(c, nameof(c.isLocked), true);

            }

        }

        void SetupContextMenu()
        {

            if (collection is not SceneCollection c)
                return;

            view.Q("button-header").ContextMenu(e =>
            {

                e.StopPropagation();

                var collections = selection.collections.Concat(c).ToArray();
                GenerateCollectionHeader(collections);

                var isSingleVisibility = collections.Length == 1 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;

                e.menu.AppendAction("View in project view...", e => ContextualActions.ViewInProjectView(collection is SceneCollection c ? c : Profile.current), isSingleVisibility);
                e.menu.AppendAction("Create template...", e => ContextualActions.CreateTemplate((SceneCollection)collection), isSingleVisibility);

                e.menu.AppendSeparator(); e.menu.AppendSeparator();
                e.menu.AppendAction("Remove...", e => ContextualActions.Remove(collections));

                void GenerateCollectionHeader(params SceneCollection[] collections)
                {
                    foreach (var c in collections)
                        e.menu.AppendAction(c.title, e => { }, DropdownMenuAction.Status.Disabled);
                    e.menu.AppendSeparator();
                }

            });

        }

        public override void ApplyAppearanceSettings(VisualElement element)
        {

            element?.Q("toggle-collection-include")?.SetVisible(collection is SceneCollection && SceneManager.settings.project.allowExcludingCollectionsFromBuild);
            element?.Q("button-collection-play")?.SetVisible(collection is SceneCollection && SceneManager.settings.user.displayCollectionPlayButton);
            element?.Q("button-collection-open")?.SetVisible(collection is SceneCollection && SceneManager.settings.user.displayCollectionOpenButton);
            element?.Q("button-collection-open-additive")?.SetVisible(collection is SceneCollection && SceneManager.settings.user.displayCollectionAdditiveButton);

            element?.Q("label-reorder-collection")?.SetVisible(collection is SceneCollection && !search.isSearching);
            element?.Q("button-add-scene")?.SetVisible(collection is ISceneCollection.IEditable);
            element?.Q("button-remove")?.SetVisible(collection is SceneCollection or DynamicCollection);
            element?.Q("button-menu")?.SetVisible(collection is SceneCollection or DynamicCollection);

            if (views?.Any() ?? false)
                foreach (var view in views)
                    view.ApplyAppearanceSettings(view.view);

        }

        #region Left

        void SetupOpenButtons()
        {

            if (collection is not SceneCollection c || !c)
                return;

            var additiveButton = view.Q<Button>("button-collection-open-additive");

            Setup(view.Q<Button>("button-collection-play"), Play);
            Setup(view.Q<Button>("button-collection-open"), Open);
            Setup(additiveButton, OpenAdditive);

            UpdateAdditiveButton();
            SceneManager.runtime.startedWorking += UpdateAdditiveButton;
            SceneManager.runtime.stoppedWorking += UpdateAdditiveButton;

            additiveButton.RegisterCallbackOnce<DetachFromPanelEvent>(e =>
            {
                SceneManager.runtime.startedWorking -= UpdateAdditiveButton;
                SceneManager.runtime.stoppedWorking -= UpdateAdditiveButton;
            });

            void Setup(Button button, Action<bool> action)
            {

                button.clickable = new(() => { });
                button.clickable.activators.Add(new() { modifiers = EventModifiers.Shift });
                button.clickable.clickedWithEventInfo += (e) => action.Invoke(e.IsShiftKeyHeld());

#if !COROUTINES
                button.SetEnabled(false);
                button.tooltip = "Editor coroutines is needed to use this feature.";
#endif

            }

            void UpdateAdditiveButton() =>
                additiveButton.text = c.isOpen ? "" : "";

        }

        #endregion
        #region Middle

        void SetupExpander()
        {

            var header = view.Q("collection-header");
            var expander = view.Q<Button>("button-header");
            var list = view.GetAncestor<ListView>();
            bool? expandedOverride = null;

            UpdateExpanded();
            UpdateSelection();

            expander.clickable = null;
            expander.clickable = new(() => { });
            expander.clickable.activators.Add(new() { modifiers = EventModifiers.Control });
            expander.clickable.clickedWithEventInfo += (_e) =>
            {

                if (_e.IsCtrlKeyHeld() || _e.IsCommandKeyHeld())
                {

                    if (collection is not SceneCollection c)
                        return;

                    selection.ToggleSelection(this);

                    var i = Profile.current.IndexOf(c);
                    if (list.selectedIndices.Contains(i))
                        list.RemoveFromSelection(i);
                    else
                        list.AddToSelection(i);

                    UpdateSelection();

                }
                else
                    ToggleExpanded(collection);

            };

            void UpdateSelection() =>
                header.EnableInClassList("selected", selection.IsSelected(this));

            void UpdateExpanded()
            {

                var isExpanded = shouldOpenInSearchMode || this.isExpanded;
                if (isSearchMode && expandedOverride.HasValue)
                    isExpanded = expandedOverride.Value;

                view.Q("collection").EnableInClassList("expanded", isExpanded);
                view.Q<Label>("label-expanded-status").text = isExpanded ? "" : "";
                view.Q<Label>("label-expanded-status").style.marginTop = isExpanded ? 0 : 1;

                collectionView.UpdateSeparator();
                SetupScenes();

            }

            void ToggleExpanded(ISceneCollection collection)
            {

                if (isSearchMode)
                    expandedOverride = !(expandedOverride ?? false);
                else
                    isExpanded = !isExpanded;

                UpdateExpanded();

            }

        }

        void SetupCollectionDrag()
        {

            if (this.collection is not SceneCollection collection)
                return;

            var header = view.Q("button-header");

            bool isDown = false;

            header.RegisterCallback<PointerDownEvent>(e => isDown = true, TrickleDown.TrickleDown);

            header.RegisterCallback<PointerLeaveEvent>(e =>
            {
                var isDragging = DragAndDrop.objectReferences.Length == 1 && DragAndDrop.objectReferences[0] == collection;
                if (isDown && !isDragging && e.pressedButtons == 1)
                {
                    DragAndDrop.PrepareStartDrag();
                    DragAndDrop.objectReferences = new[] { collection };
                    DragAndDrop.StartDrag("Collection drag: " + collection.name);
                    collectionView.Reload();
                }
                isDown = false;

            });

        }

        void SetupSceneHeaderDrop()
        {

            if (this.collection is not ISceneCollection.IEditable collection)
                return;

            var header = view.Q("button-header");

            header.RegisterCallback<DragUpdatedEvent>(e =>
            {
                e.StopPropagation();
                var scenes = SceneField.GetDragDropScenes().ToArray();
                DragAndDrop.visualMode = scenes.Length > 0 ? DragAndDropVisualMode.Link : DragAndDropVisualMode.Rejected;
            });

            header.RegisterCallback<DragPerformEvent>(e =>
            {

                var scenes = SceneField.GetDragDropScenes();
                if (scenes.Any())
                    collection.Add(scenes.ToArray());

            });

        }

        void SetupStartupIndicator()
        {
            if (collection is SceneCollection c)
                view.Q("label-startup").BindVisibility(c, nameof(c.isStartupCollection));
            else
                view.Q("label-startup").SetVisible(false);
        }

        #endregion
        #region Right

        void SetupMenu()
        {
            view.Q<Button>("button-menu").clicked += () =>
            {

                if (collection is SceneCollection sc)
                    OpenPopup<CollectionPopup>(sc);

                else if (collection is DynamicCollection dc)
                    OpenPopup<DynamicCollectionPopup>(dc);

            };
        }

        void SetupRemove() =>
            view.Q<Button>("button-remove").clicked += () => ContextualActions.Remove(collection);

        void SetupAdd()
        {

            view.Q<Button>("button-add-scene").clickable = new Clickable(() =>
            {

                (collection as ISceneCollection.IEditable)?.AddEmptyScene();
                isExpanded = true;
                collectionView.Reload();

            });

        }

        #endregion

        #endregion
        #region Content

        void SetupContent()
        {
            SetupSceneReorder();
            SetupDescription();
            SetupScenes();
            SetupNoScenesLabel();
            SetupSceneDropZone();
        }

        void SetupSceneReorder()
        {

            var list = view.Q<ListView>("list");
            int pointerID = -1;

            //Nested listviews are not supported out of the box, this fixes that by just preventing events from reaching parent listview
            list.RegisterCallback<PointerDownEvent>(e =>
            {
                e.StopPropagation();
                list.CapturePointer(e.pointerId);
                pointerID = e.pointerId;
                list.Clear();
            });

            list.RegisterCallback<PointerMoveEvent>(e =>
            {
                if (e.pressedButtons == 0)
                    list.ReleasePointer(e.pointerId);
            });

            list.itemIndexChanged += (oldIndex, newIndex) =>
            {
                list.ReleasePointer(pointerID);
                if (collection is ISceneCollection.IEditable c)
                    EditorApplication.delayCall += () => c.Move(oldIndex, newIndex);
            };

        }

        void SetupDescription()
        {
            view.Q<Label>("label-description").text = collection.description;
            view.Q<BindableElement>("label-description").BindVisibility(collection, propertyPath: nameof(collection.description));
        }

        public SceneItem[] views;

        void SetupScenes()
        {

            var list = view.Q<ListView>("list");

            if (collection is DynamicCollection)
                list.AddToClassList("dynamic");

            list.makeItem = sceneTemplate.Instantiate;

            list.bindItem = (element, index) =>
            {

                if (views is null || views.ElementAtOrDefault(index) is not SceneItem view)
                    return;

                view.SetView(element);
                view.OnAdded();
                view.ApplyAppearanceSettings(element);

            };

            collection.PropertyChanged -= OnPropertyChanged;
            collection.PropertyChanged += OnPropertyChanged;

            void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName is nameof(collection.scenes))
                    Reload();
            }

            Reload();
            void Reload()
            {

                if (collection is DynamicCollection d)
                    views = collection.scenePaths?.Select((path, i) => new SceneItem(collection, path, i))?.ToArray() ?? Array.Empty<SceneItem>();
                else
                    views = collection.scenes?.Where(IsVisible)?.Select((scene, i) => new SceneItem(collection, scene, i))?.ToArray() ?? Array.Empty<SceneItem>();

                list.itemsSource = views;
                list.SetVisible(views.Any());
                list.Rebuild();
                SetupNoScenesLabel();

            }

            bool IsVisible(Scene scene)
            {

                if (!isSearchMode)
                    return true;

                return search.IsInActiveSearch(collection as SceneCollection, scene);

            }

        }

        void SetupNoScenesLabel()
        {

            var list = view.Q<ListView>("list");
            var labelNoScenes = view.Q<Label>("label-no-scenes");

            labelNoScenes.SetVisible(list.itemsSource is null || list.itemsSource.Count == 0);

            if (collection is SceneCollection or StandaloneCollection)
                labelNoScenes.text = "No scenes here, you can add some using the plus button above.";
            else if (collection is DynamicCollection)
                labelNoScenes.text =
                    "Dynamic collections guarantee that all scenes within a certain folder will be included in build.\n\n" +
                    "No scenes were found in target folder.";

        }

        void SetupSceneDropZone()
        {

            if (this.collection is not ISceneCollection.IEditable collection)
                return;

            var zone = view.Q("scene-drop-zone");

            zone.RegisterCallback<DragUpdatedEvent>(e =>
            {
                e.StopPropagation();
                e.StopImmediatePropagation();
                DragAndDrop.visualMode = DragAndDropVisualMode.Link;
            });

            zone.RegisterCallback<DragPerformEvent>(e =>
            {
                var scenes = SceneField.GetDragDropScenes();
                collection.Add(SceneField.GetDragDropScenes().ToArray());
                Hide();
            });

            rootVisualElement.RegisterCallback<DragUpdatedEvent>(e => zone.SetVisible(IsSceneDrag()), TrickleDown.TrickleDown);
            rootVisualElement.RegisterCallback<DragPerformEvent>(e => Hide(), TrickleDown.TrickleDown);
            rootVisualElement.RegisterCallback<DragExitedEvent>(e => Hide(), TrickleDown.TrickleDown);

            void Hide() =>
                zone.SetVisible(false);

            bool IsSceneDrag()
            {

                if (DragAndDrop.objectReferences.Length == 0)
                    return false;

                var scenes = SceneField.GetDragDropScenes();
                return scenes.Any();

            }

        }

        #endregion

    }

}
