using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using Ninject;
using ProjektTPA.Lib.Model;

namespace ProjektTPA.Lib.Utility
{
    [Export(typeof(IReflector))]
    public class Reflector : IReflector
    {
        //[Inject]
        public Reflector()
        {
        }

        public AssemblyModel AssemblyModel { get; set; }
        public void Reflect(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFile(Path.GetFullPath(assemblyPath));
            AssemblyModel = new AssemblyModel(assembly);
        }
    }
}