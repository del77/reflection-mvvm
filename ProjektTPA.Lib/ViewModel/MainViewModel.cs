
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using Ninject;
using ProjektTPA.Lib.Command;
using ProjektTPA.Lib.Logging;
using ProjektTPA.Lib.Utility;

namespace ProjektTPA.Lib.ViewModel
{
    using Microsoft.Win32;
    using ProjektTPA.Lib.Model;
    public class MainViewModel
    {
        public ObservableCollection<TreeViewItem> Nodes { get; set; }
        private AssemblyTreeItem _assemblyTreeItem;
        private readonly ILoggingManager loggingManager;
        private readonly IDataProvider dataProvider;
        private IReflector reflector;

        public ICommand LoadCommand
            => new RelayCommand(Load);

        [Inject]
        public MainViewModel(ILoggingManager loggingManager, IDataProvider dataProvider, IReflector reflector)
        {
            Nodes = new ObservableCollection<TreeViewItem>();
            this.loggingManager = loggingManager;
            this.dataProvider = dataProvider;
            this.reflector = reflector;
        }
        


        private void Load()
        {
            //Assembly assembly = Assembly.LoadFile((string)path);
            string path = dataProvider.GetPath();
            loggingManager.Log("File path loaded");
            try
            {
                reflector.Reflect(path);
            }
            catch (ArgumentNullException e)
            {
                loggingManager.Log("File path was empty" + e.Message, TraceLevel.Error);
                return;
            }

            loggingManager.Log("assembly loaded");
            _assemblyTreeItem = new AssemblyTreeItem(reflector.AssemblyModel);
            loggingManager.Log("Models created");

            Nodes.Add(_assemblyTreeItem);
        }
    }
}
