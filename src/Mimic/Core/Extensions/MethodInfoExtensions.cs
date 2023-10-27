using System.Reflection;

namespace Mimic.Core.Extensions;

internal static class MethodInfoExtensions
{
    internal static bool IsGetter(this MethodInfo method)
        => method.IsSpecialName && method.Name.StartsWith("get_", StringComparison.Ordinal);

    internal static bool IsSetter(this MethodInfo method)
        => method.IsSpecialName && method.Name.StartsWith("set_", StringComparison.Ordinal);
}
