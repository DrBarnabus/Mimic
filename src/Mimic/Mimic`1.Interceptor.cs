using Mimic.Proxy;

namespace Mimic;

public partial class Mimic<T>
    : IInterceptor
{
    void IInterceptor.Intercept(IInvocation invocation)
    {
        if (HandleMimicGetter(invocation, this))
            return;

        if (HandleMatchingSetup(invocation, this))
            return;

        if (invocation.Method.ReturnType != typeof(void))
            throw new NotImplementedException();
    }

    private static bool HandleMimicGetter(IInvocation invocation, Mimic<T> mimic)
    {
        if (invocation.Method is not { IsSpecialName: true, Name: $"get{nameof(IMimicked<T>.Mimic)}" }
            || !typeof(IMimicked<T>).IsAssignableFrom(invocation.Method.DeclaringType))
            return false;

        invocation.SetReturnValue(mimic);
        return true;
    }

    private static bool HandleMatchingSetup(IInvocation invocation, Mimic<T> mimic)
    {
        var matchingSetup = mimic._setups.FindLast(s => s.MatchesInvocation(invocation));
        if (matchingSetup is null)
            return false;

        matchingSetup.Execute(invocation);
        return true;
    }
}
