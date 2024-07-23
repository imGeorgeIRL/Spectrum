using System;
using System.Linq;
using AdvancedSceneManager.DependencyInjection;
using AdvancedSceneManager.DependencyInjection.Editor;
using AdvancedSceneManager.Editor.UI.Layouts;
using AdvancedSceneManager.Editor.UI.Notifications;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.UI.Views;
using AdvancedSceneManager.Editor.UI.Views.Popups;
using AdvancedSceneManager.Editor.UI.Views.Settings;
using AdvancedSceneManager.Models.Internal;
using AdvancedSceneManager.Utility;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI
{

    [InitializeInEditor]
    /// <summary>The scene manager window provides the front-end for Advanced Scene Manager.</summary>
    public class SceneManagerWindow : EditorWindow
    {

        //Font awesome assets keep getting warnings that the assets aren't in use, even if they are.
        //This makes the warnings go away
        public FontAsset fontAsset1;
        public FontAsset fontAsset2;
        public FontAsset fontAsset3;

        internal static new VisualElement rootVisualElement { get; private set; }
        internal static SceneManagerWindow window { get; private set; }

        public static event Action onOpen;
        public static event Action onClose;
        public static event Action onFocus;

        internal static ViewHandler viewHandler { get; } = new(
            mainLayout: new MainView(),
            popupLayout: new PopupView(),
            settingsLayout: new SettingsView());

        void CreateGUI()
        {

            titleContent = new GUIContent("Scene Manager");

            minSize = new(466, 230);

            window = this;
            rootVisualElement = base.rootVisualElement;

            SetupDependencyInjection();
            ((MainView)viewHandler.mainLayout).OnCreateGUI();

            ApplyAppearanceSettings();

            onOpen?.Invoke();

            SceneManager.settings.user.PropertyChanged += (s, e) => ApplyAppearanceSettings();

        }

        private void OnDestroy() =>
            ((MainView)viewHandler.mainLayout).OnWindowClose();

        /// <summary>Closes the window.</summary>
        public static new void Close()
        {
            if (window)
                ((EditorWindow)window).Close();
        }

        [MenuItem("File/Scene Manager %#m", priority = 205)]
        [MenuItem("Window/Advanced Scene Manager/Scene Manager", priority = 3030)]
        static void Open() => GetWindow<SceneManagerWindow>();

        public bool wantsConstantRepaint { get; set; }

        public static new void Focus()
        {
            if (window)
                ((EditorWindow)window).Focus();
        }

        #region ISceneManagerWindow

        sealed class SceneManagerWindowService : ISceneManagerWindow
        {

            private readonly CollectionView collectionView;

            public SceneManagerWindowService(CollectionView collectionView) =>
                this.collectionView = collectionView;

            public void OpenWindow() =>
                Open();

            public void CloseWindow() =>
                Close();

            public void Reload()
            {
                if (window)
                {
                    collectionView.Reload();
                    DependencyInjectionUtility.GetService<NotificationView>().ReloadPersistentNotifications();
                }
            }
        }

        #endregion
        #region Dependency injection

        public const int popupFadeAnimationDuration = 250;

        static bool hasInjected;
        static void SetupDependencyInjection()
        {

            if (hasInjected)
                return;
            hasInjected = true;

            viewHandler.CacheViewAssets();

            viewHandler.AddView<CollectionView>();
            viewHandler.AddView<ProgressSpinnerView>();
            viewHandler.AddView<NotificationView>();
            viewHandler.AddView<SelectionView>();

            //Utility
            viewHandler.AddPopup<PickNamePopup>();

            //Notifications
            viewHandler.AddNotification<WelcomeNotification>();
            viewHandler.AddNotification<EditorCoroutinesNotification>();
            viewHandler.AddNotification<LegacyNotification, LegacyPopup>();

            //Scene import notification and popups
            viewHandler.FindViewAsset("ImportScenePopup", out var importScenePopup, throwIfMissing: true);
            viewHandler.AddNotification<BadScenePathNotification, BadPathScenePopup>(importScenePopup);
            viewHandler.AddNotification<DuplicateScenesNotification, DuplicateScenePopup>(importScenePopup);
            viewHandler.AddNotification<ImportedBlacklistedSceneNotification, ImportedBlacklistedScenePopup>(importScenePopup);
            viewHandler.AddNotification<ImportSceneNotification, ImportScenePopup>(importScenePopup);
            viewHandler.AddNotification<InvalidSceneNotification, InvalidScenePopup>(importScenePopup);
            viewHandler.AddNotification<UntrackedSceneNotification, UntrackedScenePopup>(importScenePopup);

            //Popups
            viewHandler.AddPopup<CollectionPopup>();
            viewHandler.AddPopup<DynamicCollectionPopup>();
            viewHandler.AddPopup<MenuPopup>();
            viewHandler.AddPopup<OverviewPopup>();
            viewHandler.AddPopup<ScenePopup>();

            //List popup
            viewHandler.FindViewAsset("ListPopup", out var listPopup, throwIfMissing: true);
            viewHandler.AddPopup<ExtraCollectionPopup>(listPopup);
            viewHandler.AddPopup<ProfilePopup>(listPopup);

            //Settings
            viewHandler.AddSettings<AppearancePage>();
            viewHandler.AddSettings<AssetsPage>();
            viewHandler.AddSettings<EditorPage>();
            viewHandler.AddSettings<NetworkPage>();
            viewHandler.AddSettings<SceneLoadingPage>();
            viewHandler.AddSettings<StartupPage>();

            //Views
            viewHandler.AddView<HeaderView>();
            viewHandler.AddView<FooterView>();
            viewHandler.AddView<UndoView>();
            viewHandler.AddView<SearchView>();
            viewHandler.AddView<DevMenuView>(requireView: false);
            viewHandler.AddView<ProfileBindingsService>(requireView: false);

            viewHandler.AddView<PopupView>();
            viewHandler.AddPopup<SettingsView>();

            viewHandler.UncacheViewAssets();

            DependencyInjectionUtility.Add<ISceneManagerWindow, SceneManagerWindowService>();

        }

        #endregion
        #region Callbacks

        void OnEnable()
        {
            foreach (var view in viewHandler.views)
                view.Key.OnWindowEnable();
        }

        void OnDisable()
        {
            foreach (var view in viewHandler.views)
                view.Key.OnWindowDisable();
            onClose?.Invoke();
        }

        void OnFocus()
        {

            if (!SceneManager.isInitialized)
                return;

            if (Assets.Cleanup())
                Assets.Save();

            foreach (var view in viewHandler.views)
                view.Key.OnWindowFocus();

            onFocus?.Invoke();

        }

        void OnLostFocus()
        {
            foreach (var view in viewHandler.views)
                view.Key.OnWindowLostFocus();
        }

        void OnGUI()
        {

            foreach (var view in viewHandler.views)
                view.Key.OnGUI();

            if (wantsConstantRepaint)
                Repaint();

        }

        #endregion
        #region Appearance settings

        void ApplyAppearanceSettings()
        {
            foreach (var view in viewHandler.all)
                view.Key.ApplyAppearanceSettings();
        }

        #endregion
        #region Close on uninstall

        [InitializeOnLoadMethod]
        static void OnLoad()
        {
            Events.registeringPackages += (e) =>
            {
                var id = SceneManager.package.id;
                if (e.removed.Any(p => p.packageId == id))
                    Close();
            };
        }

        #endregion

    }

}
