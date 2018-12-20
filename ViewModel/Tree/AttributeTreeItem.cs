using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Model.Enums;
using ViewModel.Enums;

namespace ViewModel.ViewModel
{
    public class AttributeTreeItem : TreeViewItem
    {
        public AttributeTreeItem(TypeModel attributeModel) : base(attributeModel.Name)
        {
            TreeType = TreeTypeEnum.Attribute;
        }
    }
}