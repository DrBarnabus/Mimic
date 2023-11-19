using System.Reflection;

namespace Mimic.Core.Extensions;

internal static class MethodInfoExtensions
{
    internal static bool IsGetter(this MethodInfo method)
        => method.IsSpecialName && method.Name.StartsWith("get_", StringComparison.Ordinal);

    internal static bool IsSetter(this MethodInfo method)
        => method.IsSpecialName && method.Name.StartsWith("set_", StringComparison.Ordinal);

    internal static MethodInfo GetImplementingMethod(this MethodInfo method, Type proxyType)
    {
        Guard.NotNull(method);
        Guard.NotNull(proxyType);
        Guard.Assert(proxyType.IsClass);

        if (method.IsGenericMethod)
            method = method.GetGenericMethodDefinition();

        var declaringType = method.DeclaringType;
        Guard.Assert(declaringType != null && declaringType.IsAssignableFrom(proxyType));

        if (!declaringType.IsInterface)
            return method.GetBaseDefinition();

        var interfaceMap = proxyType.GetInterfaceMap(declaringType);
        int indexOfMethod = Array.IndexOf(interfaceMap.InterfaceMethods, method);
        Guard.Assert(indexOfMethod >= 0);

        return interfaceMap.TargetMethods[indexOfMethod].GetBaseDefinition();
    }
}
