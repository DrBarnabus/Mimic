namespace Mimic.Proxy;

internal interface IInterceptor
{
    void Intercept(Invocation invocation);
}
