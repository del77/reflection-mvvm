using System.Collections.Generic;

namespace DatabaseSerializer.Model
{
    public class NamespaceDb
    {
        public int NamespaceDbId { get; set; }
        public string Name { get; set; }
        public List<NamespaceDb> Namespaces { get; set; }
        public List<TypeDb> Types { get; set; }
    }
}