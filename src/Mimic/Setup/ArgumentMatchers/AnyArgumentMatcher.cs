namespace Mimic.Setup.ArgumentMatchers;

internal sealed class AnyArgumentMatcher : IArgumentMatcher
{
    public static readonly AnyArgumentMatcher Instance = new();

    public bool Matches(object? argument, Type type) => true;
}
