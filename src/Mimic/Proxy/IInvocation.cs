﻿namespace Mimic.Proxy;

internal interface IInvocation
{
    public Type ProxyType { get; }

    MethodInfo Method { get; }

    MethodInfo MethodImplementation { get; }

    object?[] Arguments { get; }

    public void SetReturnValue(object? returnValue);
}
