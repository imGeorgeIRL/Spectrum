using System;
using System.Collections.Generic;
using AdvancedSceneManager.DependencyInjection;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Layouts;
using AdvancedSceneManager.Editor.Utility;
using AdvancedSceneManager.Legacy;
using AdvancedSceneManager.Models;
using UnityEditor;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views
{

    class NotificationView : VerticalLayout<INotification>
    {

        public override void OnAdded()
        {
            SceneImportUtility.scenesChanged += ReloadPersistentNotifications;
            Profile.onProfileChanged += ReloadPersistentNotifications;
        }

        public override void OnRemoved()
        {
            SceneImportUtility.scenesChanged -= ReloadPersistentNotifications;
            Profile.onProfileChanged -= ReloadPersistentNotifications;
        }

        public override void OnWindowFocus() =>
            ReloadPersistentNotifications();

        #region Persistent notifications

        IEnumerable<IPersistentNotification> persistentNotifications => DependencyInjectionUtility.GetServices<IPersistentNotification>();

        public void ReloadPersistentNotifications()
        {
            foreach (var notification in persistentNotifications)
                notification.ReloadNotification();
        }

        #endregion

        readonly new Dictionary<string, VisualElement> notifications = new();

        public string Notify(string message, Action onClick, Action onDismiss = null, bool canDismiss = true)
        {

            var id = GUID.Generate().ToString();
            Notify(message, id, onClick, onDismiss, canDismiss);

            return id;

        }

        public void Notify(string message, string id, Action onClick, Action onDismiss = null, bool canDismiss = true, bool dismissOnClick = true, bool priority = false)
        {

            ClearNotification(id);

            if (!priority && LegacyUtility.FindAssets())
                return;

            Button element = null;
            element = new Button(() => Dismiss(onClick, dismissOnClick));

            element.Add(new Label(message));
            var spacer = new VisualElement();
            spacer.AddToClassList("spacer");
            element.Add(spacer);

            if (canDismiss)
            {
                var button = new Button(() => Dismiss(onDismiss, true)) { text = "x" };
                button.style.marginLeft = 8;
                element.Add(button);
            }

            element.AddToClassList("notification");
            Add(element);

            notifications.Add(id, element);

            void Dismiss(Action callback, bool canDismiss)
            {
                if (canDismiss)
                    element.RemoveFromHierarchy();
                callback?.Invoke();
            }

        }

        public void ClearNotification(string id)
        {
            if (notifications.Remove(id, out var element))
                element.RemoveFromHierarchy();
        }

    }

}
