using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views.Settings
{

    class AppearancePage : ViewModel, ISettingsPage
    {

        public string Header => "Appearance";

        public override void OnAdded()
        {
            view.BindToUserSettings();
            SetupToolbarButton();
        }

        void SetupToolbarButton()
        {

            var groupInstalled = view.Q("group-toolbar").Q("group-installed");
            var groupNotInstalled = view.Q("group-toolbar").Q("group-not-installed");

#if TOOLBAR_EXTENDER

            groupInstalled.SetVisible(true);
            groupNotInstalled.SetVisible(false);

            Setup(view.Q("slider-toolbar-button-offset"));
            Setup(view.Q("slider-toolbar-button-count"));

            void Setup(VisualElement element)
            {
                element.SetVisible(true);
                element.Q("unity-drag-container").RegisterCallback<PointerMoveEvent>(e =>
                {
                    if (e.pressedButtons == 1)
                        Utility.ToolbarButton.Repaint();
                });
            }

#endif

        }
    }

}
