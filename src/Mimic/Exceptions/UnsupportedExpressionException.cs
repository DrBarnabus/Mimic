using System.Runtime.Serialization;

namespace Mimic.Exceptions;

[PublicAPI]
public sealed class UnsupportedExpressionException : MimicException
{
    public override string Identifier => "mimic_unsupported_expression";

    public Expression Expression { get; }

    public UnsupportedReason Reason { get; }

    public UnsupportedExpressionException(Expression expression, UnsupportedReason reason = UnsupportedReason.Unknown)
        : base($"Expression ({expression}) is currently unsupported. Reason: {reason}")
    {
        Expression = expression;
    }

    public UnsupportedExpressionException(Expression expression, string expressionRepresentation, UnsupportedReason reason = UnsupportedReason.Unknown)
        : base($"Expression ({expressionRepresentation}) is unsupported. Reason: {reason}")
    {
        Expression = expression;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Expression), Expression);
        info.AddValue(nameof(Reason), Reason);
    }

    public enum UnsupportedReason : byte
    {
        Unknown,
        MemberNotInterceptable,
        ExpressionThrewAnException,
        UnableToDetermineArgumentMatchers
    }
}
