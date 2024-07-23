using System;
using System.ComponentModel;
using System.Linq;
using AdvancedSceneManager.Models;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;



#if INPUTSYSTEM
using AdvancedSceneManager.Utility;
#endif

namespace AdvancedSceneManager.Editor.UI.Views.Popups
{

    class ScenePopup : ViewModel, IPopup
    {

        public string savedSceneID
        {
            get => Get(string.Empty);
            set => Set(value);
        }

        public string savedCollectionID
        {
            get => Get(string.Empty);
            set => Set(value);
        }


        public bool savedIsStandalone
        {
            get => Get(false);
            set => Set(value);
        }

        Scene scene;
        ISceneCollection collection;

        public override void PassParameter(object parameter)
        {
            if (parameter is (Scene scene, ISceneCollection collection) && scene && collection != null)
            {

                this.scene = scene;
                this.collection = collection;

                savedSceneID = scene.id;
                savedIsStandalone = collection == Profile.current.standaloneScenes;
                savedCollectionID = savedIsStandalone ? null : collection.id;

            }
            else
                throw new ArgumentException("Bad parameter. Must be either: (Scene, ISceneCollection).");
        }

        public override void OnReopen()
        {
            scene = Scene.Find(savedSceneID);
            collection = savedIsStandalone ? Profile.current.standaloneScenes : SceneCollection.Find(savedCollectionID);
        }

        public override void OnAdded()
        {

            if (!scene || collection == null)
            {
                ClosePopup();
                return;
            }

            SetupCollectionOptions();
            SetupStandaloneOptions();
            SetupSceneLoaderToggles();
            SetupSceneOptions();
            SetupEditorOptions();

        }

        public override void OnRemoved()
        {

            savedSceneID = null;
            savedCollectionID = null;
            savedIsStandalone = false;

#if ENABLE_INPUT_SYSTEM && INPUTSYSTEM
            SceneBindingUtility.CancelListenForInput();
#endif

            if (collection is SceneCollection c)
                c.Save();
            else if (collection is StandaloneCollection)
                Profile.current.Save();

            if (scene)
                scene.Save();

        }

        #region SceneCollection

        void SetupCollectionOptions()
        {

            if (collection is SceneCollection c)
            {

                view.Q("group-collection").SetVisible(true);
                var toggle = view.Q<Toggle>("toggle-dontOpen");
                toggle.SetValueWithoutNotify(c.AutomaticallyOpenScene(scene));
                toggle.RegisterValueChangedCallback(e => c.AutomaticallyOpenScene(scene, e.newValue));

                view.Q<Label>("text-collection-title").text = c.title;

            }

        }

        #endregion
        #region Standalone

        void SetupStandaloneOptions()
        {

            if (collection is StandaloneCollection c)
            {

                view.Q("group-standalone").Bind(new(scene));
                view.Q("group-standalone").SetVisible(true);
                SetupBinding(c);
            }

        }

        void SetupBinding(StandaloneCollection c)
        {
            var section = view.Q<TemplateContainer>("SceneBinding");
            section.SetVisible(true);

#if ENABLE_INPUT_SYSTEM && INPUTSYSTEM
            SceneBindingUtility.SetupBindingField(section, scene);
#else
            section.SetEnabled(false);
#endif
        }

        #endregion
        #region Scene

        void SetupSceneOptions()
        {

            view.Q("group-scene").Bind(new SerializedObject(scene));
            SetupSceneLoaderToggles();
            SetupHalfPersistent();

            view.Q<Label>("text-scene-name").text = scene.name;

        }

        void SetupSceneLoaderToggles()
        {

            var list = view.Q("group-scene-loader-toggles");
            scene.PropertyChanged += OnPropertyChanged;
            view.RegisterCallback<DetachFromPanelEvent>(e => scene.PropertyChanged -= OnPropertyChanged);

            void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(scene.sceneLoader))
                    Reload();
            }

            Reload();

            void Reload()
            {

                list.Clear();

                foreach (var loader in SceneManager.runtime.GetToggleableSceneLoaders().ToArray())
                {

                    var button = new Toggle();
                    button.label = loader.sceneToggleText;
                    button.SetValueWithoutNotify(scene.sceneLoader == loader.Key);
                    button.RegisterValueChangedCallback(e =>
                    {
                        if (e.newValue)
                            scene.sceneLoader = loader.Key;
                        else
                            scene.ClearSceneLoader();
                        scene.Save();
                    });

                    list.Add(button);

                }

            }

        }

        void SetupHalfPersistent()
        {

            SetupToggle(view.Q<RadioButton>("toggle-remain-open"), false);
            SetupToggle(view.Q<RadioButton>("toggle-re-open"), true);

            void SetupToggle(RadioButton toggle, bool invert)
            {
                toggle.SetValueWithoutNotify(invert ? !scene.keepOpenWhenNewCollectionWouldReopen : scene.keepOpenWhenNewCollectionWouldReopen);
                toggle.RegisterValueChangedCallback(e => scene.keepOpenWhenNewCollectionWouldReopen = invert ? !e.newValue : e.newValue);
            }

        }

        #endregion
        #region Editor

        void SetupEditorOptions()
        {

#if UNITY_2022_1_OR_NEWER
            view.Q("group-editor").Bind(new(scene));

            var list = view.Q<ListView>("list-auto-open-scenes");
            var enumField = view.Q<UnityEngine.UIElements.EnumField>("enum-auto-open-in-editor");
            list.makeItem = () => new SceneField();

            list.bindItem = (element, i) =>
            {

                var field = (SceneField)element;

                if (scene.autoOpenInEditorScenes.ElementAtOrDefault(i) is Scene s && s)
                    field.SetValueWithoutNotify(s);
                else
                    field.SetValueWithoutNotify(null);

                field.RegisterValueChangedCallback(e => scene.autoOpenInEditorScenes[i] = e.newValue);

            };

            enumField.RegisterValueChangedCallback(e => UpdateListVisible());

            UpdateListVisible();
            void UpdateListVisible() =>
                list.SetVisible(scene.autoOpenInEditor == Models.Enums.EditorPersistentOption.WhenAnyOfTheFollowingScenesAreOpened);

#endif

        }

        #endregion

    }

}

