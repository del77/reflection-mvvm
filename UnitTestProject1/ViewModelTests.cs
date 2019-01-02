using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;
using ViewModel.Tree;

namespace ViewModelTests
{
    [TestClass]
    public class ViewModelTests
    {
        private static string path =
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\test.dll"));
        [Import(typeof(MainViewModel))]
        private MainViewModel mainViewModel = new MainViewModel();

        public ViewModelTests()
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
        [TestMethod]
        public void Should_Get_MainViewModel_From_MEF()
        {
            Assert.IsNotNull(mainViewModel);
        }

        [TestMethod]
        public void Should_Populate_AssemblyTreeItem()
        {
            LoadAsync();
            Assert.AreEqual(1, mainViewModel.Nodes.Count);
        }

        [TestMethod]
        public void Should_Populate_ViewModels_For_Assembly()
        {
            LoadAsync();
            mainViewModel.LoadCommand.Execute(null);
            Assert.IsNull(mainViewModel.Nodes.ElementAt(0).Children.ElementAt(0));
            mainViewModel.Nodes.ElementAt(0).BuildMyself();
            Assert.IsNotNull(mainViewModel.Nodes.ElementAt(0).Children.ElementAt(0));
        }

        [TestMethod]
        public void Should_Execute_Loggers_Log_Method()
        {
            Manager manager = mainViewModel.loggingManager as Manager;
            mainViewModel.loggingManager = manager;
            Assert.AreEqual(0, manager.messagesSent);
            LoadAsync();
            Assert.AreEqual(2, manager.messagesSent);
        }

        [TestMethod]
        public void Should_Create_Lower_Level_Viewmodels()
        {
            LoadAsync();
            var zeroNode = mainViewModel.Nodes.ElementAt(0);
            zeroNode.BuildMyself();
            NamespaceTreeItem namespaceVM = (NamespaceTreeItem)zeroNode.Children.ElementAt(0);
            namespaceVM.BuildMyself();
            Assert.AreEqual(1, namespaceVM.Children.Count);
            TypeTreeItem typeVM = (TypeTreeItem)namespaceVM.Children.ElementAt(0);
            typeVM.BuildMyself();
            Assert.AreEqual(4, typeVM.Children.Count);
            MethodTreeItem methodVM = (MethodTreeItem)typeVM.Children.ElementAt(0);
            MethodTreeItem methodVM2 = (MethodTreeItem)typeVM.Children.ElementAt(1);
            methodVM2.BuildMyself();
            methodVM2.BuildMyself();
            Assert.AreEqual(0, methodVM.Children.Count);
            Assert.AreEqual(2, methodVM2.Children.Count);
        }

        private void LoadAsync()
        {
            System.Threading.Thread oSecondThread = new System.Threading.Thread(mainViewModel.LoadCommand.Execute);
            oSecondThread.Start();
            mainViewModel.oSignalEvent.WaitOne();
            mainViewModel.oSignalEvent.Reset();
            //await Task.Run(() => mainViewModel.LoadCommand.Execute(null));
        }
    }
}
