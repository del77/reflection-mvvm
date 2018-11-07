using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ProjektTPA.Lib.IoC;
using ProjektTPA.Lib.Utility;

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
            ServiceLocator.Kernel.Bind<IDataProvider>().To<DataProvider>();
        }
    }
}
