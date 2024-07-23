using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Models;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views.Popups
{

    abstract class ListPopup<T> : ViewModel, IPopup where T : ASMModel
    {

        VisualTreeAsset m_listItem;
        VisualTreeAsset listItem => m_listItem ? m_listItem : m_listItem = viewHandler.FindViewAsset("ListItem", throwIfMissing: true);

        public abstract void OnAdd();
        public abstract void OnSelected(T item);

        public virtual void OnRemove(T item) { }
        public virtual void OnRename(T item) { }
        public virtual void OnDuplicate(T item) { }

        public virtual bool displayRenameButton { get; }
        public virtual bool displayRemoveButton { get; }
        public virtual bool displayDuplicateButton { get; }

        public abstract string noItemsText { get; }
        public abstract string headerText { get; }

        public abstract IEnumerable<T> items { get; }

        T[] list;

        VisualElement container;
        public override void OnAdded()
        {

            this.container = view;
            this.list = items.Where(o => o).ToArray();

            container.BindToSettings();

            container.Q<Label>("text-header").text = headerText;
            container.Q<Label>("text-no-items").text = noItemsText;

            container.Q<Button>("button-add").clicked += OnAdd;

            var list = container.Q<ListView>();

            list.makeItem = listItem.Instantiate;

            list.unbindItem = Unbind;
            list.bindItem = Bind;
            Reload();

        }

        public void Reload()
        {
            list = items.Where(o => o).ToArray();
            container.Q("text-no-items").SetVisible(!list.Any());
            container.Q<ListView>().itemsSource = list;
            container.Q<ListView>().Rebuild();
        }

        void Unbind(VisualElement element, int index)
        {

            var nameButton = element.Q<Button>("button-name");
            var menuButton = element.Q<Button>("button-menu");

            nameButton.UnregisterCallback<ClickEvent>(OnSelect);
            menuButton.UnregisterCallback<ClickEvent>(OnMenu);

            nameButton.userData = null;
            menuButton.userData = null;

        }

        #region Menu

        void OnMenu(ClickEvent e)
        {
            if (e.currentTarget is Button button && button.userData is T t)
                OpenMenu(button, t, e.pointerId);
        }

        PopupWindow currentMenu;

        void OpenMenu(Button placementTarget, T obj, int pointerID)
        {

            CloseMenu();

            var popup = new PopupWindow();
            currentMenu = popup;
            currentMenu.userData = placementTarget;

            SetupButtons();

            popup.Hide();

            rootVisualElement.Add(popup);

            SetupPosition();
            SetStyle();

            popup.Show();
            SetupClose();

            void SetupButtons()
            {

                if (displayRenameButton) popup.Add(new Button(() => CloseMenu(OnRename)) { text = "Rename", visible = displayRenameButton });
                if (displayDuplicateButton) popup.Add(new Button(() => CloseMenu(OnDuplicate)) { text = "Duplicate", visible = displayDuplicateButton });
                if (displayRemoveButton) popup.Add(new Button(() => CloseMenu(OnRemove)) { text = "Remove", visible = displayRemoveButton });

                void CloseMenu(Action<T> action)
                {
                    action.Invoke(obj);
                    this.CloseMenu();
                }

            }

            void SetStyle()
            {
                popup.style.borderTopLeftRadius = 3;
                popup.style.borderTopRightRadius = 3;
                popup.style.borderBottomRightRadius = 3;
                popup.style.borderBottomLeftRadius = 3;
                popup.Q("unity-content-container").style.paddingTop = 4;
            }

            void SetupPosition()
            {

                EventCallback<GeometryChangedEvent> callback;
                callback = new(e => UpdatePosition());

                rootVisualElement.RegisterCallback(callback);
                popup.RegisterCallback(callback);

                popup.RegisterCallbackOnce<DetachFromPanelEvent>(e => rootVisualElement.UnregisterCallback(callback));

                UpdatePosition();
                void UpdatePosition()
                {

                    var buttonWorldBound = placementTarget.worldBound;
                    var rootWorldBound = rootVisualElement.worldBound;
                    var popupX = buttonWorldBound.xMin + (buttonWorldBound.width / 2) - (popup.worldBound.width / 2);
                    var popupY = buttonWorldBound.yMin - rootWorldBound.yMin - popup.worldBound.height;

                    popup.style.position = Position.Absolute;
                    popup.style.left = popupX;
                    popup.style.top = popupY;

                }

            }

            void SetupClose()
            {

                popup.focusable = true;
                popup.Focus();

                // Capture pointer events on the root to detect clicks outside the popup
                rootVisualElement.RegisterCallback<PointerDownEvent>(e => CloseMenu());

                // Optionally, capture pointer events on the popup to prevent it from hiding itself
                popup.RegisterCallback<PointerDownEvent>(ev => ev.StopPropagation());

                view.Q<ScrollView>().RegisterCallback<PointerDownEvent>(e =>
                {
                    CloseMenu();
                }, TrickleDown.TrickleDown);

            }

        }

        void CloseMenu()
        {

            currentMenu?.RemoveFromHierarchy();
            currentMenu = null;

        }

        #endregion

        void OnSelect(ClickEvent e)
        {
            if (e.target is Button button && button.userData is T t)
                OnSelected(t);
        }

        void Bind(VisualElement element, int index)
        {

            var item = list.ElementAt(index);
            var nameButton = element.Q<Button>("button-name");
            var menuButton = element.Q<Button>("button-menu");

            nameButton.text = item.name;
            nameButton.RegisterCallback<ClickEvent>(OnSelect);
            menuButton.RegisterCallback<ClickEvent>(OnMenu);

            menuButton.SetVisible(displayRenameButton || displayDuplicateButton || displayRemoveButton);

            nameButton.userData = item;
            menuButton.userData = item;

            menuButton.Hide();

            var isHover = false;
            element.RegisterCallback<PointerEnterEvent>(e =>
            {
                isHover = true;
                if (currentMenu is null || currentMenu.userData != menuButton)
                    menuButton.Show();
            });

            element.RegisterCallback<PointerLeaveEvent>(e =>
            {
                if (currentMenu is null || currentMenu?.userData != menuButton)
                    menuButton.Hide();
                else
                {
                    currentMenu.RegisterCallbackOnce<DetachFromPanelEvent>(e =>
                    {
                        if (!isHover)
                            menuButton.Hide();
                    });
                }

                isHover = false;
            });

        }

    }

}
