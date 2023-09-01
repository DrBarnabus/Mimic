using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Mimic.Core;

internal static class Guard
{
    [AssertionMethod]
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Assert(
        [AssertionCondition(AssertionConditionType.IS_FALSE), DoesNotReturnIf(false)]
        bool condition,
        [InterpolatedStringHandlerArgument("condition")]
        ref InterpolatedStringHandler message,
        [CallerArgumentExpression("condition")]
        string? expression = null)
    {
        if (!condition)
            ThrowException(message.ToStringAndClear(), expression);
    }

    [AssertionMethod]
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Assert(
        [AssertionCondition(AssertionConditionType.IS_FALSE), DoesNotReturnIf(false)]
        bool condition,
        [CallerArgumentExpression("condition")]
        string? expression = null)
    {
        if (!condition)
            ThrowException("Assertion failed", expression);
    }

    [DoesNotReturn]
    private static void ThrowException(string message, string? paramName) =>
        throw new AssertionException(message, paramName);

    private sealed class AssertionException : Exception
    {
        public AssertionException(string message, string? expression)
            : base($"{message} (Expression '{expression}')")
        {
            Expression = expression;
        }

        public string? Expression { get; }
    }

    [InterpolatedStringHandler]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal ref struct InterpolatedStringHandler
    {
        private DefaultInterpolatedStringHandler _handler;

        // ReSharper disable once UnusedMember.Global
        public InterpolatedStringHandler(int literalLength, int formattedCount, bool condition, out bool shouldAppend)
        {
            if (condition)
            {
                _handler = default;
                shouldAppend = false;
            }
            else
            {
                _handler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
                shouldAppend = true;
            }
        }

        public string ToStringAndClear() =>
            _handler.ToStringAndClear();

        public void AppendLiteral(string value) =>
            _handler.AppendLiteral(value);

        public void AppendFormatted<T>(T value) =>
            _handler.AppendFormatted(value);

        public void AppendFormatted<T>(T value, string? format) =>
            _handler.AppendFormatted(value, format);

        public void AppendFormatted<T>(T value, int alignment) =>
            _handler.AppendFormatted(value, alignment);

        public void AppendFormatted<T>(T value, int alignment, string? format) =>
            _handler.AppendFormatted(value, alignment, format);

        public void AppendFormatted(ReadOnlySpan<char> value) =>
            _handler.AppendFormatted(value);

        // ReSharper disable once MethodOverloadWithOptionalParameter
        public void AppendFormatted(ReadOnlySpan<char> value, int alignment = 0, string? format = null) =>
            _handler.AppendFormatted(value, alignment, format);

        public void AppendFormatted(string? value) =>
            _handler.AppendFormatted(value);

        // ReSharper disable once MethodOverloadWithOptionalParameter
        public void AppendFormatted(string? value, int alignment = 0, string? format = null) =>
            _handler.AppendFormatted(value, alignment, format);

        public void AppendFormatted(object? value, int alignment = 0, string? format = null) =>
            _handler.AppendFormatted(value, alignment, format);
    }
}
