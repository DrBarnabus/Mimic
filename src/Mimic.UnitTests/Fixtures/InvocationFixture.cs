using System.Reflection;
using Mimic.Proxy;

namespace Mimic.UnitTests.Fixtures;

public sealed class InvocationFixture : IInvocation
{
    public Type ProxyType { get; set; } = default!;

    public MethodInfo Method { get; set; } = default!;

    public MethodInfo MethodImplementation { get; set; } = default!;

    public object?[] Arguments { get; set; } = default!;

    public void SetReturnValue(object? returnValue) => ReturnValue = returnValue;

    public void SetException(Exception exception) => Exception = exception;

    public object? ReturnValue { get; private set; }

    public Exception? Exception { get; private set; }
}
