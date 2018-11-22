using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using ProjektTPA.Lib.Logging;
using ProjektTPA.Lib.ViewModel;

namespace ProjektTPA.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Import(typeof(MainViewModel))]
        private MainViewModel mainVM;

        public MainWindow()
        {
            InitializeComponent();

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(DataProvider).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainViewModel).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);
            try
            {
                container.ComposeParts(this);
                DataContext = mainVM;
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

    }
}
