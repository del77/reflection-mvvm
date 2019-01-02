using System;

namespace BusinessLogic.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAccessible(this Type type)
        {
            return type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamANDAssem;
        }

        public static string GetNamespace(this Type type)
        {
            return type.Namespace ?? string.Empty;
        }

        public static bool GetVisible(this Type type)
        {
            return type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamANDAssem;
        }
    }
}
