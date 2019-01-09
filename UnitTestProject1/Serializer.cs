using System.ComponentModel.Composition;
using BusinessLogic.Model;
using DtoLayer;
using MEF;

namespace ViewModelTests
{
    [Export(typeof(ISerializer))]
    class Serializer : ISerializer
    {
        public void Save(AssemblyDto assemblyModel)
        {

        }

        public AssemblyDto Load()
        {
            return null;
        }
    }
}