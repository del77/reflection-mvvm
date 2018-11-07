using System.Collections.ObjectModel;

namespace ProjektTPA.Lib.ViewModel
{
    public interface ITreeViewItem
    {
        void BuildMyself();
        ObservableCollection<TreeViewItem> PrepareChildrenInstance();
    }
}