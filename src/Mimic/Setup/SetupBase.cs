using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Mimic.Core;
using Mimic.Exceptions;
using Mimic.Proxy;

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

    public bool Verifiable => (_flags & Flags.Verifiable) != 0;

    protected SetupBase(Expression? originalExpression, IMimic mimic, IExpectation expectation)
    {
        OriginalExpression = originalExpression;
        Mimic = mimic;
        Expectation = expectation;
    }

    public bool MatchesInvocation(IInvocation invocation) => Expectation.MatchesInvocation(invocation);

    public void Execute(IInvocation invocation)
    {
        _flags |= Flags.Matched;

        ExecuteCore(invocation);
    }

    protected abstract void ExecuteCore(IInvocation invocation);

    public void Override()
    {
        Guard.Assert(!Overriden);
        _flags |= Flags.Overriden;
    }

    public void FlagAsVerifiable()
    {
        _flags |= Flags.Verifiable;
    }

    internal virtual void Verify()
    {
        if (!Matched)
        {
            throw MimicException.SetupNotMatched(this);
        }
    }

    [Flags]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private enum Flags : byte
    {
        None = 0,
        Matched = 1 << 0,
        Overriden = 1 << 1,
        Verifiable = 1 << 2
    }
}
