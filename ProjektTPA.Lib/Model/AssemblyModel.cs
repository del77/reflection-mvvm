using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProjektTPA.Lib.Extensions;

namespace ProjektTPA.Lib.Model
{
    public class AssemblyModel : Model
    {
        public AssemblyModel(Assembly assembly) : base(assembly.ManifestModule.Name)
        {
            List<string> namespaces = assembly.GetTypes().Select(x => x.Namespace).Distinct().ToList();
            List<string> nestedNamespaces = namespaces.Where(x => namespaces.Any(y => x != y && x.Contains(y))).ToList();
            namespaces = namespaces.Where(x => nestedNamespaces.Contains(x) == false).ToList();

            foreach (var ns in namespaces)
            {
                var nested = nestedNamespaces.Where(x => x.Contains(ns));
                NamespaceModels.Add(new NamespaceModel(ns, nested, assembly));
            }
        }

        public List<NamespaceModel> NamespaceModels { get; set; } = new List<NamespaceModel>();
    }
}