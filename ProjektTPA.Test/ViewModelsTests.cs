using System;
using System.Diagnostics;
using System.Linq;
using ProjektTPA.Lib.IoC;
using ProjektTPA.Lib.Logging;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Utility;
using ProjektTPA.Lib.ViewModel;
using Xunit;

namespace ProjektTPA.Test
{
    public class ViewModelsTests
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "TestLibrary.dll";
        IReflector reflector = new Reflector();


        [Fact]
        public void Should_Get_MainViewModel_From_IOC_Container()
        {
            //ServiceLocator.Kernel.Bind<IDataProvider>().To<DataProvider>();
            MainViewModel mainViewModel = ServiceLocator.MainViewModel;
            Assert.NotNull(mainViewModel);
        }

        [Fact]
        public void Should_Populate_AssemblyTreeItem()
        {

            MainViewModel mainViewModel = new MainViewModel(new Lib.Logging.LoggingManager(), new DataProvider(), new Reflector());

            mainViewModel.LoadCommand.Execute(null);

            Assert.Equal(1, mainViewModel.Nodes.Count);
        }

        [Fact]
        public void Should_Populate_ViewModels_For_Assembly()
        {
            ServiceLocator.Kernel.Bind<IDataProvider>().To<DataProvider>();
            MainViewModel mainViewModel = ServiceLocator.MainViewModel;
            mainViewModel.LoadCommand.Execute(null);
            Assert.Null(mainViewModel.Nodes.ElementAt(0).Children.ElementAt(0));
            mainViewModel.Nodes.ElementAt(0).BuildMyself();
            Assert.NotNull(mainViewModel.Nodes.ElementAt(0).Children.ElementAt(0));
        }

        [Fact]
        public void Should_Execute_Loggers_Log_Method()
        {
            LoggingManager manager = new LoggingManager();
            MainViewModel mainViewModel = new MainViewModel(manager, new DataProvider(), new Reflector());
            Assert.Equal(0, manager.messagesSent);
            mainViewModel.LoadCommand.Execute(null);
            Assert.Equal(3, manager.messagesSent);
        }

        [Fact]
        public void Should_Create_Lower_Level_Viewmodels()
        {
            MainViewModel mainViewModel = ServiceLocator.MainViewModel;
            mainViewModel.LoadCommand.Execute(null);
            var zeroNode = mainViewModel.Nodes.ElementAt(0);
            zeroNode.BuildMyself();
            NamespaceTreeItem namespaceVM = (NamespaceTreeItem)zeroNode.Children.ElementAt(0);
            namespaceVM.BuildMyself();
            Assert.Equal(5, namespaceVM.Children.Count);
            TypeTreeItem typeVM = (TypeTreeItem) namespaceVM.Children.ElementAt(0);
            typeVM.BuildMyself();
            Assert.Equal(2, typeVM.Children.Count);
            MethodTreeItem methodVM = (MethodTreeItem)typeVM.Children.ElementAt(0);
            FieldTreeItem fieldVM = (FieldTreeItem)typeVM.Children.ElementAt(1);
            methodVM.BuildMyself();
            fieldVM.BuildMyself();
            Assert.Equal(0, methodVM.Children.Count);
            Assert.Equal(1, fieldVM.Children.Count);
        }
    }


    class DataProvider : IDataProvider
    {
        public string GetPath() => AppDomain.CurrentDomain.BaseDirectory + "TestLibrary.dll";
    }

    class LoggingManager : ILoggingManager
    {
        public int messagesSent = 0;
        public void Log(string message)
        {
            messagesSent++;
        }

        public void Log(string message, TraceLevel level)
        {
            messagesSent++;
        }
    }
}


