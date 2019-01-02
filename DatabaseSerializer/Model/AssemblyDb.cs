using System.Collections.Generic;

namespace DatabaseSerializer.Model
{
    public class AssemblyDb
    {
        public int AssemblyDbId { get; set; }
        public string Name { get; set; }
        public List<NamespaceDb> Namespaces { get; set; }
    }
}