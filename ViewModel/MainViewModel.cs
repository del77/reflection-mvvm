using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektTPA.Lib.Logging;
using ProjektTPA.Lib.Utility;
using ViewModel.Command;
using ViewModel.Interfaces;
using ViewModel.ViewModel.Logging;

namespace ViewModel.ViewModel
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<TreeViewItem> Nodes
        {
            get => _nodes;
            set { _nodes = value; OnPropertyChanged("Nodes");  }
        }

        public ManualResetEvent oSignalEvent = new ManualResetEvent(false);

        [Import(typeof(ILoggingManager))]
        public ILoggingManager loggingManager;
        [Import(typeof(IPathProvider))]
        private IPathProvider _pathProvider;
        [Import(typeof(IReflector))]
        private IReflector reflector;
        [Import(typeof(ISerializer))]
        private ISerializer serializer;

        private ObservableCollection<TreeViewItem> _nodes;

        public ICommand LoadCommand
            => new RelayCommand(async () =>
            {
                var item = await Task.Run(() => Load());
                Nodes.Add(item);
                oSignalEvent.Set();
            });
       

        public ICommand SaveCommand => new RelayCommand(Save);

        private void Save()
        {
            Task.Run(() =>
            {
                int i = 1;
                foreach (AssemblyTreeItem assemblyTreeItem in Nodes)
                {
                    serializer.Serialize(assemblyTreeItem.AssemblyModel,
                        AppDomain.CurrentDomain.BaseDirectory + "result" + i++ + ".xml");
                }


            });

        }

        public MainViewModel()
        {
            Nodes = new ObservableCollection<TreeViewItem>();
        }

        private AssemblyTreeItem Load()
        {
            //Assembly assembly = Assembly.LoadFile((string)path);
            AssemblyTreeItem _assemblyTreeItem;
            string path = _pathProvider.GetPath();
            if (path == String.Empty)
                loggingManager.Log("File path was empty", TraceLevel.Error);
            loggingManager.Log("File path loaded", TraceLevel.Info);
            
            if (path.Contains(".xml"))
            {
                _assemblyTreeItem = new AssemblyTreeItem(serializer.Deserialize(path));
            }
            else if (path.Contains(".dll"))
            {
                reflector.Reflect(path);
                _assemblyTreeItem = new AssemblyTreeItem(reflector.AssemblyModel);
            }
            else
            {
                return null;
            }

            loggingManager.Log("Models created", TraceLevel.Info);

            return _assemblyTreeItem;
        }



    }
}
