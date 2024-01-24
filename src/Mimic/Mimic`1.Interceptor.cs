namespace Mimic;

public partial class Mimic<T>
    : IInterceptor
{
    void IInterceptor.Intercept(Invocation invocation)
    {
        if (WellKnownMethods.Handlers.TryGetValue(invocation.Method, out var handler) && handler.Invoke(invocation, this))
            return;

        lock (_invocations)
            _invocations.Add(invocation);

        if (MatchAndExecuteSetup(invocation, this))
            return;

        if (Strict)
            throw MimicException.NoMatchingSetup(invocation);

        SetDefaultReturnValue(invocation);
    }

    private static bool MatchAndExecuteSetup(Invocation invocation, Mimic<T> mimic)
    {
        var matchingSetup = mimic._setups.FindLast(s => s.MatchesInvocation(invocation));
        if (matchingSetup is null)
            return false;

        matchingSetup.Execute(invocation);
        return true;
    }

    private static void SetDefaultReturnValue(Invocation invocation)
    {
        if (invocation.Method.ReturnType == typeof(void))
            return;

        object? defaultValue = DefaultValueFactory.GetDefaultValue(invocation.Method.ReturnType);
        invocation.SetReturnValue(defaultValue);
    }

    private static class WellKnownMethods
    {
        private static readonly MethodInfo MimickedMimicPropertyGetter = typeof(IMimicked<T>).GetProperty(nameof(IMimicked<T>.Mimic))!.GetMethod!;
        private static readonly MethodInfo ObjectToStringMethod = typeof(object).GetMethod(nameof(ToString), BindingFlags.Public | BindingFlags.Instance)!;
        private static readonly MethodInfo ObjectEqualsMethod = typeof(object).GetMethod(nameof(Equals), BindingFlags.Public | BindingFlags.Instance)!;
        private static readonly MethodInfo ObjectGetHashCodeMethod = typeof(object).GetMethod(nameof(GetHashCode), BindingFlags.Public | BindingFlags.Instance)!;

        public static readonly Dictionary<MethodInfo, Func<Invocation, Mimic<T>, bool>> Handlers = new()
        {
            [MimickedMimicPropertyGetter] = HandleMimickedMimicPropertyGetter,
            [ObjectToStringMethod] = HandleObjectToStringMethod,
            [ObjectEqualsMethod] = HandleObjectEqualsMethod,
            [ObjectGetHashCodeMethod] = HandleObjectGetHashCodeMethod
        };

        private static bool HandleMimickedMimicPropertyGetter(Invocation invocation, Mimic<T> mimic)
        {
            if (!typeof(IMimicked<T>).IsAssignableFrom(invocation.Method.DeclaringType))
                return false;

            invocation.SetReturnValue(mimic);
            return true;
        }

        private static bool HandleObjectToStringMethod(Invocation invocation, Mimic<T> mimic)
        {
            if (!IsObjectMethodWithoutMatch(invocation, mimic))
                return false;

            invocation.SetReturnValue($"{mimic}.Object");
            return true;
        }

        private static bool HandleObjectEqualsMethod(Invocation invocation, Mimic<T> mimic)
        {
            if (!IsObjectMethodWithoutMatch(invocation, mimic))
                return false;

            invocation.SetReturnValue(ReferenceEquals(invocation.Arguments.First(), mimic.Object));
            return true;
        }

        private static bool HandleObjectGetHashCodeMethod(Invocation invocation, Mimic<T> mimic)
        {
            if (!IsObjectMethodWithoutMatch(invocation, mimic))
                return false;

            invocation.SetReturnValue(mimic.GetHashCode());
            return true;
        }

        private static bool IsObjectMethodWithoutMatch(Invocation invocation, Mimic<T> mimic) =>
            invocation.Method.DeclaringType == typeof(object) && mimic._setups.FindLast(s => s.MatchesInvocation(invocation)) is null;
    }
}
