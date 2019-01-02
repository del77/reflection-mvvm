using System.Collections.Generic;
using System.Runtime.Serialization;
using DtoLayer;
using DtoLayer.Enums;

namespace FileSerializer.Model
{
    [DataContract (IsReference = true)]
    public class FieldXml
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public TypeXml TypeModel { get; set; }
        [DataMember]
        public List<TypeXml> Attributes { get; set; }
        [DataMember]
        public AccessLevel Access { get; set; }
    }
}