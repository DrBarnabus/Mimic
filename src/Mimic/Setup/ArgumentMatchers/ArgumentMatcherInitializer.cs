using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Mimic.Core;
using Mimic.Expressions;

namespace Mimic.Setup.ArgumentMatchers;

internal static class ArgumentMatcherInitializer
{
    internal static InitializedMatchers Initialize(Expression[] arguments, ParameterInfo[] parameters)
    {
        Guard.NotNull(arguments);
        Guard.NotNull(parameters);
        Guard.Assert(arguments.Length == parameters.Length, $"Number of supplied arguments {arguments.Length} does not match number of parameters {parameters.Length}");

        var argumentMatchers = new IArgumentMatcher[parameters.Length];
        var argumentExpressions = new Expression[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            (argumentMatchers[i], argumentExpressions[i]) = Initialize(arguments[i], parameters[i]);
        }

        return new InitializedMatchers(argumentMatchers, argumentExpressions);
    }

    internal static InitializedMatcher Initialize(Expression expression, ParameterInfo parameter)
    {
        if (parameter.ParameterType.IsByRef)
            throw new NotSupportedException("IsByRef type method parameters are not supported yet");

        if (parameter.IsDefined(typeof(ParamArrayAttribute), true) && expression.NodeType == ExpressionType.NewArrayInit)
        {
            var newArrayExpression = (NewArrayExpression)expression;
            Guard.Assert(newArrayExpression.Type.IsArray);

            var elementType = newArrayExpression.Type.GetElementType();
            Guard.NotNull(elementType);

            var argumentMatchers = new IArgumentMatcher[newArrayExpression.Expressions.Count];
            var arrayInitializers = new Expression[newArrayExpression.Expressions.Count];
            for (int i = 0; i < newArrayExpression.Expressions.Count; i++)
            {
                (argumentMatchers[i], arrayInitializers[i]) = Initialize(newArrayExpression.Expressions[i]);
                // TODO: Do we need to convert the value, for example initializer with int but expecting long?
            }

            return new InitializedMatcher(new ParamArrayArgumentMatcher(argumentMatchers), Expression.NewArrayInit(elementType, arrayInitializers));
        }

        if (expression.NodeType == ExpressionType.Convert)
        {
            var convertExpression = (UnaryExpression)expression;
            if (convertExpression.Method?.Name == "op_Implicit" && IsArgumentMatcher(convertExpression.Operand, out var argumentMatcher))
            {
                var matchedValueTypes = argumentMatcher.GetType().IsGenericType
                    ? argumentMatcher.GetType().GenericTypeArguments[0]
                    : convertExpression.Operand.Type;

                // TODO: Revisit this and find a nicer exception/warning to throw
                Guard.Assert(!matchedValueTypes.IsAssignableFrom(parameter.ParameterType), $"ArgumentMatcher '{convertExpression.Operand.ToString()}' is unmatchable. An implict conversion of argument from type '{TypeNameFormatter.GetFormattedName(convertExpression.Operand.Type)}' to type '{TypeNameFormatter.GetFormattedName(parameter.ParameterType)}' which is an incompatible assignment");
            }
        }

        return Initialize(expression);
    }

    internal static InitializedMatcher Initialize(Expression expression)
    {
        var originalExpression = expression;
        while (expression.NodeType is ExpressionType.Convert)
            expression = ((UnaryExpression)expression).Operand;

        if (expression is MethodCallExpression methodCallExpression)
        {
            if (IsArgumentMatcher(expression, out var argumentMatcher))
                return new InitializedMatcher(argumentMatcher, expression);

            var method = methodCallExpression.Method;
            if (!(method.IsSpecialName && method.Name.StartsWith("get_", StringComparison.Ordinal)))
                return new InitializedMatcher(new LazyEvaluatedArgumentMatcher(originalExpression), originalExpression);
        }
        else if (expression is MemberExpression or IndexExpression && IsArgumentMatcher(expression, out var argumentMatcher))
        {
            return new InitializedMatcher(argumentMatcher, expression);
        }

        var evaluatedExpression = ExpressionEvaluator.PartiallyEvaluate(originalExpression);
        return evaluatedExpression.NodeType switch
        {
            ExpressionType.Constant => new InitializedMatcher(new ConstantArgumentMatcher(((ConstantExpression)evaluatedExpression).Value), evaluatedExpression),
            ExpressionType.Quote => new InitializedMatcher(new ExpressionArgumentMatcher(((UnaryExpression)expression).Operand), evaluatedExpression),
            _ => throw new NotSupportedException($"Unsupported parameter expression: {originalExpression}")
        };
    }

    private static bool IsArgumentMatcher(Expression expression, [NotNullWhen(true)] out ArgumentMatcher? argumentMatcher)
    {
        using var observer = ArgumentMatcherObserver.ActivateObserver();

        Expression.Lambda<Action>(expression).Compile().Invoke();
        return observer.TryGetLastArgumentMatcher(out argumentMatcher);
    }

    internal sealed record InitializedMatchers(IArgumentMatcher[] ArgumentMatchers, Expression[] ArgumentExpressions);

    internal sealed record InitializedMatcher(IArgumentMatcher ArgumentMatchers, Expression ArgumentExpressions);
}
