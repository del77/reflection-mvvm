using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using ProjektTPA.Lib.ViewModel;
using TreeViewItem = ProjektTPA.Lib.ViewModel.TreeViewItem;

namespace ProjektTPA.CommandLine
{
    class Program
    {
        [Import(typeof(MainViewModel))]
        private MainViewModel mainVM;
        public CompositionContainer container = null;
        static int id = 1;
        private static int id2 = Int32.MinValue;


        private Program()
        {
            ComposeParts();
            System.Threading.Thread oSecondThread = new System.Threading.Thread(mainVM.LoadCommand.Execute);
            oSecondThread.Start();
            mainVM.oSignalEvent.WaitOne(); //This thread will block here until the reset event is sent.
            mainVM.oSignalEvent.Reset();
            Print();
            //mainVM.Nodes.CollectionChanged += Nodes_CollectionChanged;
            //mainVM.LoadCommand.Execute(null);
            

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

        public void ComposeParts()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(DataProvider).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainViewModel).Assembly));
            
            container = new CompositionContainer(catalog);
            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
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
                }
                if (id == id2 && node.Children.Count >0)
                {
                    node.IsExpanded = !node.IsExpanded;
                }
                for (int i = 0; i < indents; i++)
                {
                    Console.Write("\t");
                }

                string expand = node.Children.Count == 0 ? "[x]" : node.IsExpanded ? "[-]" : "[+]"; 
                Console.WriteLine(id++ + ". " + expand+node.Name);
                if (node.IsExpanded)
                {
                    PrintTree(node.Children, indents+1);
                } 
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
        }
    }
}
