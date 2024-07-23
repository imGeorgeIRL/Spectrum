using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Utility
{

    static class VisualElementUtility
    {

        public static T OnChecked<T>(this T button, Action callback) where T : BaseBoolField
        {
            _ = button.RegisterValueChangedCallback(e => { if (e.newValue) callback.Invoke(); });
            return button;
        }

        public static T OnUnchecked<T>(this T button, Action callback) where T : BaseBoolField
        {
            _ = button.RegisterValueChangedCallback(e => { if (!e.newValue) callback.Invoke(); });
            return button;
        }

        public static T SetChecked<T>(this T button, bool isChecked = true) where T : BaseBoolField
        {
            button.SetValueWithoutNotify(isChecked);
            return button;
        }

        public static void Hide(this VisualElement element) =>
            element.style.display = DisplayStyle.None;

        public static void Show(this VisualElement element) =>
            element.style.display = DisplayStyle.Flex;

        public static void SetVisible(this VisualElement element, bool visible) =>
            element.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;

        public static T Expand<T>(this T element) where T : VisualElement
        {
            element.style.flexGrow = 1;
            return element;
        }

        public static T NoExpand<T>(this T element) where T : VisualElement
        {
            element.style.flexGrow = 0;
            return element;
        }

        public static T Shrink<T>(this T element) where T : VisualElement
        {
            element.style.flexShrink = 1;
            return element;
        }

        public static T NoShrink<T>(this T element) where T : VisualElement
        {
            element.style.flexShrink = 0;
            return element;
        }

        public static T MinHeight<T>(this T element, float height) where T : VisualElement
        {
            element.style.minHeight = height;
            return element;
        }

        public static bool IsShiftKeyHeld(this EventBase e)
        {
            if (e is PointerUpEvent pointer)
                return pointer.shiftKey;
            else if (e is MouseUpEvent mouse)
                return mouse.shiftKey;
            return false;
        }

        public static bool IsCtrlKeyHeld(this EventBase e)
        {
            if (e is PointerUpEvent pointer)
                return pointer.ctrlKey;
            else if (e is MouseUpEvent mouse)
                return mouse.ctrlKey;
            return false;
        }

        public static bool IsCommandKeyHeld(this EventBase e)
        {
            if (e is PointerUpEvent pointer)
                return pointer.commandKey;
            else if (e is MouseUpEvent mouse)
                return mouse.commandKey;
            return false;
        }

        #region RegisterCallbackOnce

#if !UNITY_6000_0_OR_NEWER
        public static void RegisterCallbackOnce<TEventType>(this VisualElement element, EventCallback<TEventType> callback) where TEventType : EventBase<TEventType>, new()
        {

            void Wrapper(TEventType e)
            {
                element.UnregisterCallback<TEventType>(Wrapper);
                callback(e);
            }

            element.RegisterCallback<TEventType>(Wrapper);

        }
#endif

        #endregion
        #region GetAncestor

        public static VisualElement GetAncestor(this VisualElement element, string name = null, string className = null) =>
            GetAncestor<VisualElement>(element, name, className);

        public static T GetAncestor<T>(this VisualElement element, string name = null, string className = null) where T : VisualElement
        {

            if (element == null)
                return null;

            if (element is T && ((string.IsNullOrEmpty(name) || element.name == name) || (string.IsNullOrEmpty(className) || element.ClassListContains(className))))
                return (T)element;

            return element.parent.GetAncestor<T>(className, name);

        }

        #endregion
        #region Rotate animation

        public static IVisualElementScheduledItem Rotate(this VisualElement element, long tick = 10, int speed = 15) =>
            element.schedule.
            Execute(() => element.style.rotate = new Rotate(new(element.style.rotate.value.angle.value + speed))).
            Every(tick);

        #endregion
        #region Fade animation

        /// <summary>Fades the element.</summary>
        /// <remarks>Uses in-out easing.</remarks>
        public static IVisualElementScheduledItem Fade(this VisualElement view, float to, float duration = 0.25f, Action onComplete = null)
        {

            var initialOpacity = view.style.opacity.value;
            var elapsed = 0f;
            var interval = 0.01f; // Interval for the updates (10ms)

            return view.schedule.Execute(() =>
            {

                elapsed += interval;
                var t = Mathf.Clamp01(elapsed / duration); // Normalized time [0, 1]

                // Ease-in-out function
                var easedT = t * t * (3f - 2f * t);

                view.style.opacity = new StyleFloat(Mathf.Lerp(initialOpacity, to, easedT));

                if (elapsed >= duration)
                {
                    view.style.opacity = new StyleFloat(to); // Ensure final value is set
                    onComplete?.Invoke();
                }

            }).Every((long)(interval * 1000)).Until(() => elapsed >= duration);

        }

        #endregion
        #region Slide animation

        public static IVisualElementScheduledItem AnimateBottom(this VisualElement view, float from, float to, float duration = 0.25f, Action onComplete = null)
        {

            var elapsed = 0f;
            var interval = 0.01f; // Interval for the updates (10ms)

            return view.schedule.Execute(() =>
            {

                elapsed += interval;
                var t = Mathf.Clamp01(elapsed / duration); // Normalized time [0, 1]

                // Ease-in-out function
                var easedT = t * t * (3f - 2f * t);

                view.style.bottom = new StyleLength(Mathf.Lerp(from, to, easedT));

                if (elapsed >= duration)
                {
                    view.style.bottom = new StyleLength(to); // Ensure final value is set
                    onComplete?.Invoke();
                }

            }).Every((long)(interval * 1000)).Until(() => elapsed >= duration);

        }

        public static IVisualElementScheduledItem AnimateLeft(this VisualElement view, float from, float to, float duration = 0.25f, LengthUnit unit = LengthUnit.Pixel, Action onComplete = null)
        {

            var elapsed = 0f;
            var interval = 0.01f; // Interval for the updates (10ms)

            return view.schedule.Execute(() =>
            {

                elapsed += interval;
                var t = Mathf.Clamp01(elapsed / duration); // Normalized time [0, 1]

                // Ease-in-out function
                var easedT = t * t * (3f - 2f * t);

                view.style.left = new StyleLength(new Length(Mathf.Lerp(from, to, easedT), unit));

                if (elapsed >= duration)
                {
                    view.style.left = new StyleLength(new Length(to, unit)); // Ensure final value is set
                    onComplete?.Invoke();
                }

            }).Every((long)(interval * 1000)).Until(() => elapsed >= duration);

        }

        public static IVisualElementScheduledItem AnimateHeight(this VisualElement view, float to, float duration = 0.25f, LengthUnit unit = LengthUnit.Pixel, Action onComplete = null)
        {

            var from = view.localBound.height;
            var elapsed = 0f;
            var interval = 0.01f; // Interval for the updates (10ms)

            return view.schedule.Execute(() =>
            {

                elapsed += interval;
                var t = Mathf.Clamp01(elapsed / duration); // Normalized time [0, 1]

                // Ease-in-out function
                var easedT = t * t * (3f - 2f * t);

                view.style.height = new StyleLength(new Length(Mathf.Lerp(from, to, easedT), unit));

                if (elapsed >= duration)
                {
                    view.style.height = new StyleLength(new Length(to, unit)); // Ensure final value is set
                    onComplete?.Invoke();
                }

            }).Every((long)(interval * 1000)).Until(() => elapsed >= duration);

        }

        public static IVisualElementScheduledItem AnimateWidth(this VisualElement view, float to, float duration = 0.25f, LengthUnit unit = LengthUnit.Pixel, Action onComplete = null)
        {

            var from = view.localBound.width;
            var elapsed = 0f;
            var interval = 0.01f; // Interval for the updates (10ms)

            return view.schedule.Execute(() =>
            {

                elapsed += interval;
                var t = Mathf.Clamp01(elapsed / duration); // Normalized time [0, 1]

                // Ease-in-out function
                var easedT = t * t * (3f - 2f * t);

                view.style.width = new StyleLength(new Length(Mathf.Lerp(from, to, easedT), unit));

                if (elapsed >= duration)
                {
                    view.style.width = new StyleLength(new Length(to, unit)); // Ensure final value is set
                    onComplete?.Invoke();
                }

            }).Every((long)(interval * 1000)).Until(() => elapsed >= duration);

        }

        #endregion
        #region IVisualElementScheduledItem awaiter

        public static Task AsTask(this IVisualElementScheduledItem scheduledItem)
        {
            var tcs = new TaskCompletionSource<bool>();
            scheduledItem.GetAwaiter().OnCompleted(() => tcs.SetResult(true));
            return tcs.Task;
        }

        public static VisualElementScheduledItemAwaiter GetAwaiter(this IVisualElementScheduledItem scheduledItem)
        {
            return new VisualElementScheduledItemAwaiter(scheduledItem);
        }

        public class VisualElementScheduledItemAwaiter : INotifyCompletion
        {
            private readonly IVisualElementScheduledItem _scheduledItem;
            private Action _continuation;

            public VisualElementScheduledItemAwaiter(IVisualElementScheduledItem scheduledItem)
            {
                _scheduledItem = scheduledItem;
                EditorApplication.update += OnEditorUpdate;
            }

            public bool IsCompleted => !_scheduledItem.isActive;

            public void GetResult() { }

            public void OnCompleted(Action continuation)
            {
                _continuation = continuation;
            }

            private void OnEditorUpdate()
            {
                if (!_scheduledItem.isActive)
                {
                    EditorApplication.update -= OnEditorUpdate;
                    _continuation?.Invoke();
                }
            }
        }

        #endregion

        public static async void Animate(Action onComplete, params IVisualElementScheduledItem[] animations)
        {

            //Make sure animations run smoothly by repainting every frame
            SceneManagerWindow.window.wantsConstantRepaint = true;

            await Task.WhenAll(animations.Select(animation => animation.AsTask()));

            EditorApplication.delayCall += () =>
            {

                if (animations.Any(a => a.isActive))
                    return;

                //Repainting every frame is expensive, lets disable in once animations are done
                SceneManagerWindow.window.wantsConstantRepaint = false;

                onComplete?.Invoke();

            };

        }

    }

}
