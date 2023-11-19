namespace Mimic;

[PublicAPI]
public static partial class ReturnsExtensions
{
    #region Task<T>

    public static IReturnsResult Returns<TResult>(this IReturns<Task<TResult>> mimic, TResult value)
    {
        return mimic.Returns(() => Task.FromResult(value));
    }

    public static IReturnsResult Returns<TResult>(this IReturns<Task<TResult>> mimic, Func<TResult> valueFunction)
    {
        return mimic.Returns(() => Task.FromResult(valueFunction()));
    }

    public static IReturnsResult Returns<T, TResult>(this IReturns<Task<TResult>> mimic, Func<T, TResult> valueFunction)
    {
        return mimic.Returns((T t) => Task.FromResult(valueFunction(t)));
    }

    #endregion

    #region ValueTask<T>

    public static IReturnsResult Returns<TResult>(this IReturns<ValueTask<TResult>> mimic, TResult value)
    {
        return mimic.Returns(() => new ValueTask<TResult>(value));
    }

    public static IReturnsResult Returns<TResult>(this IReturns<ValueTask<TResult>> mimic, Func<TResult> valueFunction)
    {
        return mimic.Returns(() => new ValueTask<TResult>(valueFunction()));
    }

    public static IReturnsResult Returns<T, TResult>(this IReturns<ValueTask<TResult>> mimic, Func<T, TResult> valueFunction)
    {
        return mimic.Returns((T t) => new ValueTask<TResult>(valueFunction(t)));
    }

    #endregion
}
