using System.Collections.Generic;
using System.Runtime.Serialization;
using DtoLayer;
using DtoLayer.Enums;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class PropertyXml
    {
        [DataMember]
        public string Name { get; set; }
        //public new TypeXml TypeModel { get; set; }
        [DataMember]
        public List<TypeDto> Attributes { get; set; }
        [DataMember]
        public AccessLevel Access { get; set; }
        [DataMember]
        public MethodXml Getter { get; set; }
        [DataMember]
        public MethodXml Setter { get; set; }
        [DataMember]
        public TypeXml Type { get; set; }
    }
}