using Mimic.Setup.ArgumentMatchers;

namespace Mimic.Expressions;

internal sealed class ArgumentMatcherExpression : Expression
{
    internal ArgumentMatcher ArgumentMatcher { get; }

    public override ExpressionType NodeType => ExpressionType.Extension;

    public override Type Type => ArgumentMatcher.Type;

    public override bool CanReduce => false;

    public ArgumentMatcherExpression(ArgumentMatcher argumentMatcher) => ArgumentMatcher = argumentMatcher;

    protected override Expression VisitChildren(ExpressionVisitor visitor) => this;
}
