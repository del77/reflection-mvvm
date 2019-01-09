using DtoLayer.Enums;
using System.Collections.Generic;

namespace DatabaseSerializer.Model
{
    public class FieldDb
    {
        public int FieldDbId { get; set; }
        public string Name { get; set; }
        public TypeDb TypeModel { get; set; }
        public List<TypeDb> Attributes { get; set; }
        public AccessLevel Access { get; set; }
        public bool IsReadOnly { get; set; }
    }
}