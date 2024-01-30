namespace Mimic.Setup.ArgumentMatchers;

internal sealed class GenericArgumentMatcher : ArgumentMatcher
{
    private static readonly MethodInfo CanCastMethod = typeof(GenericArgumentMatcher).GetMethod(nameof(CanCastImpl), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!;

    private readonly Func<object?, Type, bool> _condition;

    internal override Type Type => typeof(object);

    internal GenericArgumentMatcher(Func<object?, Type, bool> condition) => _condition = condition;

    protected override bool Matches(object? argument, Type parameterType) =>
        CanCast(argument, parameterType) && _condition(argument, parameterType);

    private static bool CanCast(object? value, Type parameterType) =>
        ((Predicate<object?>)Delegate.CreateDelegate(typeof(Predicate<object?>), CanCastMethod.MakeGenericMethod(parameterType)))(value);

    private static bool CanCastImpl<TValue>(object? value)
    {
        if (value is not null)
            return value is TValue;

        var type = typeof(TValue);
        return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
    }
}
