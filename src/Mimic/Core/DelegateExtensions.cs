using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Mimic.Core;

internal static class DelegateExtensions
{
    internal static object? Invoke(this Delegate delegateFunction, IEnumerable<object?>? arguments = null)
    {
        try
        {
            return delegateFunction.DynamicInvoke(arguments as object[] ?? arguments?.ToArray());
        }
        catch (TargetInvocationException ex)
        {
            ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            throw;
        }
    }

    internal static bool CompareParameterTypesTo<TOtherTypes>(this Delegate delegateFunction, TOtherTypes otherTypes)
        where TOtherTypes : IReadOnlyList<Type>
    {
        return delegateFunction.GetMethodInfo().GetParameters().Select(p => p.ParameterType).ToArray().CompareTypes(otherTypes);
    }

    private static bool CompareTypes<TTypes, TOtherTypes>(this TTypes types, TOtherTypes otherTypes)
        where TTypes : IReadOnlyList<Type>
        where TOtherTypes : IReadOnlyList<Type>
    {
        if (types.Count != otherTypes.Count)
            return false;

        for (int i = 0; i < otherTypes.Count; i++)
        {
            var type = types[i];

            if (!type.IsAssignableFrom(otherTypes[i]))
                return false;
        }

        return true;
    }
}
