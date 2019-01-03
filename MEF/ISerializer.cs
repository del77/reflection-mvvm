using DtoLayer;

namespace MEF
{
    public interface ISerializer
    {
        void Serialize(AssemblyDto model);
        AssemblyDto Deserialize();
    }
}