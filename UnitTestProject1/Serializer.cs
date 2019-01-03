using System.ComponentModel.Composition;
using BusinessLogic.Model;
using DtoLayer;
using MEF;

namespace ViewModelTests
{
    [Export(typeof(ISerializer))]
    class Serializer : ISerializer
    {
        public void Serialize(AssemblyDto assemblyModel)
        {

        }

        public AssemblyDto Deserialize()
        {
            return null;
        }
    }
}