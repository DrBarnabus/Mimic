﻿using System.Reflection;
using Mimic.Proxy;

namespace Mimic.UnitTests.Fixtures;

internal sealed class InvocationFixture : Invocation
{
    private static readonly MethodInfo DefaultMethod = typeof(InvocationFixture).GetMethod("ToString")!;

    public InvocationFixture(MethodInfo? method = null)
        : base(typeof(InvocationFixture), method ?? DefaultMethod, Array.Empty<object?>())
    {
    }
}
