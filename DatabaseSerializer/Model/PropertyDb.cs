using System.Collections.Generic;
using DtoLayer;
using DtoLayer.Enums;

namespace DatabaseSerializer.Model
{
    public class PropertyDb
    {
        public int PropertyDbId { get; set; }
        public string Name { get; set; }
        public List<TypeDb> Attributes { get; set; }
        public AccessLevel Access { get; set; }
        public MethodDb Getter { get; set; }
        public MethodDb Setter { get; set; }
        public TypeDb Type { get; set; }
    }
}