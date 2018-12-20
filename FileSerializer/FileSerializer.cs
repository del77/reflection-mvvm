using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Utility;

namespace FileSerializer
{
    [Export(typeof(ISerializer))]
    public class FileSerializer : ISerializer
    {
        public void Serialize(AssemblyModel assemblyModel, string path)
        {
            DataContractSerializer dataContractSerializer =
                new DataContractSerializer(typeof(AssemblyModel));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fileStream, assemblyModel);
            }
        }

        public AssemblyModel Deserialize(string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(AssemblyModel));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (AssemblyModel)dataContractSerializer.ReadObject(fileStream);
            }
        }
    }
}
