using System.ComponentModel.Composition;
using MEF;

namespace ViewModelTests
{
    [Export(typeof(ISerializer))]
    class Serializer : ISerializer
    {
        public void Serialize(object assemblyModel)
        {

        }

        public object Deserialize()
        {
            return null;
        }
    }
}