using System.Reflection;
using Castle.DynamicProxy;
using Mimic.Core;

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

    private sealed class Invocation
        : IInvocation
    {
        // ReSharper disable once NotAccessedField.Local
        private Castle.DynamicProxy.IInvocation? _underlyingInvocation;
        private object? _returnValue;

        public Type ProxyType { get; }

        public MethodInfo Method { get; }

        public object[] Arguments { get; }

        public object? ReturnValue => _returnValue is ExceptionReturnValue ? null : _returnValue;

        public Exception? Exception => _returnValue is ExceptionReturnValue r ? r.Exception : null;

        public Invocation(Castle.DynamicProxy.IInvocation underlyingInvocation)
        {
            _underlyingInvocation = underlyingInvocation;

            ProxyType = underlyingInvocation.Proxy.GetType();
            Method = underlyingInvocation.Method;
            Arguments = underlyingInvocation.Arguments;
        }

        public void SetReturnValue(object? returnValue)
        {
            Guard.Assert(_returnValue is null);

            _returnValue = returnValue;
        }

        public void SetException(Exception exception)
        {
            Guard.Assert(_returnValue is null);

            _returnValue = new ExceptionReturnValue(exception);
        }

        public void Detatch()
        {
            _underlyingInvocation = null;
        }

        private record struct ExceptionReturnValue(Exception Exception);
    }
}
