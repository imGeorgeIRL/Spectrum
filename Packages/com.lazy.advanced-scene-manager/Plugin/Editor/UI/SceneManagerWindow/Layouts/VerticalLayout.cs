using AdvancedSceneManager.Editor.UI.Interfaces;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Layouts
{

    class VerticalLayout<T> : ViewLayout<T> where T : IView
    {

        public override TemplateContainer Add<TViewModel>(VisualElement parent = null)
        {

            var view = Instantiate<TViewModel>();
            view.name = typeof(TViewModel).Name;
            (parent ?? contentContainer ?? rootView)?.Add(view);

            return view;

        }

        public void Add(Button element) =>
             view.Add(element);

    }

}
