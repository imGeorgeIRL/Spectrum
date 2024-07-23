using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AdvancedSceneManager.Editor.Utility;
using AdvancedSceneManager.Models;
using AdvancedSceneManager.Utility;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

#if INPUTSYSTEM && ENABLE_INPUT_SYSTEM
using Scene = AdvancedSceneManager.Models.Scene;
#endif

namespace AdvancedSceneManager.Editor
{

#if UNITY_2023_1_OR_NEWER
    [UxmlElement]
#endif
    /// <summary>A <see cref="ObjectField"/> that only accepts <see cref="Scene"/>, with support for <see cref="SceneAsset"/> drag drop.</summary>
    public partial class SceneField : ObjectField, INotifyValueChanged<Scene>
    {

        public void SetObjectPickerEnabled(bool value) =>
            this.Q(className: "unity-base-field__input").SetEnabled(value);

        readonly Action buttonRefresh;

        public SceneField()
        {

            SceneOpenButtonsHelper.AddButtons(this, () => value, out buttonRefresh);

            allowSceneObjects = false;
            objectType = typeof(Scene);

            SetupDragDropTarget();
            SetupPointerEvents();
            SetupTooltip();

            RegisterCallback<ChangeEvent<Object>>(OnValueChanged);

        }

        void OnValueChanged(ChangeEvent<Object> e)
        {
            using var ev = ChangeEvent<Scene>.GetPooled(e.previousValue as Scene, e.newValue as Scene);
            ev.target = this;
            SendEvent(ev);
        }

        #region Value

        public new Scene value
        {
            get => base.value as Scene;
            set => base.value = value;
        }

        public override void SetValueWithoutNotify(Object newValue)
        {
            base.SetValueWithoutNotify(newValue);
            RefreshButtons();
        }

        public void SetValueWithoutNotify(Scene newValue)
        {

            if (value) value.PropertyChanged -= Value_PropertyChanged;
            if (newValue) newValue.PropertyChanged += Value_PropertyChanged;

            base.SetValueWithoutNotify(newValue);

            RefreshButtons();

        }

        void Value_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            RefreshButtons();

        public void RegisterValueChangedCallback(EventCallback<ChangeEvent<Scene>> callback) =>
            INotifyValueChangedExtensions.RegisterValueChangedCallback(this, callback);

        public void UnregisterValueChangedCallback(EventCallback<ChangeEvent<Scene>> callback) =>
            INotifyValueChangedExtensions.UnregisterValueChangedCallback(this, callback);

        #endregion
        #region Drag drop target

        void SetupDragDropTarget()
        {

            //This fixes a bug where dropping a scene one pixel above this element would result in null being assigned to this field
            var element = this.Q(className: "unity-object-field-display");
            if (element == null)
                return;

            element.RegisterCallback<DragUpdatedEvent>(DragUpdated, TrickleDown.TrickleDown);
            element.RegisterCallback<DragPerformEvent>(DragPerform, TrickleDown.TrickleDown);

            void DragUpdated(DragUpdatedEvent e)
            {

                if (!HasSceneAsset(out var scene))
                    return;

                e.StopImmediatePropagation();

                DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                DragAndDrop.AcceptDrag();

            }

            void DragPerform(DragPerformEvent e)
            {

                if (!HasSceneAsset(out var scene))
                    return;

                value = scene;

            }

            bool HasSceneAsset(out Scene asset)
            {
                var l = GetDragDropScenes().ToArray();
                asset = GetDragDropScenes().FirstOrDefault();
                return asset;
            }

        }

        public static IEnumerable<Scene> GetDragDropScenes() =>
            DragAndDrop.objectReferences.OfType<Scene>().Concat(
                DragAndDrop.objectReferences.
                OfType<SceneAsset>().
                Select(o => o.ASMScene())).
                NonNull().
                Distinct();

        #endregion
        #region Pointer events

        public delegate void OnClick(PointerDownEvent e);

        OnClick onClick;
        public void OnClickCallback(OnClick onClick) =>
            this.onClick = onClick;

        void SetupPointerEvents()
        {

            var element = this.Q(className: "unity-object-field__object");
            var clickCount = 0;

            element.RegisterCallback<PointerDownEvent>(PointerDown, TrickleDown.TrickleDown);
            element.RegisterCallback<PointerLeaveEvent>(PointerLeave);
            element.RegisterCallback<PointerUpEvent>(PointerUp, TrickleDown.TrickleDown);

            void PointerDown(PointerDownEvent e)
            {

                if (e.button != 0)
                    return;

                onClick?.Invoke(e);
                if (e.isImmediatePropagationStopped)
                    return;

                e.StopPropagation();

#if !UNITY_2023_1_OR_NEWER
                e.PreventDefault();
#endif

                clickCount = e.clickCount;
                element.CapturePointer(e.pointerId);

            }

            void PointerLeave(PointerLeaveEvent e)
            {

                if (e.isPropagationStopped)
                    return;

                if (clickCount == 1 && e.pressedButtons == 1)
                    StartDrag();

                clickCount = 0;
                element.ReleasePointer(e.pointerId);

            }

            void PointerUp(PointerUpEvent e)
            {

                if (clickCount == 0)
                    return;

                if (!value)
                    return;

                PingAsset();
                element.ReleasePointer(e.pointerId);

                clickCount = 0;

            }

        }

        void StartDrag()
        {
            DragAndDrop.PrepareStartDrag();
            DragAndDrop.objectReferences = new[] { value };
            DragAndDrop.StartDrag("Scene drag:" + value.name);
        }

        /// <summary>Pings the associated SceneAsset in project window.</summary>
        public void PingAsset()
        {
            var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(((Scene)value).path);
            EditorGUIUtility.PingObject(asset);
        }

        /// <summary>Opens the associated SceneAsset.</summary>
        public void OpenAsset()
        {
            var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(((Scene)value).path);
            _ = AssetDatabase.OpenAsset(asset);
            Selection.activeObject = asset;
        }

        #endregion
        #region Open buttons

        void RefreshButtons()
        {
            buttonRefresh?.Invoke();
            SetupTooltip();
        }

        #endregion
        #region Tooltip

        const string InGameToolbarTooltip = "The in-game toolbar can help with diagnosing scene state during builds.";
        const string PauseScreenTooltip = "The pause scene provides a default pause screen, when your game has none yet.";

        string GetTooltip() =>
            string.Join("\n\n", new[]
            {
                GetDefaultSceneInfo(),
                GetBindingInfo(),
                GetPersistentInfo(),
                GetStartupInfo(),
                GetPath()
            }.OfType<string>());

        string GetDefaultSceneInfo()
        {
            if (value == SceneManager.assets.defaults.inGameToolbarScene)
                return InGameToolbarTooltip;
            else if (value == SceneManager.assets.defaults.pauseScene)
                return PauseScreenTooltip;
            else
                return null;
        }

        string GetBindingInfo()
        {

#if INPUTSYSTEM && ENABLE_INPUT_SYSTEM

            if (!Profile.current)
                return null;

            var scene = Profile.current.standaloneScenes.sceneBindings.FirstOrDefault(s => s.scene == value);
            if (!scene.scene || !scene.binding.isValid)
                return null;

            var input = string.Join(" + ", scene.binding.buttons.Select(b => b.name));
            if (scene.binding.interactionType == InputBindingInteractionType.Open)
                return $"Will open when {input} is pressed.";
            else if (scene.binding.interactionType == InputBindingInteractionType.Hold)
                return $"Will open when {input} is held down.";
            else if (scene.binding.interactionType == InputBindingInteractionType.Toggle)
                return $"Will toggle when {input} is pressed.";

#endif

            return null;

        }

        string GetPersistentInfo() =>
            value && value.keepOpenWhenCollectionsClose ? "Will stay open when collections close." : null;

        string GetStartupInfo() =>
            value && value.openOnStartup ? "Will open during startup." : null;

        string GetPath() =>
            value ? value.path : null;

        void SetupTooltip() =>
            tooltip = GetTooltip();

        #endregion
        #region 2022 and below

#if !UNITY_2023_1_OR_NEWER

        public new class UxmlFactory : UxmlFactory<SceneField, UxmlTraits>
        {

            public override string uxmlNamespace => "AdvancedSceneManager";

        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {

            private readonly UxmlStringAttributeDescription m_propertyPath = new() { name = "Binding-path" };
            private readonly UxmlStringAttributeDescription m_label = new() { name = "Label" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {

                base.Init(ve, bag, cc);

                if (ve is SceneField field)
                {
                    field.label = m_label.GetValueFromBag(bag, cc);
                    field.bindingPath = m_propertyPath.GetValueFromBag(bag, cc);
                }

            }

        }

#endif

        #endregion


    }

}
