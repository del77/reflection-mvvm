using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Interfaces;
using MEF;
using ViewModel;
using ViewModel.Interfaces;
using ViewModel.Tree;
using Xunit;

namespace ProjektTPA.Test
{
    public class ViewModelsTests
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "\\TestLibrary.dll";
        [Import(typeof(MainViewModel))]
        private MainViewModel mainViewModel = new MainViewModel();

        public ViewModelsTests()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(PathProvider).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainViewModel).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Reflector).Assembly));
            CompositionContainer container = new CompositionContainer(catalog);
            try
            {
                container.ComposeParts(mainViewModel);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
        [Fact]
        public void Should_Get_MainViewModel_From_MEF()
        {
            Assert.NotNull(mainViewModel);
        }

        [Fact]
        public void Should_Populate_AssemblyTreeItem()
        {
            LoadAsync();
            Assert.Equal(1, mainViewModel.Nodes.Count);
        }

        [Fact]
        public void Should_Populate_ViewModels_For_Assembly()
        {
            LoadAsync();
            mainViewModel.LoadCommand.Execute(null);
            Assert.Null(mainViewModel.Nodes.ElementAt(0).Children.ElementAt(0));
            mainViewModel.Nodes.ElementAt(0).BuildMyself();
            Assert.NotNull(mainViewModel.Nodes.ElementAt(0).Children.ElementAt(0));
        }

        [Fact]
        public void Should_Execute_Loggers_Log_Method()
        {
            Manager manager = mainViewModel.loggingManager as Manager;
            mainViewModel.loggingManager = manager;
            Assert.Equal(0, manager.messagesSent);
            LoadAsync();
            Assert.Equal(2, manager.messagesSent);
        }

        [Fact]
        public void Should_Create_Lower_Level_Viewmodels()
        {
            LoadAsync();
            var zeroNode = mainViewModel.Nodes.ElementAt(0);
            zeroNode.BuildMyself();
            NamespaceTreeItem namespaceVM = (NamespaceTreeItem)zeroNode.Children.ElementAt(0);
            namespaceVM.BuildMyself();
            Assert.Equal(5, namespaceVM.Children.Count);
            TypeTreeItem typeVM = (TypeTreeItem) namespaceVM.Children.ElementAt(0);
            typeVM.BuildMyself();
            Assert.Equal(3, typeVM.Children.Count);
            MethodTreeItem methodVM = (MethodTreeItem)typeVM.Children.ElementAt(0);
            FieldTreeItem fieldVM = (FieldTreeItem)typeVM.Children.ElementAt(1);
            methodVM.BuildMyself();
            fieldVM.BuildMyself();
            Assert.Equal(0, methodVM.Children.Count);
            Assert.Equal(1, fieldVM.Children.Count);
        }

        private void LoadAsync()
        {
            System.Threading.Thread oSecondThread = new System.Threading.Thread(mainViewModel.LoadCommand.Execute);
            oSecondThread.Start();
            mainViewModel.oSignalEvent.WaitOne();
            mainViewModel.oSignalEvent.Reset();
        }
    }

    [Export(typeof(IPathProvider))]
    class PathProvider : IPathProvider
    {
        public string GetPath() => AppDomain.CurrentDomain.BaseDirectory + "TestLibrary.dll";
    }

    [Export(typeof(ILoggingManager))]
    class Manager : ILoggingManager
    {
        public int messagesSent = 0;
        public void Log(string message, TraceLevel level)
        {
            messagesSent++;
        }
    }

    [Export(typeof(ISerializer))]
    class Serializer : ISerializer
    {
        public void Serialize(object assemblyModel)
        {
            
        }

        public object Deserialize()
        {
            return null;
        }
    }
}


