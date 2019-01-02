using System.Reflection;

namespace BusinessLogic.Extensions
{
    public static class MethodBaseExtensions
    {
        public static bool GetVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }
    }
}
