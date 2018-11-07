using System.Reflection;
using Ninject;
using ProjektTPA.Lib.Model;

namespace ProjektTPA.Lib.Utility
{
    public class Reflector : IReflector
    {
        [Inject]
        public Reflector()
        {
        }

        public AssemblyModel AssemblyModel { get; set; }
        public void Reflect(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            AssemblyModel = new AssemblyModel(assembly);
        }
    }
}