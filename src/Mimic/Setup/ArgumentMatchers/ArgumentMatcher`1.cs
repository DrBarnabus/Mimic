namespace Mimic.Setup.ArgumentMatchers;

internal sealed class ArgumentMatcher<TValue> : ArgumentMatcher
{
    private readonly Predicate<TValue?> _condition;

    internal override Type Type => typeof(TValue);

    internal ArgumentMatcher(Predicate<TValue?> condition) => _condition = condition;

    protected override bool Matches(object? argument)
    {
        return CanCast(argument) && _condition((TValue?)argument);
    }

    private static bool CanCast(object? value)
    {
        if (value is not null)
            return value is TValue;

        var type = typeof(TValue);
        return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
    }
}
