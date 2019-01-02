using System.Collections.ObjectModel;
using BusinessLogic.Model;
using ViewModel.Base;
using ViewModel.Enums;

namespace ViewModel.Tree
{
    public class AssemblyTreeItem : TreeViewItem
    {
        public AssemblyModel AssemblyModel { get; set; }
        public AssemblyTreeItem(AssemblyModel assembly) : base(assembly.Name)
        {
            //NamespacesModels = assembly.Namespaces;
            TreeType = TreeTypeEnum.Assembly;
            AssemblyModel = assembly;
            Children = PrepareChildrenInstance();
        }

        public sealed override ObservableCollection<TreeViewItem> PrepareChildrenInstance()
        {
            var ret = new ObservableCollection<TreeViewItem>();
            if (AssemblyModel.Namespaces.Count != 0)
                ret.Add(null);
            return ret;
        }

        //public IEnumerable<NamespaceModel> NamespacesModels { get; set; }

        public override void BuildMyself()
        {
            base.BuildMyself();
            foreach (var namespacesModel in AssemblyModel.Namespaces)
            {
                Children.Add(new NamespaceTreeItem(namespacesModel));
            }
        }
    }
}
