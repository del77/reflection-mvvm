using System;
using System.Collections.Generic;
using DtoLayer.Enums;

namespace DtoLayer
{
    public class TypeDto
    {
        public string Name { get; set; }
        public TypeDto DeclaringType { get; set; }
        public List<MethodDto> Constructors { get; set; }
        public List<MethodDto> Methods { get; set; }
        public List<TypeDto> NestedTypes { get; set; }
        public List<TypeDto> ImplementedInterfaces { get; set; }
        public List<TypeDto> GenericArguments { get; set; }
        public List<FieldDto> Fields { get; set; }
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }
        public TypeDto BaseType { get; set; }
        public List<PropertyDto> Properties { get; set; }
        public TypeKind TypeKind { get; set; }
        public List<TypeDto> Attributes { get; set; }
        public string NamespaceName;
        public bool Resolved { get; set; } = false;
    }
}