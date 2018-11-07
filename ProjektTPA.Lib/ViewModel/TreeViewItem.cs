using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.ViewModel
{
    public abstract class TreeViewItem : ITreeViewItem
    {
        protected TreeViewItem(string name)
        {
            Name = name;
        }

        public TreeTypeEnum TreeType { get; set; }
        public string Name { get; set; }  
        public ObservableCollection<TreeViewItem> Children { get; set; }
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
