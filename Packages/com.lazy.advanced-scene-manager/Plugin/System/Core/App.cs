using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdvancedSceneManager.Callbacks;
using AdvancedSceneManager.Models;
using AdvancedSceneManager.Utility;
using Lazy.Utility;
using UnityEngine;
using System.Runtime.CompilerServices;


#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using AdvancedSceneManager.Editor.Utility;
#endif

namespace AdvancedSceneManager.Core
{

    /// <summary>Manages startup and quit processes.</summary>
    /// <remarks>Usage: <see cref="SceneManager.app"/>.</remarks>
    public sealed class App : DependencyInjection.IApp
    {

        #region Initialize

        [RuntimeInitializeOnLoadMethod]
        [InitializeInEditorMethod]
        static void OnLoad()
        {

            SceneManager.app.isStartupFinished = false;

            SceneManager.OnInitialized(() =>
            {

#if UNITY_EDITOR
                if (!Application.isPlaying)
                    InitializeEditor();
                else if (!shouldRunStartupProcess)
                    TrackScenes();
                else
                    SceneManager.app.StartInternal();
#else
                SceneManager.app.StartInternal();
#endif

            });

        }

        static void TrackScenes()
        {
            foreach (var scene in SceneUtility.GetAllOpenUnityScenes())
                if (SceneManager.assets.scenes.TryFind(scene.path, out var s))
                    SceneManager.runtime.Track(s, scene);
        }

        #region Editor initialization

        static void InitializeEditor()
        {

#if UNITY_EDITOR

            SetProfile();

            if (!Application.isBatchMode)
            {
                CallbackUtility.Initialize();
                BuildUtility.Initialize();
            }

#endif

        }

#if UNITY_EDITOR

        static void SetProfile()
        {

            Profile.SetProfile(GetProfile(), updateBuildSettings: !Application.isBatchMode);

            static Profile GetProfile()
            {

                if (Application.isBatchMode)
                    return Profile.buildProfile;

                return GetFirstNonNull(
                    Profile.forceProfile,
                    SceneManager.settings.user.activeProfile,
                    Profile.defaultProfile,
                    SceneManager.assets.profiles.Count() == 1 ? SceneManager.assets.profiles.ElementAt(0) : null);

            }

            static Profile GetFirstNonNull(params Profile[] profile)
            {
                return profile.NonNull().FirstOrDefault();
            }

        }

#endif

        #endregion

        #endregion
        #region Properties

        /// <summary>An object that persists start properties across domain reload, which is needed when configurable enter play mode is set to reload domain on enter play mode.</summary>
        [Serializable]
        public class StartupProps
        {

            public StartupProps()
            { }

            /// <summary>Creates a new props, from the specified props, copying its values.</summary>
            public StartupProps(StartupProps props)
            {
                forceOpenAllScenesOnCollection = props.forceOpenAllScenesOnCollection;
                fadeColor = props.fadeColor;
                openCollection = props.openCollection;
                m_runStartupProcessWhenPlayingCollection = props.m_runStartupProcessWhenPlayingCollection;
                softSkipSplashScreen = props.softSkipSplashScreen;
            }

            /// <summary>Specifies whatever splash screen should open, but be skipped.</summary>
            /// <remarks>Used by ASMSplashScreen.</remarks>
            [NonSerialized] internal bool softSkipSplashScreen;

            /// <summary>Specifies whatever all scenes on <see cref="openCollection"/> should be opened.</summary>
            public bool forceOpenAllScenesOnCollection;

            /// <summary>The color for the fade out.</summary>
            /// <remarks>Unity splash screen color will be used if <see langword="null"/>.</remarks>
            public Color? fadeColor;

            [SerializeField] private bool? m_runStartupProcessWhenPlayingCollection;

            /// <summary>Specifies whatever startup process should run before <see cref="openCollection"/> is opened.</summary>
            public bool runStartupProcessWhenPlayingCollection
            {
#if UNITY_EDITOR
                get => m_runStartupProcessWhenPlayingCollection ?? SceneManager.settings.user.startupProcessOnCollectionPlay;
#else
                get => m_runStartupProcessWhenPlayingCollection ?? false;
#endif
                set => m_runStartupProcessWhenPlayingCollection = value;
            }

            /// <summary>Gets if startup process should run.</summary>
            public bool runStartupProcess =>
                openCollection
                ? runStartupProcessWhenPlayingCollection
                : true;

            /// <summary>Specifies a collection to be opened after startup process is done.</summary>
            public SceneCollection openCollection;

            /// <summary>Gets the effective fade animation color, uses <see cref="fadeColor"/> if specified. Otherwise <see cref="PlayerSettings.SplashScreen.backgroundColor"/> will be used during first startup. On subsequent restarts <see cref="Color.black"/> will be used (ASM restart, not application restart!).</summary>
            public Color effectiveFadeColor => fadeColor ?? (SceneManager.app.isRestart ? Color.black : SceneManager.settings.project.buildUnitySplashScreenColor);

        }

        /// <summary>Gets the props that should be used for startup process.</summary>
        public StartupProps startupProps
        {
            get => SessionStateUtility.Get<StartupProps>(null, $"ASM.App.{nameof(startupProps)}");
            set => SessionStateUtility.Set(value, $"ASM.App.{nameof(startupProps)}");
        }

#if UNITY_EDITOR

        /// <summary>Gets whatever we're currently in build mode.</summary>
        /// <remarks>This is <see langword="true"/> when in build or when play button in scene manager window is pressed. Also <see langword="true"/> when any start or restart method in app class is called.</remarks>
        public bool isBuildMode
        {
            get => SessionStateUtility.Get(false, $"ASM.App.{nameof(isBuildMode)}");
            private set => SessionStateUtility.Set(value, $"ASM.App.{nameof(isBuildMode)}");
        }

#else
        public bool isBuildMode => true;
#endif

        /// <summary>Gets if startup process is finished.</summary>
        public bool isStartupFinished { get; private set; }

        /// <summary>Gets if ASM has been restarted, or is currently restarting.</summary>
        public bool isRestart { get; private set; }

#if UNITY_EDITOR

        static bool shouldRunStartupProcess
        {
            get => SessionStateUtility.Get(false, $"ASM.App.{nameof(shouldRunStartupProcess)}");
            set => SessionStateUtility.Set(value, $"ASM.App.{nameof(shouldRunStartupProcess)}");
        }

        static SavedSceneSetup savedSceneSetup
        {
            get => SessionStateUtility.Get<SavedSceneSetup>(null, $"ASM.App.{nameof(savedSceneSetup)}");
            set => SessionStateUtility.Set(value, $"ASM.App.{nameof(savedSceneSetup)}");
        }

        [Serializable]
        class SavedSceneSetup
        {
            public SceneSetup[] scenes;
        }

#endif

        #endregion
        #region No profile warning

        void CheckProfile()
        {

#if !UNITY_EDITOR

            if (!Application.isPlaying)
                return;

            if (!SceneManager.settings.project)
                NoProfileWarning.Show("Could not find ASM settings!");
            else if (!SceneManager.settings.project.buildProfile)
                NoProfileWarning.Show("Could not find build profile!");

#endif

        }

        class NoProfileWarning : MonoBehaviour
        {

            static string text;
            public static void Show(string text)
            {
                Debug.LogError(text);
                NoProfileWarning.text = text;
                if (!Profile.current)
                    _ = SceneManager.runtime.AddToDontDestroyOnLoad<NoProfileWarning>();
            }

            void Start()
            {
                DontDestroyOnLoad(gameObject);
                Update();
            }

            void Update()
            {
                if (Profile.current)
                    Destroy(gameObject);
            }

            GUIContent content;
            GUIStyle style;
            void OnGUI()
            {

                content ??= new GUIContent(text);
                style ??= new GUIStyle(GUI.skin.label) { fontSize = 22 };

                var size = style.CalcSize(content);
                GUI.Label(new Rect((Screen.width / 2) - (size.x / 2), (Screen.height / 2) - (size.y / 2), size.x, size.y), content, style);

            }

        }

        #endregion
        #region Internal start

        void StartInternal()
        {

            ResetQuitStatus();
            CheckProfile();
            SetLoadingPriority();
            UnsetBuildModeOnEditMode();

            FallbackSceneUtility.EnsureOpen();

            if (isBuildMode)
                Restart();

        }

        void SetLoadingPriority()
        {

            if (SceneManager.profile && SceneManager.profile.enableChangingBackgroundLoadingPriority)
                Application.backgroundLoadingPriority = SceneManager.profile.backgroundLoadingPriority;

        }

        void UnsetBuildModeOnEditMode()
        {
#if UNITY_EDITOR

            EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;

            void EditorApplication_playModeStateChanged(PlayModeStateChange state)
            {
                if (state == PlayModeStateChange.EnteredEditMode)
                    isBuildMode = false;
            }

#endif
        }

        #endregion
        #region Start / Restart

        GlobalCoroutine coroutine;

        /// <inheritdoc cref="RestartInternal(StartupProps)"/>
        public void Restart(StartupProps props = null) =>
            RestartInternal(props);

        /// <inheritdoc cref="RestartInternal(StartupProps)"/>
        public Async<bool> RestartAsync(StartupProps props = null) =>
            RestartInternal(props);

        Async<bool> currentProcess;
        /// <summary>Restarts the ASM startup process.</summary>
        Async<bool> RestartInternal(StartupProps props = null)
        {

            if (currentProcess is not null)
                return currentProcess;

            CancelStartup();

            if (props is not null)
                startupProps = props;

            startupProps ??= new();

            coroutine?.Stop();

#if UNITY_EDITOR
            if (!Application.isPlaying)
                if (!TryEnterPlayMode())
                    return Async<bool>.FromResult(false);
#endif

            coroutine = DoStartupProcess(startupProps).StartCoroutine(description: "ASM Startup", onComplete: () => currentProcess = null);
            currentProcess = new(coroutine, () => isStartupFinished);
            return currentProcess;

        }

        public void CancelStartup() =>
            coroutine?.Stop();

#if UNITY_EDITOR

        /// <summary>Tries to enter play mode. Returns <see langword="false"/> if user denies to save modified scenes.</summary>
        /// <remarks>Prompt to save modifies scenes can be overriden, see <see cref="ASMUserSettings.alwaysSaveScenesWhenEnteringPlayMode"/>.</remarks>
        bool TryEnterPlayMode()
        {

            if (SceneManager.settings.user.alwaysSaveScenesWhenEnteringPlayMode)
                EditorSceneManager.SaveOpenScenes();

            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return false;

            EditorApplication.EnterPlaymode();

            shouldRunStartupProcess = true;
            isBuildMode = true;

            return true;

        }

#endif

        #endregion
        #region Startup process

        #region Progress

        readonly Dictionary<string, float> progress = new()
        {
            { nameof(CloseAllScenes), 0f },
            { nameof(OpenCollections), 0f },
            { nameof(OpenScenes), 0f },
            { nameof(OpenCollection), 0f },
        };

        void OnProgress(float value, [CallerMemberName] string name = "")
        {

            progress[name] = Mathf.Clamp01(value);

            if (splashScreen)
                splashScreen.OnProgressChanged(value);

#if UNITY_EDITOR
            Progress.Report(progressID, progress.Values.Sum() / progress.Count);
#endif

        }

        void OnDone([CallerMemberName] string name = "") =>
            OnProgress(1, name);

#if UNITY_EDITOR
        static int progressID;
#endif
        void SetupProgress()
        {
            UnsetupProgress();
#if UNITY_EDITOR
            progressID = Progress.Start("ASM Startup");
#endif
        }

        void UnsetupProgress()
        {

            foreach (var item in progress.Keys.ToArray())
                progress[item] = 0;

#if UNITY_EDITOR
            if (Progress.Exists(progressID))
                Progress.Remove(progressID);
            progressID = -1;
#endif

        }

        #endregion

        /// <summary>Occurs before restart process has begun, but has been initiated.</summary>
        public event Action beforeRestart;

        /// <summary>Occurs after restart has been completed.</summary>
        public event Action afterRestart;

        SplashScreen splashScreen;

        IEnumerator DoStartupProcess(StartupProps props)
        {

            isRestart = isStartupFinished;
            isStartupFinished = false;

            //Fixes issue where first scene cannot be opened when user are not using configurable enter play mode
            yield return null;

#if UNITY_EDITOR

            LogUtility.LogStartupBegin();
            if (!SceneManager.profile)
            {
                Debug.LogError("No profile set.");
                yield break;
            }

#endif

            QueueUtility<SceneOperation>.StopAll();
            beforeRestart?.Invoke();

            SetupProgress();

            yield return OpenSplashScreen(props);
            yield return CloseAllScenes(props);

            yield return OpenScenes(props, true);
            yield return OpenCollections(props);
            yield return OpenCollection(props);
            yield return OpenScenes(props, false);

            yield return CloseSplashScreen();
            UnsetupProgress();

            if (!SceneManager.openScenes.Any())
                Debug.LogWarning("No scenes opened during startup.");

#if UNITY_EDITOR
            shouldRunStartupProcess = false;
#endif

            isStartupFinished = true;

            afterRestart?.Invoke();
            LogUtility.LogStartupEnd();

        }

        IEnumerator CloseAllScenes(StartupProps _)
        {

            SceneManager.runtime.Reset();
            if (splashScreen)
                SceneManager.runtime.Track(splashScreen.ASMScene());

            var scenes = SceneUtility.GetAllOpenUnityScenes().
                Where(s => !FallbackSceneUtility.IsFallbackScene(s)).
                Where(s => !Profile.current.startupScene || Profile.current.startupScene.name != s.name).
                Where(s => s.IsValid()).
                ToArray();

            var progress = scenes.ToDictionary(s => s, s => 0f);

            if (scenes.Length > 0)
                foreach (var scene in scenes)
                {

                    FallbackSceneUtility.EnsureOpen();
                    //yield return null;

                    var isSplash = splashScreen && scene == splashScreen.gameObject.scene;
                    if (scene.IsValid() && !FallbackSceneUtility.IsFallbackScene(scene) && !isSplash)
                    {

#if UNITY_EDITOR
                        if (SceneImportUtility.StringExtensions.IsTestScene(scene.path))
                            continue;
#endif

                        yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene).WithProgress(value =>
                        {
                            progress[scene] = value;
                            OnProgress(Mathf.Clamp01(progress.Values.Sum() / scenes.Length));
                        });

                    }

                }

            OnDone();

        }

        IEnumerator OpenSplashScreen(StartupProps props)
        {

            if (props.runStartupProcess && Profile.current && Profile.current.splashScene)
            {

                yield return EnsureClosed();

                var async = LoadingScreenUtility.OpenLoadingScreen<SplashScreen>(Profile.current.splashScene);
                yield return async;

                splashScreen = async.value;

                if (splashScreen)
                    splashScreen.ASMScene().SetActive();

            }

            OnDone();

            IEnumerator EnsureClosed()
            {

                var scenes = SceneUtility.GetAllOpenUnityScenes().Where(s => s.path == Profile.current.splashScene.path);

                foreach (var scene in scenes)
                    yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);

            }

        }

        IEnumerator CloseSplashScreen()
        {
            if (splashScreen)
                yield return LoadingScreenUtility.CloseLoadingScreen(splashScreen);
        }

        IEnumerator OpenCollections(StartupProps props)
        {

            if (props.runStartupProcess)
            {

                var collections = Profile.current.startupCollections.ToArray();
                var progress = collections.ToDictionary(c => c, c => 0f);

                if (collections.Length > 0)
                    foreach (var collection in collections)
                        yield return collection.Open().DisableLoadingScreen().ReportProgress(f =>
                        {
                            progress[collection] = f;
                            OnProgress(progress.Values.Sum() / progress.Count);
                        });

            }

            OnDone();

        }

        IEnumerator OpenScenes(StartupProps props, bool persistent)
        {

            var scenes = Profile.current.startupScenes.Where(s => persistent == s.keepOpenWhenCollectionsClose);
            var progress = scenes.ToDictionary(c => c, c => 0f);

            foreach (var scene in scenes)
                yield return scene.Open().ReportProgress(f => progress[scene] = f);

            OnDone();

        }

        IEnumerator OpenCollection(StartupProps props)
        {

            var collection = props.openCollection;
            if (collection)
                yield return collection.Open(openAll: props.forceOpenAllScenesOnCollection).DisableLoadingScreen().ReportProgress(f => OnProgress(f));

            OnDone();

        }

        #endregion
        #region Quit

        #region Callbacks

        readonly List<IEnumerator> callbacks = new();

        /// <summary>Register a callback to be called before quit.</summary>
        public void RegisterQuitCallback(IEnumerator coroutine) => callbacks.Add(coroutine);

        /// <summary>Unregister a callback that was to be called before quit.</summary>
        public void UnregisterQuitCallback(IEnumerator coroutine) => callbacks.Remove(coroutine);

        IEnumerator CallSceneCloseCallbacks()
        {
            yield return CallbackUtility.Invoke<ISceneClose>().OnAllOpenScenes();
        }

        IEnumerator CallCollectionCloseCallbacks()
        {
            if (SceneManager.openCollection)
                yield return CallbackUtility.Invoke<ICollectionClose>().WithParam(SceneManager.openCollection).OnAllOpenScenes();
        }

        #endregion

        internal void ResetQuitStatus()
        {
            isQuitting = false;
            cancelQuit = false;
        }

        /// <summary>Gets whatever ASM is currently in the process of quitting.</summary>
        public bool isQuitting { get; private set; }

        bool cancelQuit;

        /// <summary>Cancels a quit in progress.</summary>
        /// <remarks>Only usable during a <see cref="RegisterQuitCallback(IEnumerator)"/> or while <see cref="isQuitting"/> is true.</remarks>
        public void CancelQuit()
        {
            if (isQuitting)
                cancelQuit = true;
        }

        /// <summary>Quits the game, and calls quitCallbacks, optionally with a fade animation.</summary>
        /// <param name="fade">Specifies whatever screen should fade out.</param>
        /// <param name="fadeColor">Defaults to <see cref="ProjectSettings.buildUnitySplashScreenColor"/>.</param>
        /// <param name="fadeDuration">Specifies the duration of the fade out.</param>
        public void Quit(bool fade = true, Color? fadeColor = null, float fadeDuration = 1)
        {

            Coroutine().StartCoroutine();
            IEnumerator Coroutine()
            {

                QueueUtility<SceneOperation>.StopAll();

                isQuitting = true;
                cancelQuit = false;

                var wait = new List<IEnumerator>();

                var async = LoadingScreenUtility.FadeOut(fadeDuration, fadeColor);
                yield return async;
                wait.Add(new WaitForSecondsRealtime(0.5f));

                wait.AddRange(callbacks);
                wait.Add(CallCollectionCloseCallbacks());
                wait.Add(CallSceneCloseCallbacks());

                yield return wait.WaitAll(isCancelled: () => cancelQuit);

                if (cancelQuit)
                {
                    cancelQuit = false;
                    isQuitting = false;
                    if (async?.value)
                        yield return LoadingScreenUtility.CloseLoadingScreen(async.value);
                    yield break;
                }

                Exit();

            }

        }

        /// <summary>Exits the game like you normally would in unity.</summary>
        /// <remarks>No callbacks will be called, and no fade out will occur.</remarks>
        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        #endregion

    }

}
