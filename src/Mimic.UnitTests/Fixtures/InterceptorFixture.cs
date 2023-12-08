using Mimic.Proxy;

namespace Mimic.UnitTests.Fixtures;

internal sealed class InterceptorFixture : IInterceptor
{
    private int _interceptCount;

    public Action<IInvocation>? Callback { get; set; }

    public int InterceptCount => _interceptCount;

    public bool Intercepted => InterceptCount > 0;

    public InterceptorFixture(Action<IInvocation>? callback = null)
    {
        Callback = callback;
    }

    public void Intercept(IInvocation invocation)
    {
        Interlocked.Increment(ref _interceptCount);

        Callback?.Invoke(invocation);
    }
}
