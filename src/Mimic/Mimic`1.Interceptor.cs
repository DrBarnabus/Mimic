namespace Mimic;

public partial class Mimic<T>
    : IInterceptor
{
    void IInterceptor.Intercept(Invocation invocation)
    {
        if (HandleMimicGetter(invocation, this))
            return;

        if (HandleToString(invocation, this))
            return;

        lock (_invocations)
            _invocations.Add(invocation);

        if (HandleMatchingSetup(invocation, this))
            return;

        if (Strict)
            throw MimicException.NotMatchingSetup(invocation);

        if (invocation.Method.ReturnType == typeof(void))
            return;

        object? defaultValue = DefaultValueFactory.GetDefaultValue(invocation.Method.ReturnType);
        invocation.SetReturnValue(defaultValue);
    }

    private static bool HandleMimicGetter(Invocation invocation, Mimic<T> mimic)
    {
        if (invocation.Method is not { IsSpecialName: true, Name: $"get_{nameof(IMimicked<T>.Mimic)}" }
            || !typeof(IMimicked<T>).IsAssignableFrom(invocation.Method.DeclaringType))
            return false;

        invocation.SetReturnValue(mimic);
        return true;
    }

    private static bool HandleToString(Invocation invocation, Mimic<T> mimic)
    {
        if (invocation.Method.DeclaringType != typeof(object) || mimic._setups.FindLast(s => s.MatchesInvocation(invocation)) is not null)
            return false;

        invocation.SetReturnValue($"{mimic}.Object");
        return true;
    }

    private static bool HandleMatchingSetup(Invocation invocation, Mimic<T> mimic)
    {
        var matchingSetup = mimic._setups.FindLast(s => s.MatchesInvocation(invocation));
        if (matchingSetup is null)
            return false;

        matchingSetup.Execute(invocation);
        return true;
    }
}
