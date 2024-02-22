using Castle.DynamicProxy;

namespace Mimic.Proxy;

internal sealed class ProxyGenerator
{
    public static readonly ProxyGenerator Instance = new();

    private readonly Castle.DynamicProxy.ProxyGenerator _proxyGenerator = new();
    private readonly ProxyGenerationOptions _proxyGenerationOptions = new()
    {
        Hook = new AllMethodsIncludingObjectHook(),
        BaseTypeForInterfaceProxy = typeof(InterfaceProxyBase)
    };

    public object GenerateProxy(Type mimicType, Type[] additionalInterfaces, object[] constructorArguments, IInterceptor interceptor)
    {
        if (mimicType.IsInterface)
            return _proxyGenerator.CreateInterfaceProxyWithoutTarget(
                mimicType,
                [typeof(IProxy), ..additionalInterfaces],
                _proxyGenerationOptions,
                new DynamicProxyInterceptor(interceptor));

        try
        {
            return _proxyGenerator.CreateClassProxy(
                mimicType,
                [typeof(IProxy), ..additionalInterfaces],
                _proxyGenerationOptions,
                constructorArguments,
                new DynamicProxyInterceptor(interceptor));
        }
        catch (ArgumentException ex) when (ex.InnerException is MissingMethodException)
        {
            throw MimicException.NoConstructorWithMatchingArguments(mimicType, ex);
        }
        catch (Exception ex)
        {
            throw MimicException.TypeCannotBeMimicked(mimicType, ex);
        }
    }

    public static void ThrowIfMethodIsInaccessible(MethodInfo method)
    {
        if (!ProxyUtil.IsAccessible(method, out string message))
            throw MimicException.MethodNotAccessibleByProxyGenerator(method, message);
    }

    private sealed class AllMethodsIncludingObjectHook : AllMethodsHook
    {
        public override bool ShouldInterceptMethod(Type type, MethodInfo method) =>
            base.ShouldInterceptMethod(type, method) || IsDesiredObjectMethod(method);

        private static bool IsDesiredObjectMethod(MethodInfo method) =>
            method.DeclaringType == typeof(object) && method.Name is nameof(ToString) or nameof(Equals) or nameof(GetHashCode);
    }

    private sealed class DynamicProxyInterceptor
        : Castle.DynamicProxy.IInterceptor
    {
        private static readonly MethodInfo ProxyInterceptorPropertyGetter = typeof(IProxy).GetProperty(nameof(IProxy.Interceptor))!.GetMethod!;

        private readonly IInterceptor _interceptor;

        public DynamicProxyInterceptor(IInterceptor interceptor)
        {
            _interceptor = interceptor;
        }

        public void Intercept(Castle.DynamicProxy.IInvocation underlyingInvocation)
        {
            // Special case for `IProxy.Interceptor` which all proxied types must implement
            if (underlyingInvocation.Method == ProxyInterceptorPropertyGetter)
            {
                underlyingInvocation.ReturnValue = _interceptor;
                return;
            }

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
