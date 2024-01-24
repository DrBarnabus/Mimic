using Castle.DynamicProxy;

namespace Mimic.Proxy;

internal sealed class ProxyGenerator
{
    public static readonly ProxyGenerator Instance = new();

    private readonly ProxyGenerationOptions _proxyGenerationOptions = new();
    private readonly Castle.DynamicProxy.ProxyGenerator _proxyGenerator = new();

    public object GenerateProxy(Type mimicType, Type[] additionalInterfacesToProxy, IInterceptor interceptor)
    {
        Guard.Assert(mimicType.IsInterface, $"{TypeNameFormatter.GetFormattedName(mimicType)} is not an interface");

        return _proxyGenerator.CreateInterfaceProxyWithoutTarget(
            mimicType,
            additionalInterfacesToProxy,
            _proxyGenerationOptions,
            new DynamicProxyInterceptor(interceptor));
    }

    private sealed class DynamicProxyInterceptor
        : Castle.DynamicProxy.IInterceptor
    {
        private readonly IInterceptor _interceptor;

        public DynamicProxyInterceptor(IInterceptor interceptor)
        {
            _interceptor = interceptor;
        }

        public void Intercept(Castle.DynamicProxy.IInvocation underlyingInvocation)
        {
            var invocation = new Invocation(underlyingInvocation);

            try
            {
                _interceptor.Intercept(invocation);
                underlyingInvocation.ReturnValue = invocation.ReturnValue;
            }
            catch (Exception ex)
            {
                invocation.SetException(ex);
                throw;
            }
            finally
            {
                invocation.Detatch();
            }
        }
    }

    private sealed class Invocation : Mimic.Proxy.Invocation
    {
        private Castle.DynamicProxy.IInvocation? _underlyingInvocation;

        public Invocation(Castle.DynamicProxy.IInvocation underlyingInvocation)
            : base(underlyingInvocation.Proxy.GetType(), underlyingInvocation.Method, underlyingInvocation.Arguments)
        {
            _underlyingInvocation = underlyingInvocation;
        }

        public void Detatch()
        {
            _underlyingInvocation = null;
        }
    }
}
