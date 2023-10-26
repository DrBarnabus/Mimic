using System.Linq.Expressions;

namespace Mimic.Setup.ArgumentMatchers;

internal sealed class ArgumentMatcherExpression : Expression
{
    internal ArgumentMatcher ArgumentMatcher { get; }

    public ArgumentMatcherExpression(ArgumentMatcher argumentMatcher) => ArgumentMatcher = argumentMatcher;

    public override ExpressionType NodeType => ExpressionType.Extension;

    public override Type Type => ArgumentMatcher.Type;

    public override bool CanReduce => false;

    protected override Expression VisitChildren(ExpressionVisitor visitor) => this;
}
