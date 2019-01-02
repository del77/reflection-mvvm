using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Model;
using ViewModel.Base;
using ViewModel.Command;
using ViewModel.Interfaces;
using ViewModel.Tree;

namespace ViewModel
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<TreeViewItem> Nodes
        {
            get => _nodes;
            set { _nodes = value; OnPropertyChanged("Nodes"); }
        }

        public bool Working
        {
            get => _working;
            set
            {
                _working = value; OnPropertyChanged("Working");
            }
        }

        public ManualResetEvent oSignalEvent = new ManualResetEvent(false);

        [Import(typeof(ILoggingManager))]
        public ILoggingManager loggingManager;
        [Import(typeof(IPathProvider))]
        private IPathProvider _pathProvider;
        [Import(typeof(IReflector))]
        private IReflector reflector;
        [Import(typeof(ModelService))]
        public ModelService modelService;

        private ObservableCollection<TreeViewItem> _nodes;
        private bool _working;

        public ICommand LoadCommand
            => new RelayCommand(async () =>
            {
                Working = true;
                var item = await Task.Run(() => Load());
                Nodes.Add(item);
                oSignalEvent.Set();
                Working = false;
            });

        public ICommand DeserializeCommand => new RelayCommand(async () =>
        {
            Working = true;
            AssemblyTreeItem item = await Task.Run(() => Deserialize());
            Nodes.Add(item);
            Working = false;
        });

        private AssemblyTreeItem Deserialize()
        {
            AssemblyModel node = modelService.Load();
            return new AssemblyTreeItem(node);
        }

        public ICommand SaveCommand => new RelayCommand(Save);

        private async void Save()
        {
            Working = true;
            await Task.Run(() =>
            {
                int i = 1;
                foreach (AssemblyTreeItem assemblyTreeItem in Nodes)
                {
                    modelService.Save(assemblyTreeItem.AssemblyModel);
                }

            });
            Working = false;
        }

        public MainViewModel()
        {
            Nodes = new ObservableCollection<TreeViewItem>();
        }

        private AssemblyTreeItem Load()
        {
            AssemblyTreeItem _assemblyTreeItem;
            string path = _pathProvider.GetPath();
            //oSignalEvent.Set();
            if (path == String.Empty)
                loggingManager.Log("File path was empty", TraceLevel.Error);
            loggingManager.Log("File path loaded", TraceLevel.Info);

            if (path.Contains(".dll"))
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
