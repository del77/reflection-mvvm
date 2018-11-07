using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace ProjektTPA.Lib.Model
{
    public class NamespaceModel : Model
    {
        public NamespaceModel(string ns, IEnumerable<string> nested, Assembly assembly) : base(ns)
        {
            Types = assembly.GetTypes().Where(x => x.Namespace == ns && !x.IsNested).Select(TypeModel.GetTypeWithDetails).ToList();
            var enumerable = nested.ToList();
            if (enumerable.Count() != 0)
            {
                foreach (var nss in enumerable)
                {
                    Namespaces.Add(new NamespaceModel(nss, new string[] {}, assembly));
                }
            }
        }

        public List<TypeModel> Types { get; set; }
        public List<NamespaceModel> Namespaces { get; set; } = new List<NamespaceModel>();
    }
}