using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer
{
    public class AssemblyDto
    {
        public string Name { get; set; }
        public List<NamespaceDto> Namespaces { get; set; }
    }
}
