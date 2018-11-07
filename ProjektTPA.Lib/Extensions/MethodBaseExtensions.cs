using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProjektTPA.Lib.Extensions
{
    public static class MethodBaseExtensions
    {
        public static bool GetVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }
    }
}
