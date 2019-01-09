using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Base;
using ViewModel.Helpers;

namespace ConsoleApplication
{
    class Program
    {
        private MainViewModel mainVM;
        public CompositionContainer container = null;
        static int id = 1;
        private static int id2 = Int32.MinValue;


        private Program()
        {
            Bootstrap();
            int choice;
            Console.WriteLine("1. Reflect");
            Console.WriteLine("2. Deserialize");
            Console.Write("Choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
            if(choice == 1)
            {
                System.Threading.Thread oSecondThread = new System.Threading.Thread(mainVM.LoadCommand.Execute);
                oSecondThread.Start();
                mainVM.oSignalEvent.WaitOne();
                mainVM.oSignalEvent.Reset();
                while (mainVM.Working)
                {
                    Console.Clear();
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                }
                

                Print();
            }
            else if (choice == 2)
            {
                mainVM.DeserializeCommand.Execute(null);
                while (mainVM.Working)
                {
                    Console.Clear();
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                }
                Print();
            }
            else
            {
                Environment.Exit(3);
            }
        }

        private void Nodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Print();
        }


        private void Print()
        {
            while (true)
            {
                id = 1;
                Console.Clear();
                Console.WriteLine("0. Exit | -1. Save");
                PrintTree(mainVM.Nodes, 0);
                Console.Write("Selection: ");
                id2 = Convert.ToInt32(Console.ReadLine());
            }
        }

        public void Bootstrap()
        {
            MainViewModel vm = new MainViewModel();
            List<DirectoryCatalog> directories = new List<DirectoryCatalog>();
            NameValueCollection configDirectories = ConfigurationManager.GetSection("pluginDirectory") as NameValueCollection;

            try
            {
                foreach (var key in configDirectories.AllKeys)
                {
                    directories.Add(new DirectoryCatalog(configDirectories[key]));
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message + "\nPress any key to exit.");
                Console.ReadKey();
                Environment.Exit(2);
            }

            directories.Add(new DirectoryCatalog(@"."));

            AggregateCatalog catalog = new AggregateCatalog(directories);
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(PathProvider).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);
            List<string> missingModules;
            try
            {
                container.ComposeParts(vm);
                missingModules = CompositionCheck.GetMissingModules(directories).ToList();
                if (missingModules.Any())
                    throw new CompositionException();
                mainVM = vm;
            }
            catch (CompositionException compositionException)
            {
                missingModules = CompositionCheck.GetMissingModules(directories).ToList();
                if (missingModules.Any())
                {
                    foreach (var missingModule in missingModules)
                    {
                        Console.WriteLine(missingModule + " plugin missing!");
                    }
                }
                else
                {
                    Console.WriteLine("Only one plugin of each type is allowed.");
                }
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        private void PrintTree(ObservableCollection<TreeViewItem> nodes, int indents)
        {
            foreach (var node in nodes)
            {
                if (id2 == 0) Environment.Exit(0);
                else if (id2 == -1)
                {
                    mainVM.SaveCommand.Execute(null);
                    while (mainVM.Working)
                    {
                        Console.Clear();
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                    }
                    Console.Clear();
                    id = 1;
                }
                if (id == id2 && node.Children.Count > 0)
                {
                    node.IsExpanded = !node.IsExpanded;
                }
                for (int i = 0; i < indents; i++)
                {
                    Console.Write("\t");
                }

                string expand = node.Children.Count == 0 ? "[x]" : node.IsExpanded ? "[-]" : "[+]";
                Console.WriteLine(id++ + ". " + expand + node.Name);
                if (node.IsExpanded)
                {
                    PrintTree(node.Children, indents + 1);
                }
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
        }
    }
}
