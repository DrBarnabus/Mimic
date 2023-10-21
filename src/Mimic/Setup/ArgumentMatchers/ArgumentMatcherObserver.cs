﻿using System.Diagnostics.CodeAnalysis;
using Mimic.Core;

namespace Mimic.Setup.ArgumentMatchers;

internal sealed class ArgumentMatcherObserver : IDisposable
{
    [ThreadStatic]
    private static Stack<ArgumentMatcherObserver>? _observers;

    private readonly List<Observation> _observations = new();
    private int _counter;

    internal static ArgumentMatcherObserver ActivateObserver()
    {
        var observer = new ArgumentMatcherObserver();

        _observers ??= new Stack<ArgumentMatcherObserver>();
        _observers.Push(observer);

        return observer;
    }

    internal static bool HasActiveObserver([NotNullWhen(true)] out ArgumentMatcherObserver? activeObserver)
    {
        if (_observers is { Count: > 0 })
        {
            activeObserver = _observers.Peek();
            return true;
        }

        activeObserver = default;
        return false;
    }

    internal void AddArgumentMatcher(ArgumentMatcher argumentMatcher)
    {
        _observations.Add(new Observation(Interlocked.Increment(ref _counter), argumentMatcher));
    }

    internal bool TryGetLastArgumentMatcher([NotNullWhen(true)] out ArgumentMatcher? lastArgumentMatcher)
    {
        if (_observations is { Count: > 0 })
        {
            lastArgumentMatcher = _observations[^1].ArgumentMatcher;
            return true;
        }

        lastArgumentMatcher = default;
        return false;
    }

    public void Dispose()
    {
        Guard.Assert(_observers is { Count: > 0 });
        _observers.Pop();
    }

    private record struct Observation(int Counter, ArgumentMatcher ArgumentMatcher);
}