namespace Mimic.Setup.ArgumentMatchers;

internal sealed class ArgumentMatcherObserver : IDisposable
{
    [ThreadStatic]
    private static Stack<ArgumentMatcherObserver>? _observers;

    private readonly List<Observation> _observations = [];
    private int _counter;

    internal IReadOnlyList<Observation> Observations => _observations;

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
        _observations.Add(new Observation(GetCounter(), argumentMatcher));
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

    internal int GetCounter() => Interlocked.Increment(ref _counter);

    internal IEnumerable<ArgumentMatcher> GetArgumentMatchersBetween(int fromCounter, int toCounter)
        => _observations.Where(o => fromCounter <= o.Counter && o.Counter < toCounter).Select(o => o.ArgumentMatcher);

    public void Dispose()
    {
        Guard.Assert(_observers is { Count: > 0 });
        _observers.Pop();
    }

    internal record struct Observation(int Counter, ArgumentMatcher ArgumentMatcher);
}
