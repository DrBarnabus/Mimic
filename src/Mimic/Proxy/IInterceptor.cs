namespace Mimic.Proxy;

internal interface IInterceptor
{
    void Intercept(IInvocation invocation);
}
