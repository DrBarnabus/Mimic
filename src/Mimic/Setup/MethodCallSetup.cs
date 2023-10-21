using System.Linq.Expressions;
using System.Reflection;
using Mimic.Core;
using Mimic.Proxy;

namespace Mimic.Setup;

internal sealed class MethodCallSetup
{
    private Flags _flags;
    private Behaviour? _returnOrThrow;

    public Expression OriginalExpression { get; }

    public IMimic Mimic { get; }

    public MethodExpectation Expectation { get; }

    public LambdaExpression Expression => Expectation.Expression;

    public MethodInfo MethodInfo => Expectation.MethodInfo;

    public bool Matched => (_flags & Flags.Matched) != 0;

    public bool Overriden => (_flags & Flags.Overriden) != 0;

    public MethodCallSetup(Expression originalExpression, IMimic mimic, MethodExpectation expectation)
    {
        OriginalExpression = originalExpression;
        Mimic = mimic;
        Expectation = expectation;
    }

    public bool MatchesInvocation(IInvocation invocation) => Expectation.MatchesInvocation(invocation);

    public void Execute(IInvocation invocation)
    {
        _flags |= Flags.Matched;

        if (_returnOrThrow is not null)
        {
            _returnOrThrow.Execute(invocation);
        }
        else if (invocation.Method.ReturnType != typeof(void))
        {
            throw new NotImplementedException(); // TODO: Create a specialized exception for this purpose
        }
    }

    public void Override()
    {
        Guard.Assert(!Overriden);
        _flags |= Flags.Overriden;
    }

    public void SetReturnValueBehaviour(object? value)
    {
        Guard.Assert(MethodInfo.ReturnType != typeof(void));
        Guard.Assert(_returnOrThrow is null);

        _returnOrThrow = new ReturnValueBehaviour(value);
    }

[Flags]
    private enum Flags : byte
    {
        None = 0,
        Matched = 1 << 0,
        Overriden = 1 << 1,
    }

    private abstract class Behaviour
    {
        internal abstract void Execute(IInvocation invocation);
    }

    private sealed class ReturnValueBehaviour : Behaviour
    {
        private readonly object? _value;

        public ReturnValueBehaviour(object? value) => _value = value;

        internal override void Execute(IInvocation invocation)
        {
            invocation.SetReturnValue(_value);
        }
    }
}
