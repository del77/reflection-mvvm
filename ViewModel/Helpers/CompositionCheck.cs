using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel;
using ViewModel.ViewModel.Logging;

namespace ViewModel.Helpers
{
    public class CompositionCheck
    {
        public static IEnumerable<string> GetMissingModules(List<DirectoryCatalog> directories)
        {
            List<string> ret = new List<string>();
            var fields = typeof(MainViewModel).GetFields(BindingFlags.Instance | BindingFlags.NonPublic |
                                                         BindingFlags.Public | BindingFlags.Static |
                                                         BindingFlags.DeclaredOnly);
            var importFields = fields.Where(x => x.CustomAttributes.Count(y => y.AttributeType.Name == "ImportAttribute" || y.AttributeType.Name == "ImportManyAttribute") != 0);
            List<string> neededInterfaces = importFields.Select(x => x.FieldType.Name).ToList();

            fields = typeof(LoggingManager).GetFields(BindingFlags.Instance | BindingFlags.NonPublic |
                                                         BindingFlags.Public | BindingFlags.Static |
                                                         BindingFlags.DeclaredOnly);
            importFields = fields.Where(x => x.CustomAttributes.Count(y => y.AttributeType.Name == "ImportAttribute" || y.AttributeType.Name == "ImportManyAttribute") != 0);
            neededInterfaces.AddRange(importFields.Select(x => x.FieldType.Name).ToList());

            List<Assembly> assemblies = new List<Assembly>();
            List<string> Files = new List<string>();
            for (int i = 0; i < directories.Count - 1; i++)
            {
                Files.AddRange(Directory.GetFiles(directories[i].FullPath));
            }
            foreach (var file in Files)
            {
                assemblies.Add(Assembly.LoadFile(file));
            }

            foreach (var neededInterface in neededInterfaces)
            {
                if (neededInterface == "IPathProvider" || neededInterface == "IReflector" || neededInterface == "ILoggingManager")
                    continue;
                bool found = false;
                foreach (var assembly in assemblies)
                {
                    var test = assembly.GetTypes();
                    var test2 = test.Select(x => x.GetInterfaces());
                    var types = assembly.GetTypes().Where(x => x.GetInterfaces().Select(y => y.Name).Contains(neededInterface));
                    if (types.Any())
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    ret.Add(neededInterface.Substring(1));
                }
            }

            return ret;
        }
    }
}
