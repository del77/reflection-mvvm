using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.ViewModel
{
    public class AssemblyTreeItem : TreeViewItem
    {
        public AssemblyModel AssemblyModel { get; set; }
        public AssemblyTreeItem(AssemblyModel assembly) : base(assembly.Name)
        {
            //NamespacesModels = assembly.NamespaceModels;
            TreeType = TreeTypeEnum.Assembly;
            AssemblyModel = assembly;
            Children = PrepareChildrenInstance();
        }

        public override ObservableCollection<TreeViewItem> PrepareChildrenInstance()
        {
            var ret = new ObservableCollection<TreeViewItem>();
            if (AssemblyModel.NamespaceModels.Count != 0)
                ret.Add(null);
            return ret;
        }

        //public IEnumerable<NamespaceModel> NamespacesModels { get; set; }

        public override void BuildMyself()
        {
            base.BuildMyself();
            foreach (var namespacesModel in AssemblyModel.NamespaceModels)
            {
                Children.Add(new NamespaceTreeItem(namespacesModel));
            }
        }
    }
}
