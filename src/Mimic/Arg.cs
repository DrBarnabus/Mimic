using Mimic.Setup.ArgumentMatchers;

namespace Mimic;

[PublicAPI]
public static class Arg
{
    public static TValue Any<TValue>()
    {
        // ReSharper disable once ConvertTypeCheckToNullCheck
        return ArgumentMatcher.Create<TValue>(argument => argument == null || argument is TValue)!;
    }

    public static TValue AnyNotNull<TValue>()
    {
        // ReSharper disable once ConvertTypeCheckToNullCheck
        return ArgumentMatcher.Create<TValue>(argument => argument is TValue)!;
    }

    public static TValue Is<TValue>(TValue value)
    {
        return ArgumentMatcher.Create<TValue>(argument => Equals(argument, value))!;
    }

    public static TValue Is<TValue>(TValue value, IEqualityComparer<TValue> comparer)
    {
        return ArgumentMatcher.Create<TValue>(argument => comparer.Equals(argument, value))!;
    }

    public static TValue Is<TValue>(Expression<Func<TValue, bool>> match)
    {
        return ArgumentMatcher.Create<TValue>(argument => match.Compile().Invoke(argument!))!;
    }

    public static TValue In<TValue>(IEnumerable<TValue> values)
    {
        return ArgumentMatcher.Create<TValue>(argument => values.Contains(argument))!;
    }

    public static TValue In<TValue>(IEnumerable<TValue> values, IEqualityComparer<TValue> comparer)
    {
        return ArgumentMatcher.Create<TValue>(argument => values.Contains(argument, comparer!))!;
    }

    public static TValue In<TValue>(params TValue[] values)
    {
        return ArgumentMatcher.Create<TValue>(argument => values.Contains(argument))!;
    }

    public static TValue NotIn<TValue>(IEnumerable<TValue> values)
    {
        return ArgumentMatcher.Create<TValue>(argument => !values.Contains(argument))!;
    }

    public static TValue NotIn<TValue>(IEnumerable<TValue> values, IEqualityComparer<TValue> comparer)
    {
        return ArgumentMatcher.Create<TValue>(argument => !values.Contains(argument, comparer!))!;
    }

    public static TValue NotIn<TValue>(params TValue[] values)
    {
        return ArgumentMatcher.Create<TValue>(argument => !values.Contains(argument))!;
    }

    public static class Ref<TValue>
    {
        #pragma warning disable CA2211
        public static TValue Any = default!;
        #pragma warning restore CA2211
    }
}
