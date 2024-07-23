using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.UI.Views.ItemTemplates;
using AdvancedSceneManager.Editor.Utility;
using AdvancedSceneManager.Models;
using AdvancedSceneManager.Utility;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views
{

    class CollectionView : ViewModel, IView
    {

        ListView collectionsList;
        ListView dynamicCollectionsList;
        VisualElement standaloneList;

        VisualTreeAsset m_collectionTemplate;
        public VisualTreeAsset collectionTemplate => m_collectionTemplate ? m_collectionTemplate : m_collectionTemplate = viewHandler.FindViewAsset("CollectionItem");

        public override void OnAdded()
        {
            Profile.onProfileChanged += Reload;
            SceneImportUtility.scenesChanged += Reload;
            Reload();
        }

        public override void OnRemoved()
        {
            Profile.onProfileChanged -= Reload;
            SceneImportUtility.scenesChanged -= Reload;
        }

        public void Reload()
        {

            collectionsList = view.Q<ListView>("list-collections") ?? throw new InvalidOperationException("Could not find collections list");
            dynamicCollectionsList = view.Q<ListView>("list-dynamic-collections") ?? throw new InvalidOperationException("Could not find dynamic collections list"); ;
            standaloneList = view.Q("list-standalone") ?? throw new InvalidOperationException("Could not find standalone collections list"); ;

            standaloneList.Clear();
            collectionsList.Clear();
            dynamicCollectionsList.Clear();
            hasCollection = false;
            UpdateNoItemsMessage();

            SetupCollectionList<SceneCollection>(collectionsList);
            SetupCollectionList<DynamicCollection>(dynamicCollectionsList);

            if (Profile.current)
                SetupSingleCollection(Profile.current.standaloneScenes);

            SetupList(collectionsList);
            SetupList(dynamicCollectionsList);

            SetupNoProfileMessage();
            SetupLine();

            EditorApplication.delayCall += () => ApplyAppearanceSettings(view);

        }

        bool hasCollection;
        public void UpdateSeparator()
        {

            var c = SceneManager.assets.collections.LastOrDefault();
            if (c && collectionsList is not null)
                collectionsList.style.marginBottom =
                     hasCollection && SceneManager.settings.user.m_expandedCollections.Contains(c.id)
                    ? 0
                    : 8;

        }

        void SetupList(ListView list)
        {

            //Both lists should use the same scrollview, so lets disable the lists own internal scrollview
            list.Q<ScrollView>().verticalScrollerVisibility = ScrollerVisibility.Hidden;

            //We use padding-bottom to give some space for expanded collections, this prevents that on last item in list
            list.Query<TemplateContainer>().Last()?.AddToClassList("last");

            list.itemIndexChanged -= OnItemIndexChanged;
            list.itemIndexChanged += OnItemIndexChanged;

            void OnItemIndexChanged(int oldIndex, int newIndex) =>
              EditorApplication.delayCall += collectionView.Reload;

        }

        Label noItemsLabel;
        void UpdateNoItemsMessage()
        {
            noItemsLabel = view.Q<Label>("label-no-items");
            noItemsLabel.text = search.isSearching ? "No items found." : "No collections added, you can add one below!";
            noItemsLabel.SetVisible(Profile.current && !hasCollection);
        }

        void SetupNoProfileMessage() => view.Q("label-no-profile").visible = !SceneManager.profile;
        void SetupLine()
        {
            view.Q("line").visible = SceneManager.profile;
            UpdateSeparator();
        }

        void SetupCollectionList<T>(ListView list) where T : ISceneCollection
        {

            list.makeItem = collectionTemplate.Instantiate;

            if (!Profile.current)
            {
                list.Unbind();
                list.Rebuild();
                return;
            }

            if (typeof(T) == typeof(SceneCollection))
            {

                if (search.isSearching && search.savedSearch != null)
                {

                    var items = search.savedSearch.Keys.ToList();

                    list.itemsSource = Array.Empty<SceneCollection>();
                    list.itemsSource = items;
                    list.bindItem = (element, index) =>
                    {
                        if (items.ElementAtOrDefault(index) is SceneCollection c && c.hasID)
                        {
                            hasCollection = true;
                            UpdateSeparator();
                            UpdateNoItemsMessage();
                            OnSetupCollection(element, c);
                        }
                        else
                            element.RemoveFromHierarchy();
                    };

                }
                else
                {

                    list.bindItem = (element, index) =>
                    {
                        if (Profile.current && Profile.current.collections.ElementAtOrDefault(index) is SceneCollection c && c && c.hasID)
                        {
                            hasCollection = true;
                            UpdateSeparator();
                            UpdateNoItemsMessage();
                            OnSetupCollection(element, c);
                        }
                        else
                            element.RemoveFromHierarchy();
                    };

                    var property = Profile.serializedObject.FindProperty("m_collections");
                    list.BindProperty(property);

                }

            }
            else if (typeof(T) == typeof(DynamicCollection))
            {
                var collections = Profile.current ? Profile.current.dynamicCollections.ToArray() : Array.Empty<DynamicCollection>();
                list.bindItem = (element, index) =>
                {
                    if (Profile.current && collections.ElementAtOrDefault(index) is DynamicCollection c && c.hasID)
                        OnSetupCollection(element, collections[index]);
                    else
                        element.RemoveFromHierarchy();
                };
                list.itemsSource = collections;
            }

        }

        void SetupSingleCollection(ISceneCollection collection)
        {

            var element = collectionTemplate.Instantiate();
            standaloneList.Add(element);
            OnSetupCollection(element, collection);

        }

        public readonly Dictionary<ISceneCollection, CollectionItem> views = new();
        void OnSetupCollection(VisualElement element, ISceneCollection collection)
        {

            if (collection is null)
                return;

            element.RegisterCallback<DetachFromPanelEvent>(e =>
            {
                if (views.Remove(collection, out var item))
                    item.OnRemoved();
            });

            var view = views.Set(collection, new CollectionItem(collection));
            view.SetView(element);
            view.OnAdded();

        }

        public override void ApplyAppearanceSettings(VisualElement element)
        {
            foreach (var view in views)
                view.Value.ApplyAppearanceSettings(view.Value.view);
        }

    }

}
