using System;
using AdvancedSceneManager.DependencyInjection;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.UI.Views.Popups;
using UnityEditor;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views
{

    class HeaderView : ViewModel, IView
    {

        public override void OnAdded()
        {

            view.Q<Button>("button-overview").clicked += OpenPopup<OverviewPopup>;
            view.Q<Button>("button-menu").clicked += OpenPopup<MenuPopup>;

            view.Q<Button>("button-dev-build").clicked += DevBuild;
            view.Q<Button>("button-build-profiles").clicked += BuildProfiles;
            view.Q<Button>("button-project-settings").clicked += ProjectSettings;
            view.Q<Button>("button-code").clicked += OpenEditor;

            SetupPlayButton(view);
            SetupSettingsButton(view);

        }

        void DevBuild() =>
            DependencyInjectionUtility.GetService<MenuPopup>().DoDevBuild();

        void BuildProfiles()
        {
            try
            {
                //Ugly... but no other option currently
                Type.GetType("UnityEditor.Build.Profile.BuildProfileWindow, UnityEditor.BuildProfileModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\r\n").
                    GetMethod("ShowBuildProfileWindow").
                    Invoke(null, Array.Empty<object>());
            }
            catch
            {
                BuildPlayerWindow.ShowBuildPlayerWindow();
            }
        }

        void ProjectSettings() =>
            SettingsService.OpenProjectSettings();

        void OpenEditor() =>
            EditorApplication.ExecuteMenuItem("Assets/Open C# Project");

        void SetupPlayButton(VisualElement element)
        {

            var button = element.Q<Button>("button-play");
            button.clickable.activators.Add(new() { button = MouseButton.LeftMouse, modifiers = UnityEngine.EventModifiers.Shift });
            element.Q<Button>("button-play").clickable.clickedWithEventInfo += (e) =>
                SceneManager.app.Restart(new() { forceOpenAllScenesOnCollection = e.IsShiftKeyHeld() || e.IsCommandKeyHeld() });

            profileBindingService.BindEnabledToProfile(button);

        }

        void SetupSettingsButton(VisualElement element)
        {

            var button = element.Q<Button>("button-settings");
            button.clicked += settingsView.Open;
            profileBindingService.BindEnabledToProfile(button);

        }

        public override void ApplyAppearanceSettings()
        {
            view.Q<Button>("button-search").SetVisible(SceneManager.settings.user.displaySearchButton);
            view.Q<Button>("button-overview").SetVisible(SceneManager.settings.user.displayOverviewButton);
            view.Q<Button>("button-dev-build").SetVisible(SceneManager.settings.user.displayDevBuildButton);
            view.Q<Button>("button-build-profiles").SetVisible(SceneManager.settings.user.displayBuildProfilesButton);
            view.Q<Button>("button-project-settings").SetVisible(SceneManager.settings.user.displayProjectSettingsButton);
            view.Q<Button>("button-code").SetVisible(SceneManager.settings.user.displayCodeEditorButton);
        }

    }

}
