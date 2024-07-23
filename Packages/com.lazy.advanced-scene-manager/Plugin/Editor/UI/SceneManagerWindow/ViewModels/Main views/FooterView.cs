using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.UI.Views.Popups;
using AdvancedSceneManager.Models;
using UnityEditor;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views
{

    class FooterView : ViewModel, IView
    {

        public override void OnAdded()
        {
            SetupPlayButton(view);
            SetupProfile(view);
            SetupCollectionButton(view);
            SetupSceneHelper(view);
        }

        void SetupPlayButton(VisualElement element) =>
            element.Q<Button>("button-play").BindEnabled(SceneManager.settings.user, nameof(SceneManager.settings.user.activeProfile));

        void SetupProfile(VisualElement element)
        {

            var profileButton = element.Q<Button>("button-profile");

            profileButton.clicked += OpenPopup<ProfilePopup>;

            Profile.onProfileChanged += OnProfileChanged;
            profileButton.RegisterCallback<DetachFromPanelEvent>(e => Profile.onProfileChanged -= OnProfileChanged);

            OnProfileChanged();
            void OnProfileChanged()
            {
                profileButton.BindText(Profile.current, nameof(Profile.name), "none");
                UpdateProfileVisibility(element);
            }

        }

        void SetupCollectionButton(VisualElement element)
        {

            var button = element.Q("split-button-add-collection");
            profileBindingService.BindEnabledToProfile(button);

            button.Q<Button>("button-add-collection-menu").clicked += OpenPopup<ExtraCollectionPopup>;
            button.Q<Button>("button-add-collection").clicked += () =>
            {
                Profile.current.CreateCollection();
                collectionView.Reload();
            };

        }

        void SetupSceneHelper(VisualElement element)
        {

            var button = element.Q<Button>("button-scene-helper");

            button.RegisterCallback<PointerDownEvent>(e =>
            {

#if !UNITY_2023_1_OR_NEWER
                e.PreventDefault();
#endif
                e.StopPropagation();
                e.StopImmediatePropagation();

                DragAndDrop.PrepareStartDrag();
                DragAndDrop.objectReferences = new[] { ASMSceneHelper.instance };
                DragAndDrop.StartDrag("Drag: Scene helper");

            }, TrickleDown.TrickleDown);

        }

        public override void ApplyAppearanceSettings(VisualElement element)
        {
            element.Q<Button>("button-scene-helper").SetVisible(SceneManager.settings.user.displaySceneHelperButton);
            UpdateProfileVisibility(element);
        }

        void UpdateProfileVisibility(VisualElement element)
        {
            element.Q("active-profile").SetVisible(SceneManager.settings.user.displayProfileButton || !SceneManager.profile);
        }

    }

}
