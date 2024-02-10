using System.Runtime.ExceptionServices;

namespace Mimic.Core.Extensions;

internal static class DelegateExtensions
{
    internal static object? Invoke(this Delegate delegateFunction, IEnumerable<object?>? arguments = null)
    {
        try
        {
            return delegateFunction.DynamicInvoke(arguments?.ToArray());
        }
        catch (TargetInvocationException ex)
        {
            ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            throw;
        }
    }

    internal static bool CompareParameterTypesTo<TOtherTypes>(this Delegate delegateFunction, TOtherTypes otherTypes)
        where TOtherTypes : IReadOnlyList<Type> =>
        delegateFunction.GetMethodInfo().GetParameters().Select(p => p.ParameterType).ToArray().CompareWith(otherTypes.ToArray());

    internal static void ValidateDelegateParameterCount(this Delegate delegateFunction, int expectedNumberOfParameters)
    {
        var methodInfo = delegateFunction.GetMethodInfo();

        int actualNumberOfParameters = methodInfo.GetParameters().Length;
        if (methodInfo.IsStatic && (methodInfo.IsDefined(typeof(ExtensionAttribute)) || delegateFunction.Target != null))
            actualNumberOfParameters--;

        if (actualNumberOfParameters > 0 && actualNumberOfParameters != expectedNumberOfParameters)
            throw MimicException.WrongCallbackParameterCount(expectedNumberOfParameters, actualNumberOfParameters);
    }

    internal static void ValidateDelegateReturnType(this Delegate delegateFunction, Type expectedReturnType)
    {
        var actualReturnType = delegateFunction.GetMethodInfo().ReturnType;
        if (actualReturnType == typeof(void))
            throw MimicException.WrongCallbackReturnType(expectedReturnType, null);

        if (!expectedReturnType.IsAssignableFrom(actualReturnType))
            throw MimicException.WrongCallbackReturnType(expectedReturnType, actualReturnType);
    }
}
