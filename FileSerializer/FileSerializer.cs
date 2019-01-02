using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DtoLayer;
using FileSerializer.Model;
using MEF;

namespace FileSerializer
{
    [Export(typeof(ISerializer))]
    public class FileSerializer : ISerializer
    {
        private string path;
        public FileSerializer()
        {
            path = "serialized.xml";
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AssemblyXml, AssemblyDto>().ReverseMap();
                cfg.CreateMap<NamespaceXml, NamespaceDto>().ReverseMap();
                cfg.CreateMap<FieldXml, FieldDto>().ReverseMap();
                cfg.CreateMap<MethodXml, MethodDto>().ReverseMap();
                cfg.CreateMap<PropertyXml, PropertyDto>().ReverseMap();
                cfg.CreateMap<TypeXml, TypeDto>().ReverseMap();
            });
        }
        public void Serialize(object model)
        {
            AssemblyDto assemblyDto = (AssemblyDto) model;
            AssemblyXml assemblyXml = Mapper.Map<AssemblyXml>(assemblyDto);

            DataContractSerializer dataContractSerializer =
                new DataContractSerializer(typeof(AssemblyXml));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fileStream, assemblyXml);
            }
        }

        public object Deserialize()
        {
            AssemblyXml ret;
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(AssemblyXml));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                ret = (AssemblyXml)dataContractSerializer.ReadObject(fileStream);
            }
            
            return Mapper.Map<AssemblyDto>(ret);
        }
    }
}
