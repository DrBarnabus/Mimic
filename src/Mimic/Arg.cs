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

    public static TValue Is<TValue>(Expression<Func<TValue, bool>> match)
    {
        return ArgumentMatcher.Create<TValue>(argument => match.Compile().Invoke(argument!))!;
    }
}
