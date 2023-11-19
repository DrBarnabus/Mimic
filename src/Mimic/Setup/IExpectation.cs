namespace Mimic.Setup;

internal interface IExpectation
{
    LambdaExpression Expression { get; }

    bool MatchesInvocation(IInvocation invocation);
}
