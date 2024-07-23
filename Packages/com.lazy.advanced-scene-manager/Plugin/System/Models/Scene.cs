using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Component = UnityEngine.Component;
using AdvancedSceneManager.Utility;
using AdvancedSceneManager.Core;

using unityScene = UnityEngine.SceneManagement.Scene;
using AdvancedSceneManager.Models.Enums;
using AdvancedSceneManager.Models.Internal;
using System.IO;
using Object = UnityEngine.Object;
using AdvancedSceneManager.Utility.CrossSceneReferences;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AdvancedSceneManager.Models
{

    /// <summary>Represents a scene.</summary>
    public partial class Scene : ASMModel,
        Scene.IEquality,
        Scene.IMethods, Scene.IMethods.IEvent,
        ILockable
    {

        #region Properties

        /// <summary>Gets whatever we are tracked by AssetRef.</summary>
        /// <remarks>Only available in editor.</remarks>
        public bool isImported => Assets.scenes.Contains(this);

        /// <summary>Gets whatever this scene is included in build.</summary>
        public bool isIncluded => SceneUtility.IsIncluded(this);

        #region SceneAsset

        [Header("Scene")]
        [SerializeField] private string m_path;
        [SerializeField] private Object m_sceneAsset;

#if UNITY_EDITOR

        /// <summary>Gets the path of this <see cref="Scene"/>.</summary>
        /// <remarks>Only available in editor.</remarks>
        public string asmPath => AssetDatabase.GetAssetPath(this);

        /// <summary>Gets the associated <see cref="SceneAsset"/>.</summary>
        /// <remarks>Only available in the editor.</remarks>
        public SceneAsset sceneAsset
        {
            get => m_sceneAsset as SceneAsset;
            internal set => m_sceneAsset = value;
        }

#endif

        /// <summary>Gets if <see cref="m_sceneAsset"/> has a value.</summary>
        /// <remarks>Only available in the editor.</remarks>
        public bool hasSceneAsset => m_sceneAsset;

        /// <summary>Gets the path of the associated <see cref="SceneAsset"/>.</summary>
        public string path
        {
            get => m_path ?? "";
            internal set { m_path = value; OnPropertyChanged(); }
        }

        #endregion
        #region Loading screen / splash screen

        [Header("Special scene flags")]
        [SerializeField] private bool m_isLoadingScreen;
        [SerializeField] private bool m_isSplashScreen;

        /// <summary>Gets if this scene is a loading screen.</summary>
        /// <remarks>
        /// <para>Automatically updated.</para>
        /// <para>If this is <see langword="false"/> for an actual loading screen, please make sure scene contains a <see cref="Callbacks.LoadingScreen"/> script.</para>
        /// <para>Scene might sometimes have to be re-saved for this flag to appear.</para>
        /// </remarks>
        public bool isLoadingScreen
        {
            get => m_isLoadingScreen;
            internal set => m_isLoadingScreen = value;
        }

        /// <summary>Gets if this scene is a splash screen.</summary>
        /// <remarks>
        /// <para>Automatically updated.</para>
        /// <para>If this is <see langword="false"/> for an actual splash screen screen, please make sure scene contains a <see cref="Callbacks.SplashScreen"/> script.</para>
        /// <para>Scene might sometimes have to be re-saved for this flag to appear.</para>
        /// </remarks>
        public bool isSplashScreen
        {
            get => m_isSplashScreen;
            internal set => m_isSplashScreen = value;
        }

        /// <summary>Gets if this is a 'special' scene.</summary>
        /// <remarks>A scene is special if any of the following is <see langword="true"/>: <see cref="isSplashScreen"/>, <see cref="isLoadingScreen"/> or <see cref="isDontDestroyOnLoad"/>.</remarks>
        public bool isSpecial =>
            isSplashScreen || isLoadingScreen || isDontDestroyOnLoad;

        #endregion
        #region Persistent properties

        [Header("Persistent")]
        [SerializeField] private bool m_keepOpenWhenCollectionsClose;
        [SerializeField] private bool m_keepOpenWhenNewCollectionWouldReopen = true;

        /// <summary>Specifies whatever this scene will remain open when collections close.</summary>
        /// <remarks>You'll have to close it manually, if needed.</remarks>
        public bool keepOpenWhenCollectionsClose
        {
            get => m_keepOpenWhenCollectionsClose;
            set { m_keepOpenWhenCollectionsClose = value; OnPropertyChanged(); }
        }

        /// <summary>Specifies whatever this will remain open when a newly opened collection would have reopened it.</summary>
        /// <remarks>You'll have to close it manually, if needed.</remarks>
        public bool keepOpenWhenNewCollectionWouldReopen
        {
            get => m_keepOpenWhenNewCollectionWouldReopen;
            set { m_keepOpenWhenNewCollectionWouldReopen = value; OnPropertyChanged(); }
        }

        /// <summary>Gets whatever this scene will close normally after a collection closes.</summary>
        public bool isNonPersistant => !keepOpenWhenCollectionsClose && !keepOpenWhenNewCollectionWouldReopen;

        #endregion
        #region Startup / Auto open

        [Header("Auto open")]
        [SerializeField] private bool m_openOnStartup;
        [SerializeField] private bool m_openOnPlayMode;
        [SerializeField] private EditorPersistentOption m_autoOpenInEditor;
        [SerializeField] internal List<Scene> m_autoOpenInEditorScenes = new();

        /// <summary>Specifies whatever this scene should be opened on startup.</summary>
        /// <remarks>Only effective when scene added to <see cref="Profile.standaloneScenes"/>.</remarks>
        public bool openOnStartup
        {
            get => m_openOnStartup;
            set { m_openOnStartup = value; OnPropertyChanged(); }
        }

        /// <summary>Specifies whatever this scene should be opened when entering playmode.</summary>
        /// <remarks>Only effective when scene added to <see cref="Profile.standaloneScenes"/>.</remarks>
        public bool openOnPlayMode
        {
            get => m_openOnPlayMode;
            set { m_openOnPlayMode = value; OnPropertyChanged(); }
        }

        /// <summary>Specifies whatever this scene should be opened automatically outside of play-mode.</summary>
        public EditorPersistentOption autoOpenInEditor
        {
            get => m_autoOpenInEditor;
            set { m_autoOpenInEditor = value; OnPropertyChanged(); }
        }

        /// <summary>Specifies the scenes that should trigger this scene to open when <see cref="autoOpenInEditor"/> is set to <see cref="EditorPersistentOption.WhenAnyOfTheFollowingScenesAreOpened"/>.</summary>
        public List<Scene> autoOpenInEditorScenes => m_autoOpenInEditorScenes;

        #endregion
        #region Locking

        [Header("Locking")]
        [SerializeField] private bool m_isLocked;
        [SerializeField] private string m_lockMessage;

        /// <summary>Gets if this scene is locked.</summary>
        public bool isLocked
        {
            get => m_isLocked;
            set { m_isLocked = value; OnPropertyChanged(); }
        }

        /// <summary>Gets the lock message for this scene.</summary>
        public string lockMessage
        {
            get => m_lockMessage;
            set { m_lockMessage = value; OnPropertyChanged(); }
        }

        #endregion
        #region Cross-scene references

        [Header("Cross-scene references")]
        [SerializeField] private List<CrossSceneReference> m_crossSceneReferences = new();

        /// <summary>Enumerates the cross-scene references defined on this scene.</summary>
        public IEnumerable<CrossSceneReference> crossSceneReferences
        {
            get => m_crossSceneReferences;
            internal set
            {
                m_crossSceneReferences = value?.ToList() ?? new();
                Save();
            }
        }

#if UNITY_EDITOR

        /// <summary>Adds a cross-scene reference for this scene.</summary>
        /// <remarks>Only available in editor.</remarks>
        public void AddCrossSceneReference(CrossSceneReference reference)
        {

            if (RemoveCrossSceneReference(reference, out var i))
                m_crossSceneReferences.Insert(i, reference);
            else
                m_crossSceneReferences.Add(reference);

            Save();

        }

        /// <summary>Removes a cross-scene reference for this scene.</summary>
        /// <remarks>Only available in editor.</remarks>
        public void RemoveCrossSceneReference(CrossSceneReference reference)
        {
            RemoveCrossSceneReference(reference, out _);
            if (internalScene.HasValue)
            {
                CrossSceneReferenceUtility.ResetScene(internalScene.Value);
                CrossSceneReferenceUtility.ResolveScene(internalScene.Value);
            }
            Save();
        }

        bool RemoveCrossSceneReference(CrossSceneReference reference, out int index)
        {

            index = m_crossSceneReferences.FindIndex(r => r.id == reference.id);
            if (index == -1)
                return false;
            else
            {
                m_crossSceneReferences.RemoveAt(index);
                return true;
            }

        }

#endif

        #endregion
        #region Runtime properties

        /// <summary>Gets if this scene is currently active.</summary>
        public bool isActive =>
            SceneManager.runtime.activeScene == this;

        /// <summary>Gets whatever the scene is open in the hierarchy, this is <see langword="true"/> if scene is currently loading, if scene is preloaded, if scene is fully open.</summary>
        public bool isOpenInHierarchy =>
            SceneUtility.GetAllOpenUnityScenes().Any(s => s.path == path);

        /// <inheritdoc cref="Runtime.GetState(Scene)"/>
        public SceneState state =>
            SceneManager.runtime.GetState(this);

        /// <summary>Gets whatever the scene is open.</summary>
        public bool isOpen =>
            isDontDestroyOnLoad || SceneManager.runtime.IsTracked(this);

        /// <summary>Gets whatever the scene is preloaded.</summary>
        public bool isPreloaded =>
            state == SceneState.Preloaded;

        /// <summary>Gets the <see cref="unityScene"/> that this scene is associated with.</summary>
        /// <remarks><see langword="null"/> if scene is not open.</remarks>
        public unityScene? internalScene { get; internal set; }

        /// <summary>Gets if this scene is opened as persistent.</summary>
        public bool isPersistent =>
            isOpen &&
            isDontDestroyOnLoad ||
            keepOpenWhenCollectionsClose ||
            (openedBy && openedBy.openAsPersistent);

        internal SceneCollection openedBy { get; set; }

        /// <summary>Gets if this is a default ASM scene. These are located in 'Packages/Advanced Scene Manager/Default scenes/'.</summary>
        public bool isDefaultASMScene =>
            path.StartsWith("Packages/com.lazy.advanced-scene-manager/");

        /// <summary>Gets if this scene is the dontDestroyOnLoad scene.</summary>
        public bool isDontDestroyOnLoad =>
            internalScene?.handle == SceneManager.runtime.dontDestroyOnLoadScene.handle;

        /// <summary>Gets if this scene is dynamic, it is not persisted to disk.</summary>
        public bool isDynamic =>
            internalScene?.IsValid() ?? false && string.IsNullOrWhiteSpace(internalScene.Value.path);

        /// <summary>Gets the root game objects in this <see cref="Scene"/>.</summary>
        /// <remarks>Only usable if scene is open.</remarks>
        public IEnumerable<GameObject> GetRootGameObjects() =>
            (internalScene?.IsValid() ?? false)
            ? internalScene.Value.GetRootGameObjects()
            : Array.Empty<GameObject>();

        /// <summary>Finds the object in the hierarchy of this <see cref="Scene"/>.</summary>
        /// <remarks>Only works if scene is loaded.</remarks>
        public T FindObject<T>() where T : Component =>
            FindObjects<T>().FirstOrDefault();

        /// <inheritdoc cref="FindObject{T}()"/>
        public bool FindObject<T>(out T component) where T : Component =>
            component = FindObject<T>();

        /// <summary>Finds the objects in the hierarchy of this <see cref="Scene"/>.</summary>
        /// <remarks>Only works if scene is loaded.</remarks>
        public IEnumerable<T> FindObjects<T>() where T : Component =>
            GetRootGameObjects().SelectMany(o => o.GetComponentsInChildren<T>()).OfType<T>();

        #endregion

        #endregion
        #region Methods

        #region Shared

        #region Interfaces

        /// <summary>Defines a set of methods that is meant to be shared between: <see cref="Scene"/>, <see cref="ASMSceneHelper"/>, and <see cref="SceneManager.runtime"/>.</summary>
        interface IXmlDocsHelper
        { }

        /// <inheritdoc cref="IXmlDocsHelper"/>
        /// <remarks>Specified methods to be used programmatically, on the scene itself.</remarks>
        public interface IMethods
        {

            /// <summary>Opens the scene.</summary>
            /// <remarks>No effect if scene is already open.</remarks>
            public SceneOperation Open();

            /// <summary>Toggles this scene open or closed.</summary>
            public SceneOperation ToggleOpen();

            /// <summary>Closes the scene.</summary>
            /// <remarks>No effect if scene is already closed.</remarks>
            public SceneOperation Close();

            /// <summary>Preloads the scene, to be displayed at a later time. See also: <see cref="FinishPreload"/>, <see cref="DiscardPreload"/>.</summary>
            /// <remarks>Scene must be closed beforehand.</remarks>
            public SceneOperation Preload(Action onPreloaded = null);

            /// <summary>Finishes preloading the scene, displaying it.</summary>
            /// <remarks>Scene must be preloaded beforehand.</remarks>
            public SceneOperation FinishPreload();

            /// <summary>Discards the scene, if preloaded.</summary>
            public SceneOperation DiscardPreload();

            /// <summary>Opens the scene while a loading screen is open.</summary>
            public SceneOperation OpenWithLoadingScreen(Scene loadingScene);

            /// <summary>Opens the scene and sets it as active.</summary>
            public SceneOperation OpenAndActivate();

            /// <summary>Sets the scene as active in heirarchy.</summary>
            public void SetActive();

            /// <inheritdoc cref="IXmlDocsHelper"/>
            /// <remarks>Specifies methods to be used in UnityEvent, using the scene itself.</remarks>
            public interface IEvent
            {

                /// <summary>Event method. Its meant for <see cref="UnityEngine.Events.UnityEvent"/>.</summary>
                public void _Open();

                /// <inheritdoc cref="_Open"/>
                public void _ToggleOpen();

                /// <inheritdoc cref="_Open"/>
                public void _Close();

                /// <inheritdoc cref="_Open"/>
                public void _Preload();

                /// <inheritdoc cref="_Open"/>
                public void _FinishPreload();

                /// <inheritdoc cref="_Open"/>
                public void _DiscardPreload();

                /// <inheritdoc cref="_Open"/>
                public void _OpenWithLoadingScreen(Scene loadingScene);

                /// <inheritdoc cref="_Open"/>
                public void _SetActive();

                /// <inheritdoc cref="_Open"/>
                public void _OpenAndActivate();

            }

        }

        /// <inheritdoc cref="IXmlDocsHelper"/>
        /// <remarks>Specifies methods to be used programmatically, using scene as first parameter.</remarks>
        public interface IMethods_Target
        {

            /// <summary>Opens the specified scene.</summary>
            /// <remarks>Already open scenes not affected.</remarks>
            public SceneOperation Open(Scene scene);

            /// <summary>Toggles the open state of the specified scene.</summary>
            public SceneOperation ToggleOpen(Scene scene);

            /// <summary>Closes the specified scene.</summary>
            /// <remarks>Already closed scenes not affected.</remarks>
            public SceneOperation Close(Scene scene);

            /// <summary>Preloads the specified scene, to be displayed at a later time. See also: <see cref="FinishPreload(Scene)"/>, <see cref="DiscardPreload(Scene)"/>.</summary>
            /// <remarks>Scene must be closed beforehand.</remarks>
            public SceneOperation Preload(Scene scene, Action onPreloaded = null);

            /// <summary>Finishes preloading the specified scene, displaying it.</summary>
            /// <remarks>Scene must be preloaded beforehand.</remarks>
            public SceneOperation FinishPreload(Scene scene);

            /// <summary>Discards the specified scene, if preloaded.</summary>
            public SceneOperation DiscardPreload(Scene scene);

            /// <summary>Opens the specified scene while a loading screen is open.</summary>
            public SceneOperation OpenWithLoadingScreen(Scene scene, Scene loadingScene);

            /// <summary>Opens the scene and activates it.</summary>
            public SceneOperation OpenAndActivate(Scene scene);

            /// <summary>Sets the specified scene as active in heirarchy.</summary>
            public void SetActive(Scene scene);

            /// <inheritdoc cref="IXmlDocsHelper"/>
            /// <remarks>Specifies methods to be used in UnityEvent, when not using scene itself.</remarks>
            public interface IEvent
            {

                /// <inheritdoc cref="IMethods.IEvent._Open"/>
                public void _Open(Scene scene);

                /// <inheritdoc cref="_Open"/>
                public void _ToggleOpen(Scene scene);

                /// <inheritdoc cref="_Open"/>
                public void _Close(Scene scene);

                /// <inheritdoc cref="_Open"/>
                public void _Preload(Scene scene);

                /// <inheritdoc cref="_Open"/>
                public void _FinishPreload(Scene scene);

                /// <inheritdoc cref="_Open"/>
                public void _DiscardPreload(Scene scene);

                /// <inheritdoc cref="_Open"/>
                public void _SetActive(Scene scene);

                /// <inheritdoc cref="_Open"/>
                public void _OpenAndActivate(Scene scene);

            }

        }

        #endregion
        #region IMethods

        public SceneOperation Open() => !isOpen ? SceneManager.runtime.Open(this) : Error($"The scene '{name}' cannot be opened, as it is already open.");
        public SceneOperation OpenAndActivate() => !isOpen ? SceneManager.runtime.OpenAndActivate(this) : Error($"The scene '{name}' cannot be opened, as it is already open.");
        public SceneOperation ToggleOpen() => SceneManager.runtime.ToggleOpen(this);
        public SceneOperation Close() => isOpen ? SceneManager.runtime.Close(this) : Error($"The scene '{name}' cannot be closed, as it is not open.");
        public SceneOperation Preload(Action onPreloaded = null) => !isOpenInHierarchy ? SceneManager.runtime.Preload(this, onPreloaded) : Error($"The scene '{name}' cannot be preloaded, as it is already open.");
        public SceneOperation FinishPreload() => SceneManager.runtime.FinishPreload(this);
        public SceneOperation DiscardPreload() => isPreloaded ? SceneManager.runtime.DiscardPreload(this) : Error($"The scene '{name}' cannot be closed, as it is not open.");
        public SceneOperation OpenWithLoadingScreen(Scene loadingScreen) => !isOpen ? SceneManager.runtime.Open(this).With(loadingScreen) : Error($"The scene '{name}' cannot be opened, as it is already open.");
        public void SetActive() => SceneManager.runtime.SetActive(this);

        /// <inheritdoc cref="SetActive"/>
        public void Activate() => SceneManager.runtime.SetActive(this);

        SceneOperation Error(string message)
        {
            Debug.LogError(message);
            return SceneOperation.done;
        }

        #endregion
        #region IEvent

        public void _Open() => Open();
        public void _OpenAndActivate() => OpenAndActivate();
        public void _ToggleOpen() => ToggleOpen();
        public void _Close() => Close();
        public void _Preload() => Preload();
        public void _FinishPreload() => FinishPreload();
        public void _DiscardPreload() => DiscardPreload();
        public void _OpenWithLoadingScreen(Scene loadingScene) => OpenWithLoadingScreen(loadingScene);
        public void _SetActive() => SetActive();

        #endregion

        #endregion
        #region Find

        /// <summary>Gets 't:AdvancedSceneManager.Models.Scene', the string to use in <see cref="AssetDatabase.FindAssets(string)"/>.</summary>
        public readonly static string AssetSearchString = "t:" + typeof(Scene).FullName;

        /// <summary>Gets if <paramref name="q"/> matches <see cref="ASMModel.name"/>, <see cref="id"/>, <see cref="path"/>.</summary>
        public override bool IsMatch(string q) =>
            base.IsMatch(q) || IsPathMatch(q);

        bool IsPathMatch(string q) =>
            q == path;

        /// <inheritdoc cref="SceneUtility.Find(Func{Scene, bool}) Find"/>
        public static IEnumerable<Scene> Find(Func<Scene, bool> predicate) =>
            SceneUtility.Find(predicate);

        /// <inheritdoc cref="SceneUtility.Find(string)"/>
        public static Scene Find(string q) =>
            SceneUtility.Find(q);

        /// <inheritdoc cref="SceneUtility.Find(string)"/>
        public static bool TryFind(string q, out Scene scene) =>
            scene = Find(q);

        /// <inheritdoc cref="SceneUtility.FindOpen(string)"/>
        public static IEnumerable<Scene> FindOpen(string q) =>
            SceneUtility.FindOpen(q);

        /// <inheritdoc cref="SceneUtility.FindOpen(Func{Scene, bool})"/>
        public static IEnumerable<Scene> FindOpen(Func<Scene, bool> predicate) =>
            SceneUtility.FindOpen(predicate);

        #endregion
        #region Helpers

        /// <summary>Gets whatever this scene will be opened as persistent.</summary>
        /// <param name="parentCollection">Specifies the parent collection that was opened before <paramref name="finalCollection"/>.</param>
        /// <param name="collectionToOpen">Specifies the collection that will be opened, if you are not evaluating state after it would have opened, pass <see langword="null"/>. If multiple collections are opened in sequence, then pass the final one.</param>
        public bool EvalOpenAsPersistent(SceneCollection parentCollection, SceneCollection collectionToOpen = null) =>
            keepOpenWhenCollectionsClose ||
            (parentCollection && parentCollection.openAsPersistent) ||
            (keepOpenWhenNewCollectionWouldReopen && collectionToOpen && collectionToOpen.Contains(this));

#if UNITY_EDITOR

        internal bool CheckIfSpecialScene()
        {

            if (File.Exists(path))
            {

                var str = File.ReadAllText(path);
                var isSplashScreen = str.Contains("isSplashScreen: 1");
                var isLoadingScreen = str.Contains("isLoadingScreen: 1");

                if (this.isSplashScreen != isSplashScreen || this.isLoadingScreen != isLoadingScreen)
                {
                    this.isSplashScreen = isSplashScreen;
                    this.isLoadingScreen = isLoadingScreen;
                    return true;
                }

            }

            return false;

        }

#endif

        #endregion

        #endregion
        #region Scene loader

        [SerializeField] private string m_sceneLoader;

        /// <summary>Specifies what <see cref="SceneManagement.SceneLoader"/> to use.</summary>
        public string sceneLoader
        {
            get => m_sceneLoader;
            set { m_sceneLoader = value; OnPropertyChanged(); NotifyAddressables(); }
        }

        /// <summary>Specifies the scene loader to use for this scene.</summary>
        /// <remarks>If the specified scene loader is not registered when scene is opened, then ASM will fallback to other scene loaders, if any (normal ASM functionality is used if not).</remarks>
        public void SetSceneLoader<T>() where T : SceneLoader
        {
            sceneLoader = SceneLoader.GetKey<T>();
            NotifyAddressables();
        }

        void NotifyAddressables()
        {
#if ADDRESSABLES
            PackageSupport.Addressables.SceneExtensions.OnAddressablesChanged(this);
#endif
        }

        /// <summary>Gets the scene loader specified for this scene. <see langword="null"/> if none set.</summary>
        public Type GetSceneLoader() =>
            !string.IsNullOrWhiteSpace(sceneLoader)
            ? Type.GetType(sceneLoader, false)
            : null;

        /// <summary>Gets the effective, contextual, scene loader for this scene. <see langword="null"/> if none found (this means normal ASM loader will be used).</summary>
        public SceneLoader GetEffectiveSceneLoader() =>
            SceneManager.runtime.GetLoaderForScene(this);

        public bool UsesSceneLoader<T>() where T : SceneLoader =>
            GetSceneLoader() == typeof(T);

        /// <summary>Clears custom scene loader for this scene. This means normal ASM functionality will be used.</summary>
        public void ClearSceneLoader() =>
            sceneLoader = null;

        #endregion
        #region Netcode

#if NETCODE

        /// <summary>Gets or sets if this scene is enabled for netcode.</summary>
        public bool isNetcode
        {
            get => UsesSceneLoader<PackageSupport.Netcode.SceneLoader>();
            set { if (value) SetSceneLoader<PackageSupport.Netcode.SceneLoader>(); OnPropertyChanged(); }
        }

        /// <summary>Gets if this scene was synced using netcode.</summary>
        public bool isSynced { get; internal set; }

#endif

        #endregion
        #region Addressables

#if ADDRESSABLES

        /// <summary>Gets or sets if this scene is enabled for addressables.</summary>
        public bool isAddressable
        {
            get => UsesSceneLoader<PackageSupport.Addressables.SceneLoader>();
            set { if (value) SetSceneLoader<PackageSupport.Addressables.SceneLoader>(); OnPropertyChanged(); }
        }

        /// <summary>Gets the addressable address for this scene.</summary>
        public string address => isAddressable ? id : null;

#endif

        #endregion
        #region Equality

        public interface IEquality : IEquatable<Scene>, IEquatable<unityScene?>
#if UNITY_EDITOR
    , IEquatable<SceneAsset>
#endif
        { }

        public override int GetHashCode() => id?.GetHashCode() ?? 0;

        public override bool Equals(object obj) => IsEqual(obj, this);
        public bool Equals(Scene scene) => IsEqual(scene, this);
        public bool Equals(unityScene? scene) => IsEqual(scene, this);

        public static bool operator ==(Scene scene, Scene scene1) => IsEqual(scene, scene1);
        public static bool operator !=(Scene scene, Scene scene1) => !IsEqual(scene, scene1);

#if UNITY_EDITOR
        public bool Equals(SceneAsset scene) => IsEqual(scene, this);
        public static bool operator ==(Scene scene, SceneAsset sceneAsset) => IsEqual(scene, sceneAsset);
        public static bool operator !=(Scene scene, SceneAsset sceneAsset) => !IsEqual(scene, sceneAsset);
        public static bool operator ==(SceneAsset sceneAsset, Scene scene) => IsEqual(scene, sceneAsset);
        public static bool operator !=(SceneAsset sceneAsset, Scene scene) => !IsEqual(scene, sceneAsset);
#endif

        public static bool IsEqual(object left, object right) =>
            left is null && right is null ||
            (GetPath(left, out var l) &&
            GetPath(right, out var r) &&
            l == r);

        static bool GetPath(object obj, out string path)
        {

            if (obj is null)
                path = null;
            else if (obj is Scene scene && scene)
                path = scene.path;
            else if (obj is unityScene unityScene)
                path = unityScene.path;

#if UNITY_EDITOR
            else if (obj is SceneAsset sceneAsset && sceneAsset)
                path = AssetDatabase.GetAssetPath(sceneAsset);
#endif

            else if (obj is UnityEngine.Object o && !o)
                path = null;

            else if (obj is unityScene uScene)
                path = uScene.path;

            else
                path = null;

            return !string.IsNullOrEmpty(path);

        }

        #endregion
        #region Implicit

        #region Path

        public static implicit operator string(Scene scene) => scene ? scene.path : string.Empty;

        #endregion
        #region Unity scene

        public static implicit operator unityScene?(Scene scene) => scene ? scene.internalScene : default;
        public static implicit operator unityScene(Scene scene) => scene ? scene.internalScene ?? default : default;

        public static implicit operator Scene(unityScene scene) => scene.ASMScene();
        public static implicit operator Scene(unityScene? scene) => scene?.ASMScene();

        #endregion
        #region SceneAsset
#if UNITY_EDITOR

        public static implicit operator SceneAsset(Scene scene) => scene ? (SceneAsset)scene.sceneAsset : default;
        public static implicit operator Scene(SceneAsset scene) => scene ? scene.ASMScene() : default;

#endif
        #endregion

        #endregion
        #region ToString

        public override string ToString()
        {

            var persistentIndicator = "";
            if (keepOpenWhenCollectionsClose)
                persistentIndicator = " (keepOpenWhenCollectionsClose)";
            else if (keepOpenWhenNewCollectionWouldReopen)
                persistentIndicator = " (keepOpenWhenNewCollectionWouldReopen)";

            return $"{name}{persistentIndicator}";

        }

        #endregion

    }

}
