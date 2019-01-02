using System.Collections.ObjectModel;
using BusinessLogic.Model;
using ViewModel.Base;
using ViewModel.Enums;

namespace ViewModel.Tree
{
    public class ParameterTreeItem : TreeViewItem
    {
        public ParameterTreeItem(FieldModel fieldModel) : base(fieldModel.ToString())
        {
            TreeType = TreeTypeEnum.Parameter;
            FieldModel = fieldModel;
            //ResolveFullName();
            Children = PrepareChildrenInstance();
        }

        public sealed override ObservableCollection<TreeViewItem> PrepareChildrenInstance()
        {
            var ret = new ObservableCollection<TreeViewItem>();
            if(FieldModel.TypeModel != null)
                ret.Add(null);
            return ret;

        }

        public override void BuildMyself()
        {
            base.BuildMyself();
            Children.Add(new TypeTreeItem(FieldModel.TypeModel));
        }

        private void ResolveFullName()
        {
           // Name = FieldModel.TypeModel.Name + " " + Name;
        }

        public FieldModel FieldModel { get; set; }
        public override string Name { get; set; }
    }
}