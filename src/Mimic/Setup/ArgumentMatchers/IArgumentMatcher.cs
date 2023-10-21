namespace Mimic.Setup.ArgumentMatchers;

internal interface IArgumentMatcher
{
    bool Matches(object? argument, Type type);
}
