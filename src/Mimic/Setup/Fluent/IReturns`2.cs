using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturns<TMimic, in TResult> : IFluent
    where TMimic : class
{
    IReturnsResult<TMimic> Returns(TResult? value);

    IReturnsResult<TMimic> Returns(Func<TResult?> valueFunction);

    IReturnsResult<TMimic> Returns<T>(Func<T, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2>(Func<T1, T2, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3>(Func<T1, T2, T3, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFunction);

    IReturnsResult<TMimic> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> valueFunction);
}
