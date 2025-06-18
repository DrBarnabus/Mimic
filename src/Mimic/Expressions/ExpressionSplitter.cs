using Mimic.Setup;

namespace Mimic.Expressions;

internal static class ExpressionSplitter
{
    private const string EmptyParameterName = "...";

    internal static Stack<MethodExpectation> Split(LambdaExpression expression)
    {
        Guard.NotNull(expression);

        var parts = new Stack<MethodExpectation>();

        var remainder = expression.Body;
        while (CanSplitExpression(remainder))
        {
            var part = SplitExpression(remainder, out remainder);
            parts.Push(part);
        }

        if (parts.Count > 0 && remainder is ParameterExpression)
            return parts;

        throw MimicException.UnsupportedExpressionType(expression);
    }

    private static bool CanSplitExpression(Expression expression)
    {
        return expression.NodeType switch
        {
            ExpressionType.Assign or ExpressionType.AddAssign or ExpressionType.SubtractAssign => CanSplitExpression(((BinaryExpression)expression).Left),
            ExpressionType.Call or ExpressionType.Index => true,
            ExpressionType.Invoke => typeof(Delegate).IsAssignableFrom(((InvocationExpression)expression).Expression.Type),
            ExpressionType.MemberAccess => ((MemberExpression)expression).Member is PropertyInfo,
            _ => false
        };
    }

    private static MethodExpectation SplitExpression(
        Expression expression, out Expression remainder, bool isAssignment = false)
    {
        return expression.NodeType switch
        {
            ExpressionType.Assign => SplitBinaryExpression(expression, out remainder),
            ExpressionType.AddAssign => SplitBinaryExpression(expression, out remainder),
            ExpressionType.SubtractAssign => SplitBinaryExpression(expression, out remainder),
            ExpressionType.Call => SplitMethodCallExpression(expression, out remainder),
            ExpressionType.Index => SplitIndexExpression(expression, out remainder, isAssignment),
            ExpressionType.MemberAccess => SplitMemberExpression(expression, out remainder, isAssignment),
            _ => throw MimicException.UnsupportedExpressionType(expression)
        };
    }

    private static MethodExpectation SplitMemberExpression(
        Expression expression, out Expression remainder, bool isAssignment)
    {
        var memberExpression = (MemberExpression)expression;
        Guard.Assert(memberExpression.Member is PropertyInfo, $"Member should be {nameof(PropertyInfo)}");

        remainder = memberExpression.Expression!;

        var parameter = ParameterFromExpression(remainder);
        var property = (PropertyInfo)memberExpression.Member;

        MethodInfo? method = null;
        if (!isAssignment && property.CanReadProperty(out var getter, out var getterProperty))
        {
            method = getter;
            property = getterProperty;
        }
        else if (property.CanWriteProperty(out var setter, out var setterProperty))
        {
            method = setter;
            property = setterProperty;
        }

        return new MethodExpectation(
            Expression.Lambda(Expression.MakeMemberAccess(parameter, property!), parameter),
            method!,
            skipMatchers: isAssignment);
    }

    private static MethodExpectation SplitIndexExpression(
        Expression expression, out Expression remainder, bool isAssignment)
    {
        var indexExpression = (IndexExpression)expression;
        remainder = indexExpression.Object!;

        var parameter = ParameterFromExpression(remainder);
        var indexer = indexExpression.Indexer;
        var arguments = indexExpression.Arguments;

        MethodInfo? method = null;
        if (!isAssignment && indexer!.CanReadProperty(out var getter, out var getterIndexer))
        {
            method = getter;
            indexer = getterIndexer;
        }
        else if (indexer!.CanWriteProperty(out var setter, out var setterIndexer))
        {
            method = setter;
            indexer = setterIndexer;
        }

        return new MethodExpectation(
            Expression.Lambda(Expression.MakeIndex(parameter, indexer, arguments), parameter),
            method!,
            arguments,
            isAssignment);
    }

    private static MethodExpectation SplitMethodCallExpression(Expression expression, out Expression remainder)
    {
        var methodCallExpression = (MethodCallExpression)expression;
        var method = methodCallExpression.Method;
        var arguments = methodCallExpression.Arguments;

        if (!method.IsStatic)
        {
            remainder = methodCallExpression.Object!;

            var parameter = ParameterFromExpression(remainder);

            return new MethodExpectation(
                Expression.Lambda(Expression.Call(parameter, method, arguments), parameter),
                method,
                arguments);
        }
        else
        {
            Guard.Assert(IsExtension(method), "Method must be an extension method");
            Guard.Assert(arguments.Count > 0, "Method must have at least 1 argument");

            remainder = methodCallExpression.Arguments[0];

            var parameter = ParameterFromExpression(remainder);
            var mutableArguments = arguments.ToArray();
            mutableArguments[0] = parameter;

            return new MethodExpectation(
                Expression.Lambda(Expression.Call(method, mutableArguments), parameter),
                method,
                mutableArguments);
        }
    }

    private static MethodExpectation SplitBinaryExpression(Expression expression, out Expression remainder)
    {
        var binaryExpression = (BinaryExpression)expression;
        var leftHandSidePart = SplitExpression(binaryExpression.Left, out remainder, isAssignment: true);

        var parameter = ParameterFromExpression(remainder);

        var arguments = new Expression[leftHandSidePart.MethodInfo.GetParameters().Length];
        for (int i = 0; i < arguments.Length - 1; i++)
        {
            arguments[i] = leftHandSidePart.Arguments[i];
        }

        arguments[^1] = binaryExpression.Right;

        return new MethodExpectation(
            Expression.Lambda(
                Expression.MakeBinary(expression.NodeType, leftHandSidePart.Expression.Body, binaryExpression.Right),
                parameter),
            leftHandSidePart.MethodInfo,
            arguments);
    }

    private static ParameterExpression ParameterFromExpression(Expression remainder)
    {
        return Expression.Parameter(
            remainder.Type,
            remainder is ParameterExpression parameterExpression
                ? parameterExpression.Name
                : EmptyParameterName);
    }

    private static bool IsExtension(MethodBase method)
    {
        return method.IsStatic && method.IsDefined(typeof(ExtensionAttribute));
    }
}
