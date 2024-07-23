﻿using System;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.UI.Views.Settings;
using AdvancedSceneManager.Models;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Layouts
{

    class SettingsView : PopupLayout<ISettingsPage>, IPopup
    {

        VisualElement mainPage;
        VisualElement settingPage;
        VisualElement pageView;

        ScrollView mainScroll;
        ScrollView pageScroll;

        public override VisualElement contentContainer => pageScroll?.contentContainer;

        public override void OnAdded()
        {

            DisableTemplateContainer();

            //Not entirely sure why this is needed, as popupView.contentContainer is correct, and is correct before instantiating view.
            //Must be a bug in VisualElement.Add(VisualElement parent) where it sometimes ignores parent?
            view.RemoveFromHierarchy();
            popupView.rootView.Add(view);

            mainPage = view.Q("page-main");
            settingPage = view.Q("page-settings");

            mainScroll = mainPage.Q<ScrollView>();
            pageScroll = settingPage.Q<ScrollView>();

            SetupMainPage();

            settingPage.Q<Button>("button-back").clickable = new(settingsView.ClosePage);

            base.OnAdded();

            pageScroll.verticalScroller.value = 0;

        }

        public override void OnRemoved()
        {

            currentPopup = null;
            mainPage = null;
            settingPage = null;
            pageView = null;
            lastPage = null;

            if (Profile.current)
                Profile.current.Save();

            ASMSettings.instance.Save();
            ASMUserSettings.instance.Save();

        }

        public void Open()
        {

            if (popupView.currentPopup != this)
                popupView.Open<SettingsView>();

        }

        public override void Open<TPopup>(object parameter = null)
        {
            Open();
            OpenInternal<TPopup>(animate: true);
        }

        public override void Reopen<TPopup>() =>
            OpenInternal<TPopup>(animate: false);

        public override void ClosePopup(Action onComplete = null)
        {
            popupView.ClosePopup(onComplete);
        }

        #region Main page

        void SetupMainPage()
        {

            mainPage.Q<Button>("button-startup").clickable = new(OpenSettings<StartupPage>);
            mainPage.Q<Button>("button-scene-loading").clickable = new(OpenSettings<SceneLoadingPage>);
            mainPage.Q<Button>("button-assets").clickable = new(OpenSettings<AssetsPage>);
            mainPage.Q<Button>("button-appearance").clickable = new(OpenSettings<AppearancePage>);
            mainPage.Q<Button>("button-editor").clickable = new(OpenSettings<EditorPage>);

            var netcodeButton = mainPage.Q<Button>("button-network");
            netcodeButton.SetVisible(false);

#if NETCODE
            netcodeButton.clickable = new(OpenSettings<NetworkPage>);
            netcodeButton.SetVisible(true);
#endif

        }

        #endregion
        #region Open / close settings page

        Type lastPage;

        void OpenInternal<TPopup>(bool animate, Action onComplete = null) where TPopup : ISettingsPage
        {

            var viewModel = viewHandler.Get<TPopup>();

            //Instantiate settings page
            pageView = Add<TPopup>(settingPage.Q<ScrollView>().contentContainer);
            viewModel.SetView(pageView);
            viewModel.OnAdded();

            pageView.RemoveFromHierarchy();
            contentContainer.Add(pageView);

            currentPopup = viewModel;

            UpdateHeader(viewModel);

            mainPage.style.left = new StyleLength(new Length(-104, LengthUnit.Percent));
            settingPage.style.left = new StyleLength(new Length(-101.5f, LengthUnit.Percent));

            if (animate)
                AnimateOpenPage(onComplete);
            else
                onComplete?.Invoke();

            //Reset scroll unless same page as earlier
            if (lastPage != typeof(TPopup))
                pageScroll.verticalScroller.value = 0;
            lastPage = typeof(TPopup);

        }

        void UpdateHeader<TPopup>(TPopup viewModel) where TPopup : ISettingsPage =>
            settingPage.Q<Label>("label-header").text = viewModel.Header;

        public void ClosePage()
        {

            AnimateClosePage(onComplete: () =>
            {
                currentPopup.OnRemoved();
                currentPopup.view?.RemoveFromHierarchy();
                currentPopup.SetView(null);
                currentPopup = null;
                settingPage.Q<ScrollView>().contentContainer.Clear();
            });

        }

        #endregion
        #region Animations

        void AnimateOpenPage(Action onComplete) =>
            VisualElementUtility.Animate(
                onComplete,
                mainPage?.AnimateLeft(0, -104, unit: LengthUnit.Percent),
                mainPage?.Fade(0, 0.3f),
                settingPage?.AnimateLeft(0, -101.5f, unit: LengthUnit.Percent),
                settingPage?.Fade(1, 0.3f));

        void AnimateClosePage(Action onComplete) =>
           VisualElementUtility.Animate(
                onComplete,
                mainPage?.AnimateLeft(-104, 0, unit: LengthUnit.Percent),
                mainPage?.Fade(1, 0.3f),
                settingPage?.AnimateLeft(-101.5f, 0, unit: LengthUnit.Percent),
                settingPage?.Fade(0, 0.15f),
                pageView.AnimateHeight(0),
                pageView.AnimateWidth(0, 0.3f));

        #endregion

    }

}
