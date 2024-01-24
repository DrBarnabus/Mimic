namespace Mimic.Proxy;

internal interface IProxy
{
    IInterceptor Interceptor { get; }
}
