﻿using System.Linq;
using AdvancedSceneManager.DependencyInjection;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.UI.Views;
using AdvancedSceneManager.Editor.Utility;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Layouts
{

    class MainView : VerticalLayout<IView>
    {

        public void OnCreateGUI()
        {
            LoadStyles();
            SetupUI();
        }

        public void OnWindowClose()
        {
            foreach (var view in viewHandler.views)
                view.Key.OnRemoved();
        }

        void LoadStyles()
        {

            var styles = AssetDatabaseUtility.FindAssets<StyleSheet>($"{SceneManager.package.folder}/Plugin/Editor");

            if (!styles.Any())
                Debug.LogError("Could not find any styles for the scene manager window. You may try re-importing or re-installing ASM.");

            foreach (var style in styles)
                rootVisualElement.styleSheets.Add(style);

        }

        void SetupUI()
        {

            viewHandler.mainLayout.SetWrapper(() =>
            {
                var scroll = new ScrollView().Expand();
                scroll.name = "RootScroll";
                scroll.contentContainer.Expand();
                return scroll;
            },
            scroll => scroll.contentContainer);

            viewHandler.mainLayout.Add<HeaderView>().NoShrink();
            viewHandler.mainLayout.Add<SearchView>().NoShrink();
            viewHandler.mainLayout.Add<CollectionView>().Expand().MinHeight(64);
            viewHandler.mainLayout.Add<SelectionView>().NoShrink();
            viewHandler.mainLayout.Add<UndoView>().NoShrink();
            viewHandler.mainLayout.Add<NotificationView>().NoShrink();
            viewHandler.mainLayout.Add<FooterView>().NoShrink();

            viewHandler.mainLayout.Add<PopupView>(rootVisualElement);
            viewHandler.mainLayout.Add<ProgressSpinnerView>(rootVisualElement);

            foreach (var view in viewHandler.views)
                view.Key.OnAdded();

            SetupScrollViews();

            DependencyInjectionUtility.GetService<NotificationView>().ReloadPersistentNotifications();

        }

        #region Scrollviews

        void SetupScrollViews()
        {

            var rootScroll = rootVisualElement.Q<ScrollView>();
            var collectionView = viewHandler.Get<CollectionView>();
            var collectionScroll = collectionView.view.Q<ScrollView>();

            rootScroll.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
            rootScroll.style.paddingLeft = 8;
            rootScroll.style.paddingTop = 4;
            rootScroll.style.paddingRight = 8;
            rootScroll.style.paddingBottom = 4;

            //Only show root scroll if collection scroll is clamped to its min height, 64px.
            UpdateScrollViews();
            rootVisualElement.RegisterCallback<GeometryChangedEvent>(e => UpdateScrollViews());

            void UpdateScrollViews()
            {
                var isClamped = Mathf.Approximately(collectionView.view.resolvedStyle.height, collectionView.view.style.minHeight.value.value);
                rootScroll.verticalScrollerVisibility = isClamped ? ScrollerVisibility.Auto : ScrollerVisibility.Hidden;
            }

        }

        #endregion

    }

}
