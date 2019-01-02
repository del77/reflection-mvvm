using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using BusinessLogic.Interfaces;
using BusinessLogic.Model;

namespace BusinessLogic
{
    [Export(typeof(IReflector))]
    public class Reflector : IReflector
    {
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