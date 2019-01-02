using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DtoLayer;
using DtoLayer.Enums;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class MethodXml
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool Extension { get; set; }
        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        [DataMember]
        public List<FieldXml> Parameters { get; set; }
        [DataMember]
        public TypeXml ReturnType { get; set; }
        [DataMember]
        public List<TypeXml> GenericArguments { get; set; }
        [DataMember]
        public List<TypeXml> Attributes { get; set; }
    }
}