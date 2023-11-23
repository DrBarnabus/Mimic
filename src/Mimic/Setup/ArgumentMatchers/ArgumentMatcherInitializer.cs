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
        {
            if ((parameter.Attributes & (ParameterAttributes.In | ParameterAttributes.Out)) == ParameterAttributes.Out)
                return new InitializedMatcher(AnyArgumentMatcher.Instance, expression);

            if (expression is MemberExpression { Member.Name: nameof(Arg.Ref<object>.Any) } memberExpression)
            {
                var declaringType = memberExpression.Member.DeclaringType!;
                if (declaringType.IsGenericType && declaringType.GetGenericTypeDefinition() == typeof(Arg.Ref<>))
                    return new InitializedMatcher(AnyArgumentMatcher.Instance, expression);
            }

            if (ExpressionEvaluator.PartiallyEvaluate(expression) is ConstantExpression constantExpression)
                return new InitializedMatcher(new RefArgumentMatcher(constantExpression.Value), constantExpression);
        }

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

                if (!matchedValueTypes.IsAssignableFrom(parameter.ParameterType))
                    throw MimicException.UnmatchableArgumentMatcher(convertExpression.Operand, parameter.ParameterType);
            }
        }

        return Initialize(expression);
    }

    internal static InitializedMatcher Initialize(Expression expression)
    {
        var originalExpression = expression;
        while (expression.NodeType is ExpressionType.Convert)
            expression = ((UnaryExpression)expression).Operand;

        if (expression is ArgumentMatcherExpression argumentMatcherExpression)
            return new InitializedMatcher(argumentMatcherExpression.ArgumentMatcher, argumentMatcherExpression);

        if (expression is MethodCallExpression methodCallExpression)
        {
            if (IsArgumentMatcher(expression, out var argumentMatcher))
                return new InitializedMatcher(argumentMatcher, expression);

            var method = methodCallExpression.Method;
            if (!method.IsGetter())
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
            _ => throw new UnsupportedExpressionException(originalExpression)
        };
    }

    internal static bool IsArgumentMatcher(Expression expression, [NotNullWhen(true)] out ArgumentMatcher? argumentMatcher)
    {
        using var observer = ArgumentMatcherObserver.ActivateObserver();

        Expression.Lambda<Action>(expression).Compile().Invoke();
        return observer.TryGetLastArgumentMatcher(out argumentMatcher);
    }

    internal sealed record InitializedMatchers(IArgumentMatcher[] ArgumentMatchers, Expression[] ArgumentExpressions);

    internal sealed record InitializedMatcher(IArgumentMatcher ArgumentMatchers, Expression ArgumentExpressions);
}
