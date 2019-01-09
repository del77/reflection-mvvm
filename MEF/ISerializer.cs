using DtoLayer;

namespace MEF
{
    public interface ISerializer
    {
        void Save(AssemblyDto model);
        AssemblyDto Load();
    }
}