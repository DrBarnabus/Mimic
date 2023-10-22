using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Mimic.Exceptions;

namespace Mimic.Core;

internal static partial class Guard
{
    [AssertionMethod]
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Assert(
        [AssertionCondition(AssertionConditionType.IS_FALSE)] [DoesNotReturnIf(false)] bool condition,
        [InterpolatedStringHandlerArgument("condition")] ref InterpolatedStringHandler<bool, BooleanCondition> message,
        [CallerArgumentExpression("condition")] string? expression = null)
    {
        if (!condition)
            ThrowException(message.ToStringAndClear(), expression);
    }

    [AssertionMethod]
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Assert(
        [AssertionCondition(AssertionConditionType.IS_FALSE)] [DoesNotReturnIf(false)] bool condition,
        string? message = null,
        [CallerArgumentExpression("condition")] string? expression = null)
    {
        if (!condition)
            ThrowException(message ?? "Assertion failed", expression);
    }

    [AssertionMethod]
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void NotNull(
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [System.Diagnostics.CodeAnalysis.NotNull] object? value,
        [InterpolatedStringHandlerArgument("value")] ref InterpolatedStringHandler<object?, NullCondition> message,
        [CallerArgumentExpression("value")] string? expression = null)
    {
        if (value is null)
            ThrowException(message.ToStringAndClear(), expression);
    }

    [AssertionMethod]
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void NotNull(
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [System.Diagnostics.CodeAnalysis.NotNull] object? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? expression = null)
    {
        if (value is null)
            ThrowException(message ?? $"{expression} must not be null", expression);
    }

    [AssertionMethod]
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void NotNullOrEmpty(
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [System.Diagnostics.CodeAnalysis.NotNull] string? value,
        [InterpolatedStringHandlerArgument("value")] ref InterpolatedStringHandler<string?, NullOrEmptyCondition> message,
        [CallerArgumentExpression("value")] string? expression = null)
    {
        if (value is null or "")
            ThrowException(message.ToStringAndClear(), expression);
    }

    [AssertionMethod]
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void NotNullOrEmpty(
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [System.Diagnostics.CodeAnalysis.NotNull] string? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? expression = null)
    {
        if (value is null or "")
            ThrowException(message ?? $"{expression} must not be null or empty", expression);
    }

    [DoesNotReturn]
    private static void ThrowException(string message, string? paramName)
    {
        throw new AssertionException(message, paramName);
    }

    internal sealed class AssertionException : MimicException
    {
        public override string Identifier => "mimic_assertion";

        public AssertionException(string message, string? expression)
            : base($"{message} (Expression '{expression}')")
        {
            Expression = expression;
        }

        public string? Expression { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Expression), Expression);
        }
    }
}
