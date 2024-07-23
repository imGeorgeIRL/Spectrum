using System.Linq;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.Utility;
using AdvancedSceneManager.Models.Internal;
using AdvancedSceneManager.Utility;
using AdvancedSceneManager.Utility.CrossSceneReferences;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views.Settings
{

    class SceneLoadingPage : ViewModel, ISettingsPage
    {

        public string Header => "Scene loading";

        public override void OnAdded()
        {

            view.Q("section-profile").BindToProfile();
            view.Q("section-project-settings").BindToSettings();
            view.Q<Toggle>("toggle-enable-cross-scene-references").RegisterValueChangedCallback(e => CrossSceneReferenceUtility.Initialize());

            view.Q<DropdownField>("dropdown-loading-scene").
                SetupSceneDropdown(
                getScenes: () => Assets.scenes.Where(s => s.isLoadingScreen),
                getValue: () => SceneManager.settings.profile.loadingScene,
                setValue: (s) =>
                {
                    SceneManager.settings.profile.loadingScene = s;
                    SceneManager.settings.profile.Save();
                },
                onRefreshButton: LoadingScreenUtility.RefreshSpecialScenes);

            view.Q<DropdownField>("dropdown-fade-scene").
                SetupSceneDropdown(
                getScenes: () => Assets.scenes.Where(s => s.isLoadingScreen),
                getValue: () => SceneManager.settings.project.fadeScene,
                setValue: (s) => SceneManager.settings.project.fadeScene = s,
                onRefreshButton: LoadingScreenUtility.RefreshSpecialScenes);

        }

    }

}
