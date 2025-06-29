namespace Mimic;

/// <summary>
/// Represents a call count constraint that can be used to verify the number of times a method was invoked.
/// </summary>
[PublicAPI]
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

    /// <summary>
    /// Creates a call count constraint that requires at least one invocation.
    /// </summary>
    /// <returns>A <see cref="CallCount"/> that validates if a method was called at least once.</returns>
    public static CallCount AtLeastOnce() => new(Type.AtLeastOnce, 1, int.MaxValue);

    /// <summary>
    /// Creates a call count constraint that requires at least the specified number of invocations.
    /// </summary>
    /// <param name="count">The minimum number of invocations required.</param>
    /// <returns>A <see cref="CallCount"/> that validates if a method was called at least the specified number of times.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="count"/> is less than 1.</exception>
    public static CallCount AtLeast(int count)
    {
        Guard.Assert(count >= 1);
        return new CallCount(Type.AtLeast, count, int.MaxValue);
    }

    /// <summary>
    /// Creates a call count constraint that allows at most the specified number of invocations.
    /// </summary>
    /// <param name="count">The maximum number of invocations allowed.</param>
    /// <returns>A <see cref="CallCount"/> that validates if a method was called at most the specified number of times.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="count"/> is less than 1.</exception>
    public static CallCount AtMost(int count)
    {
        Guard.Assert(count >= 1);
        return new CallCount(Type.AtMost, 0, count);
    }

    /// <summary>
    /// Creates a call count constraint that allows at most one invocation.
    /// </summary>
    /// <returns>A <see cref="CallCount"/> that validates if a method was called at most once.</returns>
    public static CallCount AtMostOnce() => new(Type.AtMostOnce, 0, 1);

    /// <summary>
    /// Creates a call count constraint that requires the number of invocations to be within the specified range (inclusive).
    /// </summary>
    /// <param name="from">The minimum number of invocations (inclusive).</param>
    /// <param name="to">The maximum number of invocations (inclusive).</param>
    /// <returns>A <see cref="CallCount"/> that validates if a method was called within the specified range.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="from"/> is negative or <paramref name="to"/> is less than <paramref name="from"/>.</exception>
    public static CallCount InclusiveBetween(int from, int to)
    {
        Guard.Assert(from >= 0 && to >= from);
        return new CallCount(Type.InclusiveBetween, from, to);
    }

    /// <summary>
    /// Creates a call count constraint that requires the number of invocations to be within the specified range (exclusive).
    /// </summary>
    /// <param name="from">The minimum number of invocations (exclusive).</param>
    /// <param name="to">The maximum number of invocations (exclusive).</param>
    /// <returns>A <see cref="CallCount"/> that validates if a method was called within the specified range.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="from"/> is not positive, <paramref name="to"/> is not greater than <paramref name="from"/>, or the difference between <paramref name="to"/> and <paramref name="from"/> is 1.</exception>
    public static CallCount ExclusiveBetween(int from, int to)
    {
        Guard.Assert(from > 0 && to > from);
        Guard.Assert(to - from != 1);

        return new CallCount(Type.ExclusiveBetween, from + 1, to - 1);
    }

    /// <summary>
    /// Creates a call count constraint that requires exactly the specified number of invocations.
    /// </summary>
    /// <param name="count">The exact number of invocations required.</param>
    /// <returns>A <see cref="CallCount"/> that validates if a method was called exactly the specified number of times.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="count"/> is not positive.</exception>
    public static CallCount Exactly(int count)
    {
        Guard.Assert(count > 0);
        return new CallCount(Type.Exactly, count, count);
    }

    /// <summary>
    /// Creates a call count constraint that requires exactly one invocation.
    /// </summary>
    /// <returns>A <see cref="CallCount"/> that validates if a method was called exactly once.</returns>
    public static CallCount Once() => new(Type.Once, 1, 1);

    /// <summary>
    /// Creates a call count constraint that requires zero invocations.
    /// </summary>
    /// <returns>A <see cref="CallCount"/> that validates if a method was never called.</returns>
    public static CallCount Never() => new(Type.Never, 0, 0);

    /// <summary>
    /// Validates whether the specified call count satisfies this constraint.
    /// </summary>
    /// <param name="count">The actual number of invocations to validate.</param>
    /// <returns><c>true</c> if the call count satisfies this constraint; otherwise, <c>false</c>.</returns>
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
