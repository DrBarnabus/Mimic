using Mimic.Setup;

namespace Mimic.Proxy;

internal abstract class Invocation : IInvocation
{
    private MethodInfo? _methodImplementation;
    private object? _result;

    public Type ProxyType { get; }

    public MethodInfo Method { get; }

    public object?[] Arguments { get; }

    public SetupBase? MatchedSetup { get; private set; }

    public bool Verified { get; private set; }

    public object? ReturnValue => _result is ReturnValueResult rv ? rv.Object : null;

    public Exception? Exception => _result is ExceptionResult rv ? rv.Exception : null;

    IReadOnlyList<object?> IInvocation.Arguments => Arguments.ToArray();

    public MethodInfo MethodImplementation => _methodImplementation ??= Method.GetImplementingMethod(ProxyType);

    protected Invocation(Type proxyType, MethodInfo method, object?[] arguments)
    {
        Guard.Assert(proxyType is not null && method is not null && arguments is not null);

        ProxyType = proxyType;
        Method = method;
        Arguments = arguments;
    }

    public abstract object? Proceed();

    public void MarkMatchedBy(SetupBase setup)
    {
        Guard.Assert(MatchedSetup is null);
        MatchedSetup = setup;
    }

    public void MarkVerified() => Verified = true;

    public void MarkVerified(Predicate<SetupBase> predicate)
    {
        if (MatchedSetup != null && predicate(MatchedSetup))
            Verified = true;
    }

    public void SetReturnValue(object? returnValue)
    {
        Guard.Assert(_result is null);
        _result = new ReturnValueResult(returnValue);
    }

    public void SetException(Exception exception)
    {
        Guard.Assert(_result is null);
        _result = new ExceptionResult(exception);
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
        else if (valueType.IsArray ||
                 (valueType.IsConstructedGenericType && valueType.GetGenericTypeDefinition() == typeof(List<>)))
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

            (enumerator as IDisposable)?.Dispose();

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

    private readonly record struct ReturnValueResult(object? Object);

    private readonly record struct ExceptionResult(Exception Exception);
}
