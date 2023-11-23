using System.Runtime.ExceptionServices;

namespace Mimic.Core.Extensions;

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
        return delegateFunction.GetMethodInfo().GetParameters().Select(p => p.ParameterType).ToArray().CompareWith(otherTypes.ToArray());
    }
}
