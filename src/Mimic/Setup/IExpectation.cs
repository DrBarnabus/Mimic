namespace Mimic.Setup;

internal interface IExpectation
{
    LambdaExpression Expression { get; }

    bool MatchesInvocation(Invocation invocation);
}
