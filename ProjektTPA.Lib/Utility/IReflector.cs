using ProjektTPA.Lib.Model;

namespace ProjektTPA.Lib.Utility
{
    public interface IReflector
    {
        AssemblyModel AssemblyModel { get; }
        void Reflect(string assemblyPath);
    }
}