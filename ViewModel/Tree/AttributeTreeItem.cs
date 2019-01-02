using BusinessLogic.Model;
using ViewModel.Base;
using ViewModel.Enums;

namespace ViewModel.Tree
{
    public class AttributeTreeItem : TreeViewItem
    {
        public AttributeTreeItem(TypeModel attributeModel) : base(attributeModel.Name)
        {
            TreeType = TreeTypeEnum.Attribute;
        }
    }
}