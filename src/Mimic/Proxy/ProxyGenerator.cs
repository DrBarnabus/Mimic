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
            finally
            {
                invocation.Detatch();
            }
        }
    }

    private sealed class Invocation : IInvocation
    {
        private Castle.DynamicProxy.IInvocation? _underlyingInvocation;
        private object? _returnValue;
        private MethodInfo? _methodImplementation;

        public Type ProxyType { get; }

        public MethodInfo Method { get; }

        public MethodInfo MethodImplementation => _methodImplementation ??= Method.GetImplementingMethod(ProxyType);

        public object?[] Arguments { get; }

        public object? ReturnValue => _returnValue;

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

        public void Detatch()
        {
            _underlyingInvocation = null;
        }

        public override string ToString()
        {
            var stringBuilder = new ValueStringBuilder(stackalloc char[256]);

            TypeNameFormatter.AppendFormattedTypeName(ref stringBuilder, Method.DeclaringType!);
            stringBuilder.Append('.');

            if (Method.IsGetter())
            {
                stringBuilder.Append(Method.Name.AsSpan(4));
            }
            else if (Method.IsSetter())
            {
                stringBuilder.Append(Method.Name.AsSpan(4));
                stringBuilder.Append(" = ".AsSpan());
                AppendValue(ref stringBuilder, Arguments[0]);
            }
            else
            {
                stringBuilder.Append(Method.Name.AsSpan());
                if (Method.IsGenericMethod)
                {
                    stringBuilder.Append('<');

                    var genericArguments = Method.GetGenericArguments();
                    for (int i = 0; i < genericArguments.Length; i++)
                    {
                        if (i > 0)
                            stringBuilder.Append(", ".AsSpan());

                        TypeNameFormatter.AppendFormattedTypeName(ref stringBuilder, genericArguments[i]);
                    }

                    stringBuilder.Append('>');
                }

                stringBuilder.Append('(');

                for (int i = 0; i < Arguments.Length; i++)
                {
                    if (i > 0)
                        stringBuilder.Append(", ".AsSpan());

                    AppendValue(ref stringBuilder, Arguments[i]);
                }

                stringBuilder.Append(')');
            }

            return stringBuilder.ToString();
        }

        private static void AppendValue(ref ValueStringBuilder stringBuilder, object? value)
        {
            if (value is null)
            {
                stringBuilder.Append("null".AsSpan());
                return;
            }

            if (value is string stringValue)
            {
                stringBuilder.Append('"');
                stringBuilder.Append(stringValue.AsSpan());
                stringBuilder.Append('"');
                return;
            }

            var valueType = value.GetType();
            if (valueType.IsEnum)
            {
                TypeNameFormatter.AppendFormattedTypeName(ref stringBuilder, valueType);
                stringBuilder.Append('.');
                stringBuilder.Append(value.ToString());
            }
            else if (valueType.IsArray || (valueType.IsConstructedGenericType && valueType.GetGenericTypeDefinition() == typeof(List<>)))
            {
                stringBuilder.Append('[');

                var enumerator = ((IEnumerable)value).GetEnumerator();
                for (int i = 0; enumerator.MoveNext() && i <= 10; ++i)
                {
                    if (i > 0)
                        stringBuilder.Append(", ".AsSpan());

                    if (i == 10)
                    {
                        stringBuilder.Append("...".AsSpan());
                        break;
                    }

                    AppendValue(ref stringBuilder, enumerator.Current);
                }

                stringBuilder.Append(']');
            }
            else
            {
                string? formattedValue = value.ToString();
                if (formattedValue is null || formattedValue == valueType.ToString())
                    TypeNameFormatter.AppendFormattedTypeName(ref stringBuilder, valueType);
                else
                    stringBuilder.Append(formattedValue);
            }
        }
    }
}
