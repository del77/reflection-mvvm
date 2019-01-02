using System.Collections.Generic;
using DtoLayer.Enums;

namespace DtoLayer
{
    public class FieldDto
    {
        public string Name { get; set; }
        public TypeDto TypeModel { get; set; }
        public List<TypeDto> Attributes { get; set; }
        public AccessLevel Access { get; set; }
    }
}