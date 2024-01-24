using Mimic.Proxy;

namespace Mimic.UnitTests.Fixtures;

internal sealed class InterceptorFixture : IInterceptor
{
    private int _interceptCount;

    public Action<Invocation>? Callback { get; set; }

    public int InterceptCount => _interceptCount;

    public bool Intercepted => InterceptCount > 0;

    public InterceptorFixture(Action<Invocation>? callback = null)
    {
        Callback = callback;
    }

    public void Intercept(Invocation invocation)
    {
        Interlocked.Increment(ref _interceptCount);

        Callback?.Invoke(invocation);
    }
}
