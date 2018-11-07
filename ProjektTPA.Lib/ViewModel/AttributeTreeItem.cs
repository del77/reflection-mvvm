using System.Collections.ObjectModel;
using System.Linq;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.ViewModel
{
    public class AttributeTreeItem : TreeViewItem
    {
        public AttributeTreeItem(TypeModel attributeModel) : base(attributeModel.Name)
        {
            TreeType = TreeTypeEnum.Attribute;
        }
    }
}