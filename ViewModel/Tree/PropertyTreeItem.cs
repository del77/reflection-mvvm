using System.Collections.ObjectModel;
using BusinessLogic.Model;
using ViewModel.Base;
using ViewModel.Enums;

namespace ViewModel.Tree
{
    public class PropertyTreeItem : TreeViewItem
    {
        public PropertyTreeItem(PropertyModel propertyModel) : base(propertyModel.ToString())
        {
            TreeType = TreeTypeEnum.Property;
            Children = PrepareChildrenInstance();
            PropertyModel = propertyModel;
            //ResolveFullName();
        }

        private void ResolveFullName()
        {
            //Name = PropertyModel.Access + " " + PropertyModel.Type.Name + " " + Name;
        }

        private PropertyModel PropertyModel { get; set; }
        public override string Name { get; set; }

        public sealed override ObservableCollection<TreeViewItem> PrepareChildrenInstance()
        {
            var ret = new ObservableCollection<TreeViewItem>() {null};
            return ret;
        }

        public override void BuildMyself()
        {
            base.BuildMyself();
            if(PropertyModel.Attributes != null)
            {
                foreach (var attribute in PropertyModel.Attributes)
                {
                    Children.Add(new AttributeTreeItem(attribute));
                }
            }

            if (PropertyModel.Getter != null)
            {
                Children.Add(new MethodTreeItem(PropertyModel.Getter));
            }

            if (PropertyModel.Setter != null)
            {
                Children.Add(new MethodTreeItem(PropertyModel.Setter));
            }

            if (PropertyModel.Type != null)
            {
                Children.Add(new TypeTreeItem(PropertyModel.Type));
            }

            
        }

        
    }
}