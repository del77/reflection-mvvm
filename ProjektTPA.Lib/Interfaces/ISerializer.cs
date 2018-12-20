using ProjektTPA.Lib.Model;

namespace ProjektTPA.Lib.Utility
{
    public interface ISerializer
    {
        void Serialize(AssemblyModel assemblyModel, string path);
        AssemblyModel Deserialize(string path);
    }
}