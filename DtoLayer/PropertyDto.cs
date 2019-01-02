using System.Collections.Generic;
using DtoLayer.Enums;

namespace DtoLayer
{
    public class PropertyDto
    {
        public string Name { get; set; }
        //public TypeDto TypeModel { get; set; }
        public List<TypeDto> Attributes { get; set; }
        public AccessLevel Access { get; set; }
        public MethodDto Getter { get; set; }
        public MethodDto Setter { get; set; }
        public TypeDto Type { get; set; }
    }
}