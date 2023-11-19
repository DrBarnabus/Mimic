namespace Mimic.Core;

internal partial class Guard
{
    [InterpolatedStringHandler]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal ref struct InterpolatedStringHandler<TValue, TCondition>
        where TCondition : ICondition<TValue>, new()
    {
        private static readonly TCondition Condition = new();

        private DefaultInterpolatedStringHandler _innerHandler;

        internal InterpolatedStringHandler(int literalLength, int formattedCount, TValue value, out bool shouldAppend)
        {
            if (!Condition.Check(value))
            {
                _innerHandler = default;
                shouldAppend = false;
                return;
            }

            _innerHandler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
            shouldAppend = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendLiteral(string value)
        {
            _innerHandler.AppendLiteral(value);
        }

        internal void AppendFormatted<T>(T value)
        {
            _innerHandler.AppendFormatted(value);
        }

        internal void AppendFormatted<T>(T value, string? format)
        {
            _innerHandler.AppendFormatted(value, format);
        }

        internal void AppendFormatted<T>(T value, int alignment)
        {
            _innerHandler.AppendFormatted(value, alignment);
        }

        internal void AppendFormatted<T>(T value, int alignment, string? format)
        {
            _innerHandler.AppendFormatted(value, alignment, format);
        }

        internal void AppendFormatted(ReadOnlySpan<char> value)
        {
            _innerHandler.AppendFormatted(value);
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        internal void AppendFormatted(ReadOnlySpan<char> value, int alignment = 0, string? format = null)
        {
            _innerHandler.AppendFormatted(value, alignment, format);
        }

        internal void AppendFormatted(string? value)
        {
            _innerHandler.AppendFormatted(value);
        }

        internal string ToStringAndClear()
        {
            return _innerHandler.ToStringAndClear();
        }
    }

    #region Conditions

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal interface ICondition<in TValue>
    {
        bool Check(TValue value);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal struct BooleanCondition : ICondition<bool>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Check(bool value) => !value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal struct NullCondition : ICondition<object?>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Check(object? value) => value is null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal struct NullOrEmptyCondition : ICondition<string?>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Check(string? value) => value is null or "";
    }

    #endregion
}
