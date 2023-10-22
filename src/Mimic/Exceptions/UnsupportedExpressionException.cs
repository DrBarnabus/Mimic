using System.Linq.Expressions;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mimic.Exceptions;

[PublicAPI]
public sealed class UnsupportedExpressionException : MimicException
{
    public override string Identifier => "mimic_unsupported_expression";

    public Expression Expression { get; }

    public UnsupportedReason Reason { get; }

    public UnsupportedExpressionException(Expression expression, UnsupportedReason reason = UnsupportedReason.Unknown)
        : base($"Expression ({expression}) is currently unsupported reason: {reason}")
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
        Unknown = 0,
        RefTypeParameters
    }
}
