using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using ProjektTPA.Lib.IoC;
using ProjektTPA.Lib.Utility;
using ProjektTPA.Lib.ViewModel;
using TreeViewItem = ProjektTPA.Lib.ViewModel.TreeViewItem;

namespace ProjektTPA.CommandLine
{
    class Program
    {
        private static MainViewModel mainVM;
        static int id = 1;
        private static int id2 =-1;
        static void Main(string[] args)
        {
            ServiceLocator.Kernel.Bind<IDataProvider>().To<DataProvider>();
            mainVM = ServiceLocator.MainViewModel;
            mainVM.LoadCommand.Execute(null);

            while (true)
            {
                id = 1;
                Console.Clear();
                Console.WriteLine("0. Exit");
                Print(mainVM.Nodes, 0);
                Console.Write("Selection: ");
                id2 = Convert.ToInt32(Console.ReadLine());
            }
        }

        private static void Print(ObservableCollection<TreeViewItem> nodes, int indents)
        {
            foreach (var node in nodes)
            {
                if (id2 == 0) Environment.Exit(0);
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
                    Print(node.Children, indents+1);
                } 
            }
        }
    }
}
