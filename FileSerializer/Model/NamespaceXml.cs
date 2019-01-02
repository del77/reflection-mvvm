using System.Collections.Generic;
using System.Runtime.Serialization;
using DtoLayer;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceXml
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<NamespaceXml> Namespaces { get; set; }
        [DataMember]
        public List<TypeXml> Types { get; set; }
    }
}