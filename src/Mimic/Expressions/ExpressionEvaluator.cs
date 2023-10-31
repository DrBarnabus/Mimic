using System.Linq.Expressions;
using Mimic.Core;
using Mimic.Setup.ArgumentMatchers;

namespace Mimic.Expressions;

internal static class ExpressionEvaluator
{
    internal static Expression PartiallyEvaluate(Expression expression, bool argumentMatcherAware = false)
    {
        Func<Expression,bool> canEvaluate = !argumentMatcherAware
            ? e => e.NodeType != ExpressionType.Parameter
            : ArgumentMatcherCanEvaluate;

        var evaluatableExpressions = new EvaluatableExpressionNominator(canEvaluate).Nominate(expression);
        var partiallyEvaluatedExpression = new ExpressionSubtreeEvaluator(evaluatableExpressions).Evaluate(expression);
        Guard.NotNull(partiallyEvaluatedExpression);

        return partiallyEvaluatedExpression;
    }

    private static bool ArgumentMatcherCanEvaluate(Expression expression)
    {
        return expression.NodeType switch
        {
            ExpressionType.Quote or ExpressionType.Parameter => false,
            ExpressionType.Extension => expression is not ArgumentMatcherExpression,
            ExpressionType.Call or ExpressionType.MemberAccess => !ArgumentMatcherInitializer.IsArgumentMatcher(expression, out _),
            _ => true
        };
    }

    private class ExpressionSubtreeEvaluator : ExpressionVisitor
    {
        private readonly HashSet<Expression> _expressions;

        internal ExpressionSubtreeEvaluator(HashSet<Expression> expressions)
        {
            _expressions = expressions;
        }

        internal Expression? Evaluate(Expression expression)
        {
            return Visit(expression);
        }

        public override Expression? Visit(Expression? node)
        {
            if (node is null)
                return null;

            return _expressions.Contains(node)
                ? EvaluateNode(node)
                : base.Visit(node);
        }

        private static Expression EvaluateNode(Expression? node)
        {
            Guard.NotNull(node);

            if (node is { NodeType: ExpressionType.Constant })
                return node;

            var lambda = Expression.Lambda(node);
            var compiledDelegate = lambda.Compile();
            return Expression.Constant(compiledDelegate.DynamicInvoke(null), node.Type);
        }
    }

    private class EvaluatableExpressionNominator : ExpressionVisitor
    {
        private readonly Func<Expression, bool> _canEvaluate;
        private readonly HashSet<Expression> _evaluatableExpressions = new();
        private bool _notPossibleToEvaluate;

        internal EvaluatableExpressionNominator(Func<Expression, bool> canEvaluate)
        {
            _canEvaluate = canEvaluate;
        }

        internal HashSet<Expression> Nominate(Expression expression)
        {
            Guard.Assert(_evaluatableExpressions.Count == 0);

            Visit(expression);
            return _evaluatableExpressions;
        }

        public override Expression? Visit(Expression? node)
        {
            if (node is not { NodeType: ExpressionType.Quote })
                return node;

            bool notPossibleToEvaluate = _notPossibleToEvaluate;
            _notPossibleToEvaluate = false;

            base.Visit(node);

            if (!_notPossibleToEvaluate)
            {
                if (CanEvaluate(node))
                {
                    _evaluatableExpressions.Add(node);
                }
                else
                {
                    _notPossibleToEvaluate = true;
                }
            }

            _notPossibleToEvaluate |= notPossibleToEvaluate;

            return node;
        }

        private bool CanEvaluate(Expression node)
        {
            try
            {
                return _canEvaluate(node);
            }
            catch
            {
                return false;
            }
        }
    }
}
