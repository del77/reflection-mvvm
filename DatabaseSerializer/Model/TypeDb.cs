using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DtoLayer;
using DtoLayer.Enums;

namespace DatabaseSerializer.Model
{
    public class TypeDb
    {
        public int TypeDbId { get; set; }
        public string Name { get; set; }
        public TypeDb DeclaringType { get; set; }
        public List<MethodDb> Constructors { get; set; }
        public List<MethodDb> Methods { get; set; }
        public List<TypeDb> NestedTypes { get; set; }
        public List<TypeDb> ImplementedInterfaces { get; set; }
        public List<TypeDb> GenericArguments { get; set; }
        public List<FieldDb> Fields { get; set; }
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }
        public TypeDb BaseType { get; set; }
        public List<PropertyDb> Properties { get; set; }
        public TypeKind TypeKind { get; set; }
        public List<TypeDb> Attributes { get; set; }
        public string NamespaceName;
        public bool Resolved { get; set; } = false;


        [InverseProperty("TypeModel")]
        public ICollection<FieldDb> FieldType { get; set; }
        [InverseProperty("Attributes")]
        public ICollection<FieldDb> FieldAttributes { get; set; }

        [InverseProperty("Attributes")]
        public ICollection<MethodDb> MethodAttributes { get; set; }
    }
}