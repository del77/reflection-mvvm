using System.Collections.ObjectModel;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.ViewModel
{
    public class PropertyTreeItem : TreeViewItem
    {
        public PropertyTreeItem(PropertyModel propertyModel) : base(propertyModel.Name)
        {
            TreeType = TreeTypeEnum.Property;
            Children = PrepareChildrenInstance();
            PropertyModel = propertyModel;
            ResolveFullName();
        }

        private void ResolveFullName()
        {
            Name = PropertyModel.Access + " " + PropertyModel.Type.Name + " " + Name;
        }

        private PropertyModel PropertyModel { get; set; }

        public override ObservableCollection<TreeViewItem> PrepareChildrenInstance()
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