using System.Collections.Generic;

namespace DtoLayer
{
    public class NamespaceDto
    {
        public string Name { get; set; }
        public List<NamespaceDto> Namespaces { get; set; }
        public List<TypeDto> Types { get; set; }
    }
}