using Mimic.Expressions;

namespace Mimic.Setup;

internal abstract class SetupBase
{
    private Flags _flags;

    public Expression? OriginalExpression { get; }

    public IMimic Mimic { get; }

    public IExpectation Expectation { get; }

    public LambdaExpression Expression => Expectation.Expression;

    public bool Matched => (_flags & Flags.Matched) != 0;

    public bool Overriden => (_flags & Flags.Overriden) != 0;

    public bool Expected => (_flags & Flags.Expected) != 0;

    protected SetupBase(Expression? originalExpression, IMimic mimic, IExpectation expectation)
    {
        OriginalExpression = originalExpression;
        Mimic = mimic;
        Expectation = expectation;
    }

    public virtual bool MatchesInvocation(Invocation invocation) => Expectation.MatchesInvocation(invocation);

    public void Execute(Invocation invocation)
    {
        _flags |= Flags.Matched;

        invocation.MarkMatchedBy(this);

        ExecuteCore(invocation);
    }

    protected abstract void ExecuteCore(Invocation invocation);

    public void Override()
    {
        Guard.Assert(!Overriden);
        _flags |= Flags.Overriden;
    }

    public void FlagAsExpected() => _flags |= Flags.Expected;

    internal virtual void VerifyMatched()
    {
        if (!Matched)
            throw MimicException.ExpectedSetupNotMatched(this);
    }

    public override string ToString()
    {
        var type = Expression.Parameters[0].Type;
        var partiallyEvaluatedExpression = ExpressionEvaluator.PartiallyEvaluate(Expression, true);

        var stringBuilder = new ValueStringBuilder(stackalloc char[256]);

        TypeNameFormatter.AppendFormattedTypeName(ref stringBuilder, type);
        stringBuilder.Append(' ');
        stringBuilder.Append(partiallyEvaluatedExpression.ToString());

        return stringBuilder.ToString();
    }

    [Flags]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private enum Flags : byte
    {
        None = 0,
        Matched = 1 << 0,
        Overriden = 1 << 1,
        Expected = 1 << 2
    }
}
