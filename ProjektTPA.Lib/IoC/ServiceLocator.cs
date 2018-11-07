using Ninject;
using ProjektTPA.Lib.ViewModel;

namespace ProjektTPA.Lib.IoC
{
    public class ServiceLocator
    {
        public static readonly IKernel Kernel;

        static ServiceLocator()
        {
            Kernel = new StandardKernel(new ServiceModule());
        }

        public static  MainViewModel MainViewModel => Kernel.Get<MainViewModel>();
    }
}