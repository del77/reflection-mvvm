using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Windows;
using ViewModel.Helpers;
using ViewModel.ViewModel;

namespace ProjektTPA.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrap();
        }

        private void Bootstrap()
        {
            MainViewModel vm = new MainViewModel();
            List<DirectoryCatalog> directories = new List<DirectoryCatalog>();
            NameValueCollection configDirectories = ConfigurationManager.GetSection("pluginDirectory") as NameValueCollection;

            foreach (var key in configDirectories.AllKeys)
            {
                directories.Add(new DirectoryCatalog(configDirectories[key]));
            }

            directories.Add(new DirectoryCatalog(@"."));

            AggregateCatalog catalog = new AggregateCatalog(directories);
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(WPFPathProvider).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);
            List<string> missingModules;
            try
            {
                container.ComposeParts(vm);
                missingModules = CompositionCheck.GetMissingModules(directories).ToList();
                if (missingModules.Any())
                    throw new CompositionException();
                MainWindow window = new MainWindow();
                MainWindow.DataContext = vm;
                window.Show();
            }
            catch (CompositionException compositionException)
            {
                missingModules = CompositionCheck.GetMissingModules(directories).ToList();
                if (missingModules.Any())
                {
                    foreach (var missingModule in missingModules)
                    {
                        MessageBox.Show(missingModule + " plugin missing!");
                    }
                    System.Windows.Application.Current.Shutdown();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
