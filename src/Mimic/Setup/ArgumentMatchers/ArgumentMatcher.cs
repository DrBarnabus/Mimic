namespace Mimic.Setup.ArgumentMatchers;

internal abstract class ArgumentMatcher : IArgumentMatcher
{
    internal abstract Type Type { get; }

    protected abstract bool Matches(object? argument, Type parameterType);

    bool IArgumentMatcher.Matches(object? argument, Type parameterType) => Matches(argument, parameterType);

    public static TValue? Create<TValue>(Predicate<TValue?> condition)
    {
        Register(new ArgumentMatcher<TValue>(condition));
        return default;
    }

    public static TValue? Create<TValue>(Func<object?, Type, bool> condition)
    {
        Register(new GenericArgumentMatcher(condition));
        return default;
    }

    private static void Register(ArgumentMatcher argumentMatcher)
    {
        if (ArgumentMatcherObserver.HasActiveObserver(out var activeObserver))
            activeObserver.AddArgumentMatcher(argumentMatcher);
    }
}
