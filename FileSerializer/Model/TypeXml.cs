using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DtoLayer;
using DtoLayer.Enums;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class TypeXml
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public TypeXml DeclaringType { get; set; }
        [DataMember]
        public List<MethodXml> Constructors { get; set; }
        [DataMember]
        public List<MethodXml> Methods { get; set; }
        [DataMember]
        public List<TypeXml> NestedTypes { get; set; }
        [DataMember]
        public List<TypeXml> ImplementedInterfaces { get; set; }
        [DataMember]
        public List<TypeXml> GenericArguments { get; set; }
        [DataMember]
        public List<FieldXml> Fields { get; set; }
        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }
        [DataMember]
        public TypeXml BaseType { get; set; }
        [DataMember]
        public List<PropertyXml> Properties { get; set; }
        [DataMember]
        public TypeKind TypeKind { get; set; }
        [DataMember]
        public List<TypeXml> Attributes { get; set; }
        [DataMember]
        public string NamespaceName;
        [DataMember]
        public bool Resolved { get; set; } = false;


    }
}