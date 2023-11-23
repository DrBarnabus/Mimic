namespace Mimic.Setup.ArgumentMatchers;

internal sealed class RefArgumentMatcher : IArgumentMatcher
{
    private readonly object? _reference;

    public RefArgumentMatcher(object? reference) => _reference = reference;

    public bool Matches(object? argument, Type type) =>
        _reference?.GetType().IsValueType ?? false
            ? Equals(_reference, argument)
            : ReferenceEquals(_reference, argument);
}
