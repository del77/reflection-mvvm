using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileSerializer.Model
{
    [DataContract]
    public class AssemblyXml
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<NamespaceXml> Namespaces { get; set; }
    }
}