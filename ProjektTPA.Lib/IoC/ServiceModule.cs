using System.Collections.Generic;
using Ninject.Modules;
using ProjektTPA.Lib.Logging;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Utility;

namespace ProjektTPA.Lib.IoC
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoggingManager>().To<LoggingManager>();
            Bind<IReflector>().To<Reflector>();
        }
    }
}