using System;
using System.Linq;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.Utility;
using AdvancedSceneManager.Models;
using AdvancedSceneManager.Models.Enums;
using AdvancedSceneManager.Models.Internal;
using AdvancedSceneManager.Utility;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views.Popups
{

    class CollectionPopup : ViewModel, IPopup
    {

        string savedCollectionID
        {
            get => Get(string.Empty);
            set => Set(value);
        }

        SceneCollection collection;

        public override void PassParameter(object parameter)
        {
            if (parameter is SceneCollection collection && collection)
                this.collection = collection;
            else
                throw new ArgumentException("Bad parameter. Must be SceneCollection.");
        }

        public override void OnReopen() =>
            collection = SceneCollection.Find(savedCollectionID);

        public override void OnRemoved() =>
            savedCollectionID = null;

        public override void OnAdded()
        {

            if (!collection)
            {
                ClosePopup();
                return;
            }

            view.Bind(new(collection));
            savedCollectionID = collection.id;

            SetupTitle();
            SetupSceneLoaderToggles();
            SetupLoadingOptions();
            SetupStartupOptions();
            SetupBinding();
            SetupLock();
            SetupActiveScene();

        }

        #region Title

        void SetupTitle()
        {
            var title = view.Q<TextField>("text-title");
            title.value = collection.title;
            title.RegisterCallback<FocusOutEvent>(e =>
            {
                if (!string.IsNullOrEmpty(title.value))
                    collection.Rename(title.value);
                else
                    title.value = collection.title;
            });
        }

        #endregion
        #region Active scene

        void SetupActiveScene() =>
            view.Q<DropdownField>("dropdown-active-scene").
                SetupSceneDropdown(
                getScenes: () => collection.scenes,
                getValue: () => collection.activeScene,
                setValue: (s) =>
                {
                    collection.activeScene = s;
                    collection.Save();
                });

        #endregion
        #region Lock

        void SetupLock()
        {

            var lockButton = view.Q<Button>("button-lock");
            var unlockButton = view.Q<Button>("button-unlock");
            lockButton.clicked += () => collection.Lock(prompt: true);
            unlockButton.clicked += () => collection.Unlock(prompt: true);

            BindingHelper lockBinding = null;
            BindingHelper unlockBinding = null;

            ReloadButtons();
            view.SetupLockBindings(collection);

            void ReloadButtons()
            {

                lockBinding?.Unbind();
                unlockBinding?.Unbind();
                lockButton.SetVisible(false);
                unlockButton.SetVisible(false);

                if (!SceneManager.settings.project.allowCollectionLocking)
                    return;

                lockBinding = lockButton.BindVisibility(collection, nameof(collection.isLocked), true);
                unlockBinding = unlockButton.BindVisibility(collection, nameof(collection.isLocked));

            }

        }

        #endregion
        #region Scene loader toggles

        void SetupSceneLoaderToggles()
        {

            var list = view.Q("group-scene-loader-toggles");

            Reload();

            void Reload()
            {

                list.Clear();

                foreach (var loader in SceneManager.runtime.GetToggleableSceneLoaders().ToArray())
                {

                    var isCheck = collection.scenes.NonNull().All(s => s.sceneLoader == loader.Key);
                    var isMixedValue = !isCheck && collection.scenes.NonNull().Any(s => s.sceneLoader == loader.Key);

                    var button = new Toggle();
                    button.showMixedValue = isMixedValue;
                    button.label = loader.sceneToggleText;
                    button.SetValueWithoutNotify(isCheck);
                    button.RegisterValueChangedCallback(e =>
                    {
                        foreach (var scene in collection.scenes.NonNull())
                        {
                            if (e.newValue)
                                scene.sceneLoader = loader.Key;
                            else
                                scene.ClearSceneLoader();
                            scene.Save();
                        }
                        Reload();
                    });

                    list.Add(button);

                }

            }

        }

        #endregion
        #region Loading options

        void SetupLoadingOptions()
        {

            var dropdown = view.Q<DropdownField>("dropdown-loading-scene");
            dropdown.
                SetupSceneDropdown(
                getScenes: () => Assets.scenes.Where(s => s.isLoadingScreen),
                getValue: () => collection.loadingScreen,
                setValue: (s) =>
                {
                    collection.loadingScreen = s;
                    collection.Save();
                },
                onRefreshButton: () => LoadingScreenUtility.RefreshSpecialScenes());

            dropdown.SetEnabled(collection.loadingScreenUsage is LoadingScreenUsage.Override);
            _ = view.Q<UnityEngine.UIElements.EnumField>("enum-loading-screen").
                RegisterValueChangedCallback(e =>
                    dropdown.SetEnabled(e.newValue is LoadingScreenUsage.Override));

        }

        #endregion
        #region Startup options

        void SetupStartupOptions()
        {

            var group = view.Q<RadioButtonGroup>("radio-group-startup");
            group.RegisterValueChangedCallback(e => collection.OnPropertyChanged(nameof(collection.startupOption)));
        }

        #endregion
        #region Binding

        void SetupBinding()
        {
            var section = view.Q<TemplateContainer>("SceneBinding");
#if ENABLE_INPUT_SYSTEM && INPUTSYSTEM
            SceneBindingUtility.SetupBindingField(section, collection);
#else
            section.SetEnabled(false);
#endif
        }

        #endregion

    }

}
