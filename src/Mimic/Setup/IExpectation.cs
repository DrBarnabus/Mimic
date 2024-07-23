namespace Mimic.Setup;

internal interface IExpectation : IEquatable<IExpectation>
{
    LambdaExpression Expression { get; }

    bool MatchesInvocation(Invocation invocation);
}
