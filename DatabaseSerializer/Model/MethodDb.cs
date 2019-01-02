using System;
using System.Collections.Generic;
using DtoLayer;
using DtoLayer.Enums;

namespace DatabaseSerializer.Model
{
    public class MethodDb
    {
        public int MethodDbId { get; set; }
        public string Name { get; set; }
        public bool Extension { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public List<FieldDb> Parameters { get; set; }
        public TypeDb ReturnType { get; set; }
        public List<TypeDb> GenericArguments { get; set; }
        public List<TypeDb> Attributes { get; set; }
    }
}