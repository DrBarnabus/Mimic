using JetBrains.Annotations;
using Mimic.Setup.Fluent;

namespace Mimic;

[PublicAPI]
public static partial class ReturnsExtensions
{
    #region Task<T>

    public static IReturnsResult<TMimic> Returns<TMimic, TResult>(this IReturns<TMimic, Task<TResult>> mimic, TResult value)
        where TMimic : class
    {
        return mimic.Returns(() => Task.FromResult(value));
    }

    public static IReturnsResult<TMimic> Returns<TMimic, TResult>(this IReturns<TMimic, Task<TResult>> mimic, Func<TResult> valueFunction)
        where TMimic : class
    {
        return mimic.Returns(() => Task.FromResult(valueFunction()));
    }

    public static IReturnsResult<TMimic> Returns<TMimic, T, TResult>(this IReturns<TMimic, Task<TResult>> mimic, Func<T, TResult> valueFunction)
        where TMimic : class
    {
        return mimic.Returns((T t) => Task.FromResult(valueFunction(t)));
    }

    #endregion

    #region ValueTask<T>

    public static IReturnsResult<TMimic> Returns<TMimic, TResult>(this IReturns<TMimic, ValueTask<TResult>> mimic, TResult value)
        where TMimic : class
    {
        return mimic.Returns(() => new ValueTask<TResult>(value));
    }

    public static IReturnsResult<TMimic> Returns<TMimic, TResult>(this IReturns<TMimic, ValueTask<TResult>> mimic, Func<TResult> valueFunction)
        where TMimic : class
    {
        return mimic.Returns(() => new ValueTask<TResult>(valueFunction()));
    }

    public static IReturnsResult<TMimic> Returns<TMimic, T, TResult>(this IReturns<TMimic, ValueTask<TResult>> mimic, Func<T, TResult> valueFunction)
        where TMimic : class
    {
        return mimic.Returns((T t) => new ValueTask<TResult>(valueFunction(t)));
    }

    #endregion
}
