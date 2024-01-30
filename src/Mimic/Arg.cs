using Mimic.Setup.ArgumentMatchers;

// ReSharper disable ConvertTypeCheckToNullCheck

namespace Mimic;

[PublicAPI]
public static class Arg
{
    public static TValue Any<TValue>() => typeof(TValue).IsOrContainsGenericMatcher()
            ? ArgumentMatcher.Create<TValue>((argument, parameterType) => argument == null || parameterType.IsInstanceOfType(argument))!
            : ArgumentMatcher.Create<TValue>(argument => argument == null || argument is TValue)!;

    public static TValue AnyNotNull<TValue>() => typeof(TValue).IsOrContainsGenericMatcher()
            ? ArgumentMatcher.Create<TValue>((argument, parameterType) => argument != null && parameterType.IsInstanceOfType(argument))!
            : ArgumentMatcher.Create<TValue>(argument => argument is TValue)!;

    public static TValue Is<TValue>(TValue value)
    {
        Guard.Assert(!typeof(TValue).IsOrContainsGenericMatcher(), "This overload does not support generic matchers due to the strongly typed value. Please use the overload with `Expression<Func<object, Type, bool>> predicate` instead.");

        return ArgumentMatcher.Create<TValue>(argument => Equals(argument, value))!;
    }

    public static TValue Is<TValue>(TValue value, IEqualityComparer<TValue> comparer)
    {
        Guard.Assert(!typeof(TValue).IsOrContainsGenericMatcher(), "This overload does not support generic matchers due to the strongly typed value. Please use the overload with `Expression<Func<object, Type, bool>> predicate` instead.");

        return ArgumentMatcher.Create<TValue>(argument => comparer.Equals(argument, value))!;
    }

    public static TValue Is<TValue>(Expression<Func<TValue, bool>> match)
    {
        Guard.Assert(!typeof(TValue).IsOrContainsGenericMatcher(), "This overload does not support generic matchers due to the strongly typed value. Please use the overload with `Expression<Func<object, Type, bool>> predicate` instead.");

        return ArgumentMatcher.Create<TValue>(argument => match.Compile().Invoke(argument!))!;
    }

    public static TValue Is<TValue>(Expression<Func<object, Type, bool>> match) =>
        ArgumentMatcher.Create<TValue>((argument, parameterType) => match.Compile().Invoke(argument!, parameterType))!;

    public static TValue In<TValue>(IEnumerable<TValue> values) =>
        ArgumentMatcher.Create<TValue>(argument => values.Contains(argument))!;

    public static TValue In<TValue>(IEnumerable<TValue> values, IEqualityComparer<TValue> comparer) =>
        ArgumentMatcher.Create<TValue>(argument => values.Contains(argument, comparer!))!;

    public static TValue In<TValue>(params TValue[] values) =>
        ArgumentMatcher.Create<TValue>(argument => values.Contains(argument))!;

    public static TValue NotIn<TValue>(IEnumerable<TValue> values) =>
        ArgumentMatcher.Create<TValue>(argument => !values.Contains(argument))!;

    public static TValue NotIn<TValue>(IEnumerable<TValue> values, IEqualityComparer<TValue> comparer) =>
        ArgumentMatcher.Create<TValue>(argument => !values.Contains(argument, comparer!))!;

    public static TValue NotIn<TValue>(params TValue[] values) =>
        ArgumentMatcher.Create<TValue>(argument => !values.Contains(argument))!;

    public static class Ref<TValue>
    {
        #pragma warning disable CA2211
        public static TValue Any = default!;
        #pragma warning restore CA2211
    }
}
