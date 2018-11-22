using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using ProjektTPA.Lib.Model;

namespace ProjektTPA.Lib.Utility
{
    [Export(typeof(ISerializer))]
    public class FileSerializer : ISerializer
    {
        public void Serialize(AssemblyModel assemblyModel, string path)
        {
            DataContractSerializer dataContractSerializer =
                new DataContractSerializer(typeof(Model.AssemblyModel));

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