using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.ViewModel
{
    public class NamespaceTreeItem : TreeViewItem
    {
        public NamespaceModel NamespaceModel { get; set; }
        public NamespaceTreeItem(NamespaceModel namespaceModel) : base(namespaceModel.Name)
        {
            NamespaceModel = namespaceModel;
            TreeType = TreeTypeEnum.Namespace;
            Children = PrepareChildrenInstance();
        }

        public override ObservableCollection<TreeViewItem> PrepareChildrenInstance()
        {
            var ret = new ObservableCollection<TreeViewItem>();
            if (NamespaceModel.Types.Count() != 0 || NamespaceModel.Namespaces.Count() != 0)
                ret.Add(null);
            return ret;
        }

        public override void BuildMyself()
        {
            base.BuildMyself();
            foreach (var typeModel in NamespaceModel.Types)
            {
                Children.Add(new TypeTreeItem(typeModel));
            }

            foreach (var namespaceModel in NamespaceModel.Namespaces)
            {
                Children.Add(new NamespaceTreeItem(namespaceModel));
            }
        }
    }
}
