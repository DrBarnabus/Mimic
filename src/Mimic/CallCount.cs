namespace Mimic;

public readonly struct CallCount : IEquatable<CallCount>
{
    private readonly Type _type;
    private readonly int _from;
    private readonly int _to;

    private CallCount(Type type, int from, int to)
    {
        _type = type;
        _from = from;
        _to = to;
    }

    public static CallCount AtLeastOnce() => new(Type.AtLeastOnce, 1, int.MaxValue);

    public static CallCount AtLeast(int count)
    {
        Guard.Assert(count >= 1);
        return new CallCount(Type.AtLeast, count, int.MaxValue);
    }

    public static CallCount AtMost(int count)
    {
        Guard.Assert(count >= 1);
        return new CallCount(Type.AtMost, 0, count);
    }

    public static CallCount AtMostOnce() => new(Type.AtMostOnce, 0, 1);

    public static CallCount InclusiveBetween(int from, int to)
    {
        Guard.Assert(from >= 0 && to >= from);
        return new CallCount(Type.InclusiveBetween, from, to);
    }

    public static CallCount ExclusiveBetween(int from, int to)
    {
        Guard.Assert(from > 0 && to > from);
        Guard.Assert(to - from != 1);

        return new CallCount(Type.ExclusiveBetween, from + 1, to - 1);
    }

    public static CallCount Exactly(int count)
    {
        Guard.Assert(count > 0);
        return new CallCount(Type.Exactly, count, count);
    }

    public static CallCount Once() => new(Type.Once, 1, 1);

    public static CallCount Never() => new(Type.Never, 0, 0);

    public bool Validate(int count) => _from <= count && count <= _to;

    public bool Equals(CallCount other) => _from == other._from && _to == other._to;

    public override bool Equals(object? obj) => obj is CallCount other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(_from, _to);

    public static bool operator ==(CallCount left, CallCount right) => left.Equals(right);

    public static bool operator !=(CallCount left, CallCount right) => !left.Equals(right);

    internal string? GetExceptionMessage(int actualCallCount)
    {
        if (Validate(actualCallCount))
            return null;

        (int from, int to) = (_from, _to);

        if (_type is Type.ExclusiveBetween)
        {
            --from;
            ++to;
        }

        return _type switch
        {
            Type.AtLeastOnce => "Expected at least one invocation on the mimic, but it was never invoked: ",
            Type.AtLeast => $"Expected at least {from} invocation(s) on the mimic, but it was invoked {actualCallCount} time(s): ",
            Type.AtMost => $"Expected at most {to} invocation(s) on the mimic, but it was invoked {actualCallCount} time(s): ",
            Type.AtMostOnce => $"Expected at most one invocation on the mimic, but it was invoked {actualCallCount} times: ",
            Type.InclusiveBetween => $"Expected between {from} and {to} invocations (Inclusive) on the mimic, but it was invoked {actualCallCount} times: ",
            Type.ExclusiveBetween => $"Expected between {from} and {to} invocations (Exclusive) on the mimic, but it was invoked {actualCallCount} times: ",
            Type.Exactly => $"Expected exactly {from} invocation(s) on the mimic, but it was invoked {actualCallCount} time(s): ",
            Type.Once => $"Expected a single invocation on the mimic, but it was invoked {actualCallCount} times: ",
            Type.Never => $"Expected zero invocations on the mimic, but it was invoked {actualCallCount} time(s): ",
            _ => throw new InvalidOperationException()
        };
    }

    private enum Type
    {
        AtLeastOnce,
        AtLeast,
        AtMost,
        AtMostOnce,
        InclusiveBetween,
        ExclusiveBetween,
        Exactly,
        Once,
        Never
    }
};
