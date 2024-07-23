using System.Linq;
using AdvancedSceneManager.Editor.UI.Interfaces;
using AdvancedSceneManager.Editor.UI.Utility;
using AdvancedSceneManager.Editor.Utility;
using AdvancedSceneManager.Models;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views.Popups
{

    class OverviewPopup : ViewModel, IPopup
    {

        bool isImportedExpanded
        {
            get => Get(false);
            set => Set(value);
        }

        bool isUnimportedExpanded
        {
            get => Get(false);
            set => Set(value);
        }

        bool isASMExpanded
        {
            get => Get(false);
            set => Set(value);
        }

        Foldout importedFoldout;
        Foldout unimportedFoldout;
        Foldout asmFoldout;

        public override void OnAdded()
        {

            importedFoldout = view.Q<Foldout>("foldout-imported");
            unimportedFoldout = view.Q<Foldout>("foldout-unimported");
            asmFoldout = view.Q<Foldout>("foldout-asm");

            Reload();
            SceneImportUtility.scenesChanged += Reload;

        }

        bool isReopen;
        public override void OnReopen() =>
            isReopen = true;

        public override void OnRemoved() =>
            isReopen = false;

        public override void OnWindowDisable()
        {
            isImportedExpanded = importedFoldout.value;
            isUnimportedExpanded = unimportedFoldout.value;
            isASMExpanded = asmFoldout.value;
        }

        void Reload()
        {

            importedFoldout.Clear();
            unimportedFoldout.Clear();
            asmFoldout.Clear();

            var asmScenes = SceneManager.assets.defaults.Enumerate().OrderBy(s => s.name).ToArray();
            var importedScenes = SceneManager.assets.scenes.Where(s => s).Except(asmScenes).OrderBy(s => s.name).ToArray();
            var unimportedScenes = SceneImportUtility.unimportedScenes.Select(AssetDatabase.LoadAssetAtPath<SceneAsset>).Where(s => s).OrderBy(s => s.name).ToArray();

            foreach (var scene in importedScenes)
                importedFoldout.Add(SceneField(scene));

            foreach (var scene in unimportedScenes)
                unimportedFoldout.Add(SceneField(scene));

            foreach (var scene in asmScenes)
                asmFoldout.Add(SceneField(scene));

            unimportedFoldout.SetVisible(unimportedScenes.Any());
            importedFoldout.SetVisible(importedScenes.Any());
            asmFoldout.SetVisible(asmScenes.Any());

            importedFoldout.value = isReopen ? isImportedExpanded : !unimportedScenes.Any();
            unimportedFoldout.value = isReopen ? isUnimportedExpanded : unimportedScenes.Any();
            asmFoldout.value = isReopen ? isASMExpanded : false;

        }

        VisualElement SceneField(Scene scene)
        {

            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;

            var element = new SceneField();
            element.style.marginBottom = 8;
            element.value = scene;
            element.Q(className: "unity-object-field__input").SetEnabled(false);
            element.style.flexGrow = 1;
            container.Add(element);

            if (!scene.isDefaultASMScene)
            {
                var button = new Button(() => SceneImportUtility.Unimport(scene)) { text = "Unimport..." };
                button.style.width = new StyleLength(StyleKeyword.Auto);
                container.Add(button);
            }

            return container;

        }

        VisualElement SceneField(SceneAsset scene)
        {

            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;

            var element = new ObjectField();
            element.value = scene;
            element.Q(className: "unity-object-field__input").SetEnabled(false);
            element.style.flexGrow = 1;

            container.Add(element);

            var button = new Button(() => SceneImportUtility.Import(AssetDatabase.GetAssetPath(scene))) { text = "Import..." };
            button.style.width = new StyleLength(StyleKeyword.Auto);
            container.Add(button);

            return container;

        }

    }

}
