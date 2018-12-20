using System.Collections.ObjectModel;

namespace ViewModel.ViewModel
{
    public interface ITreeViewItem
    {
        void BuildMyself();
        ObservableCollection<TreeViewItem> PrepareChildrenInstance();
    }
}