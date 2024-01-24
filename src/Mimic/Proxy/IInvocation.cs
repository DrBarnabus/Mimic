namespace Mimic.Proxy;

public interface IInvocation
{
    MethodInfo Method { get; }

    IReadOnlyList<object?> Arguments { get; }

    bool Verified { get; }

    object? ReturnValue { get; }

    Exception? Exception { get; }
}
