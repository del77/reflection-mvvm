using System;
using System.Collections.Generic;
using DtoLayer.Enums;

namespace DtoLayer
{
    public class MethodDto
    {
        public string Name { get; set; }
        public bool Extension { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public List<FieldDto> Parameters { get; set; }
        public TypeDto ReturnType { get; set; }
        public List<TypeDto> GenericArguments { get; set; }
        public List<TypeDto> Attributes { get; set; }
    }
}