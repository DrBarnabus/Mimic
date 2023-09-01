using Mimic.Proxy;

namespace Mimic;

public partial class Mimic<T>
    : IInterceptor
{
    void IInterceptor.Intercept(IInvocation invocation)
    {
        if (invocation.Method is { IsSpecialName: true, Name: $"get_{nameof(IMimicked<T>.Mimic)}" }
            && typeof(IMimicked<T>).IsAssignableFrom(invocation.Method.DeclaringType))
        {
            invocation.SetReturnValue(this);
            return;
        }

        Console.WriteLine($"{invocation.Method} invoked");
    }
}
