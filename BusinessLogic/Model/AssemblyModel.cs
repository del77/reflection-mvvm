using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BusinessLogic.Model
{
    public class AssemblyModel
    {
        public AssemblyModel()
        {
            
        }
        public AssemblyModel(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            List<string> namespaces = assembly.GetTypes().Select(x => x.Namespace).Distinct().ToList();
            List<string> nestedNamespaces = namespaces.Where(x => namespaces.Any(y => x != y && x.Contains(y))).ToList();
            namespaces = namespaces.Where(x => nestedNamespaces.Contains(x) == false).ToList();

            foreach (var ns in namespaces)
            {
                var nested = nestedNamespaces.Where(x => x.Contains(ns));
                Namespaces.Add(new NamespaceModel(ns, nested, assembly));
            }
        }
        public string Name { get; set; }
        public List<NamespaceModel> Namespaces { get; set; } = new List<NamespaceModel>();
    }
}