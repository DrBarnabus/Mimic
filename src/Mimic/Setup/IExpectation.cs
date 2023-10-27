using System.Linq.Expressions;
using Mimic.Proxy;

namespace Mimic.Setup;

internal interface IExpectation
{
    LambdaExpression Expression { get; }

    bool MatchesInvocation(IInvocation invocation);
}
