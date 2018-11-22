using System.Collections.ObjectModel;
using System.Linq;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.ViewModel
{
    public class FieldTreeItem : TreeViewItem
    {
        public FieldTreeItem(FieldModel fieldModel) : base(fieldModel.Name)
        {
            TreeType = TreeTypeEnum.Field;
            FieldModel = fieldModel;
            ResolveFullName();
            Children = PrepareChildrenInstance();
        }

        public sealed override ObservableCollection<TreeViewItem> PrepareChildrenInstance()
        {
            var ret = new ObservableCollection<TreeViewItem>();
            if (FieldModel.TypeModel != null || FieldModel.Attributes.Count() != 0)
            {
                ret.Add(null);
            }
            return ret;
        }

        public override void BuildMyself()
        {
            base.BuildMyself();
            if (FieldModel.TypeModel != null)
            {
                Children.Add(new TypeTreeItem(FieldModel.TypeModel));
            }

            foreach (var attribute in FieldModel.Attributes)
            {
                Children.Add(new AttributeTreeItem(attribute));
            }
        }

        private void ResolveFullName()
        {
            Name = FieldModel.Access + " " + FieldModel.TypeModel.Name + " " + Name;
        }

        public override string Name { get; set; }
        public FieldModel FieldModel { get; set; }
    }
}