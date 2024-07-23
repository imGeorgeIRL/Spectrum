using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedSceneManager.DependencyInjection;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.Utility;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI
{

    class ViewHandler
    {

        public ViewHandler(ViewLayout<IView> mainLayout, ViewLayout<IPopup> popupLayout, ViewLayout<ISettingsPage> settingsLayout)
        {
            this.mainLayout = mainLayout;
            this.popupLayout = popupLayout;
            this.settingsLayout = settingsLayout;
        }

        public readonly Dictionary<IView, VisualTreeAsset> all = new();

        public readonly Dictionary<IView, VisualTreeAsset> views = new();
        public readonly Dictionary<IPopup, VisualTreeAsset> popups = new();
        public readonly Dictionary<ISettingsPage, VisualTreeAsset> settingsPages = new();

        public readonly Dictionary<IPersistentNotification, VisualTreeAsset> notifications = new(); //VisualTreeAsset unused

        public VisualElement rootVisualElement { get; }

        public ViewLayout<IView> mainLayout { get; }
        public ViewLayout<IPopup> popupLayout { get; }
        public ViewLayout<ISettingsPage> settingsLayout { get; }

        #region Add

        public TViewModel AddView<TViewModel>(VisualTreeAsset view = null, bool requireView = true) where TViewModel : IView =>
            Add<IView, TViewModel>(views, view, requireView);

        public TViewModel AddSettings<TViewModel>(VisualTreeAsset view = null) where TViewModel : ISettingsPage =>
            Add<ISettingsPage, TViewModel>(settingsPages, view);

        public TViewModel AddPopup<TViewModel>(VisualTreeAsset view = null) where TViewModel : IPopup =>
            Add<IPopup, TViewModel>(popups, view);

        public TViewModel AddNotification<TViewModel>() where TViewModel : IPersistentNotification =>
            Add<IPersistentNotification, TViewModel>(notifications, requireView: false);

        public TNotification AddNotification<TNotification, TPopup>(VisualTreeAsset view = null) where TNotification : IPersistentNotification where TPopup : IPopup
        {
            AddPopup<TPopup>(view);
            return AddNotification<TNotification>();
        }

        TImplementation Add<TInterface, TImplementation>(Dictionary<TInterface, VisualTreeAsset> list, VisualTreeAsset view = null, bool requireView = true) where TInterface : IView where TImplementation : TInterface
        {

            var viewModel = DependencyInjectionUtility.Construct<TImplementation>() ??
                throw new ArgumentException($"Could not construct view model {typeof(TImplementation).Name}.");

            if (requireView)
                FindViewAsset<TImplementation>(ref view, true);

            all.Add(viewModel, view);
            list.Add(viewModel, view);
            DependencyInjectionUtility.Add(typeof(TInterface), viewModel);
            return viewModel;

        }

        #endregion
        #region Get view model

        public TViewModel Get<TViewModel>() where TViewModel : DependencyInjectionUtility.IInjectable =>
            (TViewModel)all.Keys.FirstOrDefault(viewModel => typeof(TViewModel).IsAssignableFrom(viewModel.GetType()));

        public IView Get(Type type) =>
            all.Keys.FirstOrDefault(viewModel => type?.IsAssignableFrom(viewModel.GetType()) ?? false);

        #endregion
        #region Get view

        public VisualTreeAsset GetView<TViewModel>() where TViewModel : IView
        {
            var key = Get<TViewModel>();
            return key is not null ? all.GetValueOrDefault(key) : null;
        }

        #endregion
        #region Find view assets

        VisualTreeAsset[] assets;

        public void CacheViewAssets()
        {
            var path = $"{SceneManager.package.folder}/Plugin/Editor/UI/SceneManagerWindow";
            assets = AssetDatabaseUtility.FindAssets<VisualTreeAsset>(path).ToArray();
        }

        public void UncacheViewAssets()
        {
            assets = null;
        }

        bool FindViewAsset<T>(ref VisualTreeAsset view, bool throwIfMissing = false)
        {

            if (view)
                return true;

            return FindViewAsset(typeof(T).Name, out view, throwIfMissing);

        }

        public VisualTreeAsset FindViewAsset(string name, bool throwIfMissing = false)
        {
            FindViewAsset(name, out var asset, throwIfMissing);
            return asset;
        }

        public bool FindViewAsset(string name, out VisualTreeAsset view, bool throwIfMissing = false)
        {

            var path = $"{SceneManager.package.folder}/Plugin/Editor/UI/SceneManagerWindow";
            view = (assets ?? AssetDatabaseUtility.FindAssets<VisualTreeAsset>(path)).FirstOrDefault(asset => asset.name == name);

            if (throwIfMissing && !view)
                throw new ArgumentException($"Could not find view '{name}'.");

            return view;

        }

        #endregion

    }

}
