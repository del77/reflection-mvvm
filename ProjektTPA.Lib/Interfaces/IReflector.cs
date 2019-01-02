using BusinessLogic.Model;

namespace BusinessLogic.Interfaces
{
    public interface IReflector
    {
        AssemblyModel AssemblyModel { get; }
        void Reflect(string assemblyPath);
    }
}