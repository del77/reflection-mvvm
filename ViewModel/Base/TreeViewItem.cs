using System.Collections.ObjectModel;
using ViewModel.Enums;

namespace ViewModel.Base
{
    public abstract class TreeViewItem : ITreeViewItem
    {
        protected TreeViewItem(string name)
        {
            Name = name;
        }

        public TreeTypeEnum TreeType { get; set; }
        public virtual string Name { get; set; }  
        public ObservableCollection<TreeViewItem> Children { get; set; }= new ObservableCollection<TreeViewItem>();
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                if (wasBuilt)
                    return;
                //Children?.Clear();
                //Children.Clear();
                if(Children != null && Children.Count > 0)
                    BuildMyself();
                wasBuilt = true;
            }
        }

        private bool isExpanded;
        private bool wasBuilt;

        public virtual void BuildMyself()
        {
            Children?.Clear();
        }

        public virtual ObservableCollection<TreeViewItem> PrepareChildrenInstance()
        {
            return new ObservableCollection<TreeViewItem>();
        }
    }
}
