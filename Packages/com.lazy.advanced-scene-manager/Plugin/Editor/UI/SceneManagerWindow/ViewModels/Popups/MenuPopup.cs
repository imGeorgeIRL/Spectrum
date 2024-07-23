using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.Utility;
using AdvancedSceneManager.Models;
using Lazy.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Environment = System.Environment;
using Object = UnityEngine.Object;

namespace AdvancedSceneManager.Editor.UI.Views.Popups
{

    static class Icons
    {

        public const string UpdateNotNeeded = "";
        public const string UpdateAvailable = "";
        public const string UpdateIsChecking = "";
        public const string UpdateIsTooOutOfDate = "X";

        public const string DebugEnabled = "";
        public const string DebugDisabled = "";

    }

    static class Urls
    {
        public const string AssetStoreReview = "https://assetstore.unity.com/packages/tools/utilities/advanced-scene-manager-174152#reviews";
        public const string GithubIssue = "https://github.com/Lazy-Solutions/AdvancedSceneManager/issues/new";
        public const string Discord = "https://discord.gg/pnRn6zeFEJ";
        public const string GithubDocs = "https://github.com/Lazy-Solutions/AdvancedSceneManager/blob/2.0";
        public const string GithubReleases = "https://github.com/Lazy-Solutions/AdvancedSceneManager/releases/latest";
        public const string GithubPatchFile = "https://gist.githubusercontent.com/Zumwani/195afd3053cf1cb951013e30908903c0/raw";
        public const string Examples = "https://github.com/Lazy-Solutions/AdvancedSceneManager/blob/main/examples.md";
    }

    class MenuPopup : ViewModel, IPopup
    {

        [InitializeOnLoadMethod]
        static void OnLoad()
        {
            CoroutineUtility.Timer(() => _ = CheckUpdate(), TimeSpan.FromHours(1));
        }

        public override void OnAdded()
        {
            InitializeUpdate(view);
            InitializeBuild(view);
            InitializeDocs(view);
            InitializeReview(view);
        }

        ASMUserSettings settings => SceneManager.settings.user;

        #region Parameters

        public static object flashDocsSection { get; } = new();

        public override void PassParameter(object parameter)
        {
            if (parameter == flashDocsSection)
                FlashDocs();
        }

        void FlashDocs() =>
            FlashBorderColor(view.Q("section-docs"), Color.clear, Color.white, 500).StartCoroutine();

        private IEnumerator FlashBorderColor(VisualElement element, Color color1, Color color2, float duration)
        {

            SceneManagerWindow.window.wantsConstantRepaint = true;
            yield return new WaitForSeconds(0.25f);

            yield return AnimateBorderColor(element, color1, color2, duration);
            yield return new WaitForSeconds(0.25f);
            yield return AnimateBorderColor(element, color2, color1, duration);
            yield return new WaitForSeconds(0.25f);

            SceneManagerWindow.window.wantsConstantRepaint = false;

        }

        IEnumerator AnimateBorderColor(VisualElement element, Color color1, Color color2, float duration)
        {

            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {

                elapsedTime += Time.deltaTime;
                var t = elapsedTime / duration;
                var easedT = EaseOutExpo(t);
                var newColor = Color.Lerp(color1, color2, easedT);

                SetBorderColor(element, newColor);

                yield return null;

            }

            SetBorderColor(element, color2);

        }

        void SetBorderColor(VisualElement element, Color color)
        {
            element.style.borderTopColor = color;
            element.style.borderBottomColor = color;
            element.style.borderLeftColor = color;
            element.style.borderRightColor = color;
        }

        private float EaseOutExpo(float t)
        {
            return t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
        }

        #endregion
        #region Update

        static bool isCheckingForUpdates;
        static bool isUpdating;
        static Button updateButton;

        static string cachedPatchVersion;
        static bool cachedIsMajorVersionRequired;

        static bool cachedIsPatchAvailable =>
                !cachedIsMajorVersionRequired &&
                Version.TryParse(SceneManager.package.version, out var currentVersion) &&
                Version.TryParse(cachedPatchVersion, out var patchVersion) &&
                patchVersion > currentVersion;

        static string lastPatchWhenNotified
        {
            get => SessionState.GetString($"ASM.{nameof(lastPatchWhenNotified)}", string.Empty);
            set => SessionState.SetString($"ASM.{nameof(lastPatchWhenNotified)}", value);
        }

        static bool hasNotifiedAboutVersion =>
             Version.TryParse(lastPatchWhenNotified, out var lastNotifyPatch) &&
             Version.TryParse(cachedPatchVersion, out var patchVersion) &&
             lastNotifyPatch >= patchVersion;


#if ASM_DEV
        static string actualPatchVersion;
#endif

        void InitializeUpdate(VisualElement element)
        {

            element.Q<Label>("text-version").text = SceneManager.package.version;
            SetupLink(element.Q<Button>("button-view-updates"), Urls.GithubReleases);

            updateButton = element.Q<Button>("button-update");
            updateButton.clicked += async () =>
            {
                await CheckUpdate(true);
                if (cachedIsPatchAvailable)
                    Update();
            };

            _ = CheckUpdate();

        }

        static IVisualElementScheduledItem rotateAnimation;
        static void UpdateUpdateButton()
        {

            if (updateButton == null)
                return;

            if (isCheckingForUpdates || isUpdating)
            {

                updateButton.pickingMode = PickingMode.Ignore;

                updateButton.text = Icons.UpdateIsChecking;
                rotateAnimation = updateButton.Rotate(speed: 8);

            }
            else
            {

                updateButton.pickingMode = PickingMode.Position;

                if (cachedIsMajorVersionRequired)
                {
                    updateButton.text = Icons.UpdateIsTooOutOfDate;
                    updateButton.tooltip = "Your ASM version is too old to receive patches, please install the latest asset store version.";
                }
                else if (cachedIsPatchAvailable)
                {
                    updateButton.text = Icons.UpdateAvailable;
                    updateButton.tooltip = $"An update is available, click here to apply it.";
                }
                else
                {
                    updateButton.text = Icons.UpdateNotNeeded;
                    updateButton.tooltip = "Your version of ASM is currently up to date. Click here to check again.";
                }

                rotateAnimation.Pause();
                updateButton.style.rotate = new(StyleKeyword.Initial);

            }

        }

        public async static Task CheckUpdate(bool logError = false)
        {

            if (isCheckingForUpdates)
                return;
            isCheckingForUpdates = true;

            UpdateUpdateButton();

            try
            {

                using var client = new HttpClient();
                var text = await client.GetStringAsync(Urls.GithubPatchFile);

                if (!text.Contains("\n"))
                    throw new Exception("Could not parse version file.");

                var versionStr = text[..text.IndexOf("\n")];
                var patchNotes = text[text.IndexOf("\n")..];

#if ASM_DEV
                actualPatchVersion = versionStr;
                versionStr = "2.1.44";
#endif

                if (!Version.TryParse(versionStr, out var patchVersion))
                    throw new Exception("Could not parse version file.");

                if (!Version.TryParse(SceneManager.package.version, out var currentVersion))
                    throw new Exception("Could not retrieve current version.");

                if (currentVersion < patchVersion && patchVersion.Minor > currentVersion.Minor)
                {
                    cachedPatchVersion = "";
                    cachedIsMajorVersionRequired = true;
                }
                else
                {

                    cachedPatchVersion = versionStr;
                    cachedIsMajorVersionRequired = false;

                    if (patchVersion > currentVersion && !hasNotifiedAboutVersion)
                    {
                        Debug.Log($"ASM {versionStr} is available:{patchNotes}");
                        lastPatchWhenNotified = cachedPatchVersion;
                    }

                }

            }
            catch (Exception e)
            {
                if (logError)
                {
                    Debug.LogError("An error occured when checking for updates, please try again. Exception has will be logged below.");
                    Debug.LogException(e);
                }
            }

            isCheckingForUpdates = false;
            UpdateUpdateButton();

        }

        async void Update()
        {

            try
            {

                isUpdating = true;
                UpdateUpdateButton();

#if ASM_DEV
                cachedPatchVersion = actualPatchVersion;
#endif

                var url = $"https://github.com/Lazy-Solutions/AdvancedSceneManager/releases/download/{cachedPatchVersion}/AdvancedSceneManager.{cachedPatchVersion}.partial.unitypackage";

                using var client = new HttpClient();
                var stream = await client.GetStreamAsync(url);

                var path = Path.Combine(Path.GetTempPath(), "ASM", "update");
                if (!Directory.Exists(path))
                    _ = Directory.CreateDirectory(path);

                path = Path.Combine(path, $"ASM.{cachedPatchVersion}.partial.unitypackage");
                using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                await stream.CopyToAsync(fs);

                fs.Close();
                stream.Close();

#if !ASM_DEV
                AssetDatabase.ImportPackage(path, true);
#else
                Debug.Log($"AssetDatabase.ImportPackage({path}, true);");
#endif

                isUpdating = false;
                UpdateUpdateButton();
                view.Q<Label>("text-version").text = SceneManager.package.version;

            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

        }

        #endregion
        #region Build

        void InitializeBuild(VisualElement element)
        {

            InitializeFolderButton(element);
            InitializeProfilerButton(element);

            element.Q<Button>("button-build").clicked += DoDevBuild;

        }

        public void DoDevBuild()
        {
            var path = GetBuildPath().Replace("%temp%", Path.GetTempPath());
            BuildUtility.DoBuild(path + "/app.exe", attachProfiler: settings.quickBuildUseProfiler, true);
        }

        string GetBuildPath() =>
            string.IsNullOrEmpty(settings.quickBuildPath)
            ? Path.Combine("%temp%", "ASM", "builds", Application.companyName, Application.productName)
            : settings.quickBuildPath;

        void InitializeFolderButton(VisualElement element)
        {

            var folderButton = element.Q<Button>("button-build-folder");
            folderButton.clicked += () =>
            {

                var path = EditorUtility.OpenFolderPanel("Select folder to put builds in...", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Application.productName);
                settings.quickBuildPath = Directory.Exists(path) ? path : null;

                UpdateFolderButton();

            };

            UpdateFolderButton();
            void UpdateFolderButton() =>
                folderButton.tooltip = GetBuildPath();
        }

        void InitializeProfilerButton(VisualElement element)
        {

            var profilerButton = element.Q<Button>("button-build-profiler");

            profilerButton.clicked += () =>
            {
                settings.quickBuildUseProfiler = !settings.quickBuildUseProfiler;
                UpdateProfilerButton();
            };

            UpdateProfilerButton();
            void UpdateProfilerButton()
            {
                profilerButton.text = settings.quickBuildUseProfiler ? Icons.DebugEnabled : Icons.DebugDisabled;
                profilerButton.tooltip = settings.quickBuildUseProfiler ? "Profiler enabled. Press to disable." : "Profiler disabled. Press to enable.";
            }

        }

        #endregion
        #region Docs

        void InitializeDocs(VisualElement element)
        {

            SetupLink(element.Q<Button>("button-docs-online"), Urls.GithubDocs, HideNotification);

            var obj = FindLocalDocs();
            var folder = $"{ProjectWindowUtil.GetContainingFolder(AssetDatabase.GetAssetPath(obj))}/";

            var button = element.Q<Button>("button-docs-local");
            SetupTooltip(button, folder);

            button.clicked += () =>
            {
                HideNotification();
                EditorGUIUtility.PingObject(obj);
            };

            void HideNotification()
            {

                SceneManager.settings.user.m_hideDocsNotification = true;
                SceneManager.settings.user.Save();

                notifications.ReloadPersistentNotifications();

            }

        }

        Object FindLocalDocs()
        {
            var path = $"{SceneManager.package.folder}/Documentation/_api-reference.pdf";
            return AssetDatabase.LoadAssetAtPath<Object>(path);
        }

        #endregion
        #region Review

        void InitializeReview(VisualElement element)
        {
            SetupLink(element.Q<Button>("button-asset-store"), Urls.AssetStoreReview);
            SetupLink(element.Q<Button>("button-github"), Urls.GithubIssue);
            SetupLink(element.Q<Button>("button-discord"), Urls.Discord);
        }

        #endregion

        void SetupLink(Button button, string url, Action onClicked = null)
        {

            button.clicked += () =>
            {
                Application.OpenURL(url);
                onClicked?.Invoke();
            };

            SetupTooltip(button, url);

        }

        void SetupTooltip(Button button, string url)
        {
            if (EditorGUIUtility.isProSkin)
                button.tooltip += "\n\n" + $"<color=#00ffffff>{url}</color>";
            else
                button.tooltip += "\n\n" + $"<color=#00008B>{url}</color>";
        }
    }

}
