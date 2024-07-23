using System.Collections.Generic;
using System.Linq;
using AdvancedSceneManager.Editor.Utility;
using AdvancedSceneManager.Models;
using AdvancedSceneManager.Models.Utility;
using UnityEditor;
using UnityEngine.UIElements;

namespace AdvancedSceneManager.Editor.UI.Views.Popups
{

    class ExtraCollectionPopup : ListPopup<SceneCollectionTemplate>
    {

        public override string noItemsText { get; } = "No templates, you can create one using + button.";
        public override string headerText { get; } = "Collection templates";
        public override IEnumerable<SceneCollectionTemplate> items => SceneManager.assets.templates;

        public override bool displayRenameButton => true;
        public override bool displayRemoveButton => true;

        public override void OnAdded()
        {

            base.OnAdded();

            var group = new GroupBox();
            var button = new Button(CreateDynamicCollection) { text = "Create dynamic collection" };
            group.Add(button);
            view.Q<ScrollView>().Insert(0, group);

        }

        void CreateDynamicCollection()
        {
            Profile.current.CreateDynamicCollection();
            collectionView.Reload();
        }

        public override void OnAdd()
        {

            pickNamePopup.Prompt(value =>
            {
                SceneCollectionTemplate.CreateTemplate(value);
                OpenPopup<ExtraCollectionPopup>();
            },
            onCancel: OpenPopup<ProfilePopup>);

        }

        public override void OnSelected(SceneCollectionTemplate template)
        {
            Profile.current.CreateCollection(template);
            ClosePopup();
        }

        public override void OnRename(SceneCollectionTemplate template)
        {

            pickNamePopup.Prompt(
                value: template.title,
                onContinue: value =>
                {
                    template.m_title = value;
                    template.Rename(value);
                    OpenPopup<ExtraCollectionPopup>();
                },
                onCancel: OpenPopup<ExtraCollectionPopup>);

        }

        public override void OnRemove(SceneCollectionTemplate template)
        {

            if (PromptUtility.PromptDelete("template"))
            {

                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(template));

                if (!items.Where(o => o).Any())
                    ClosePopup();
                else
                    Reload();

            }

        }

    }

}
