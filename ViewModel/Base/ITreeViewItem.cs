using System.Collections.ObjectModel;

namespace ViewModel.Base
{
    public interface ITreeViewItem
    {
        void BuildMyself();
        ObservableCollection<TreeViewItem> PrepareChildrenInstance();
    }
}