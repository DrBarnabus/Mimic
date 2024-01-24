using Mimic.Setup.ArgumentMatchers;

namespace Mimic.Expressions;

internal static class SetterExpressionConstructor
{
    internal static Expression<Action<T>> ConstructFromAction<T>(Action<T> action)
    {
        using var observer = ArgumentMatcherObserver.ActivateObserver();

        var proxy = CreateProxy<T>(observer, out var interceptor);

        Exception? exception = null;
        try
        {
            action.Invoke(proxy);
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        var actionParameters = action.GetMethodInfo().GetParameters();
        string? actionParameterName = actionParameters[^1].Name;

        var expression = Expression.Parameter(typeof(T), actionParameterName);
        Expression body = expression;

        var invocation = interceptor.Invocation;
        if (invocation == null)
            throw new UnsupportedExpressionException(body, $"{actionParameterName} => {body}...", UnsupportedExpressionException.UnsupportedReason.MemberNotInterceptable);

        body = Expression.Call(body, invocation.Method, GetArgumentExpressions(body, invocation, interceptor.ArgumentMatchers.ToArray()));

        if (exception == null)
            return Expression.Lambda<Action<T>>(body, expression);

        throw new UnsupportedExpressionException(body, $"{actionParameterName} => {body}...", UnsupportedExpressionException.UnsupportedReason.ExpressionThrewAnException);
    }

    private static T CreateProxy<T>(ArgumentMatcherObserver observer, out Interceptor interceptor)
    {
        interceptor = new Interceptor(observer);
        return (T)ProxyGenerator.Instance.GenerateProxy(typeof(T), Type.EmptyTypes, interceptor);
    }

    private static Expression[] GetArgumentExpressions(Expression body, Invocation invocation, IReadOnlyList<ArgumentMatcher> argumentMatchers)
    {
        var parameterTypes = invocation.Method.GetParameters().Select(p => p.ParameterType).ToArray();

        var expressions = new Expression[parameterTypes.Length];
        for (int i = 0; i < parameterTypes.Length; i++)
            expressions[i] = Expression.Constant(invocation.Arguments[i], parameterTypes[i]);

        if (argumentMatchers.Count > 0)
        {
            int argumentMatcherIndex = 0;
            for (int i = 0; argumentMatcherIndex < argumentMatchers.Count && i < expressions.Length; i++)
            {
                var argumentMatcherType = argumentMatchers[argumentMatcherIndex].Type;
                object? defaultValue = GetDefaultValue(argumentMatcherType, parameterTypes[i]);

                if (!Equals(invocation.Arguments[i], defaultValue))
                    continue;

                if (!parameterTypes[i].IsAssignableFrom(defaultValue?.GetType() ?? argumentMatcherType))
                    continue;

                if (argumentMatcherIndex < argumentMatchers.Count - 1 &&
                    !(argumentMatcherIndex < expressions.Length - 1 || CanDistribute(argumentMatcherIndex + 1, i + 1)))
                    break;

                expressions[i] = new ArgumentMatcherExpression(argumentMatchers[argumentMatcherIndex]);
                argumentMatcherIndex++;
            }

            if (argumentMatcherIndex < argumentMatchers.Count)
                throw new UnsupportedExpressionException(body, body.ToString(), UnsupportedExpressionException.UnsupportedReason.UnableToDetermineArgumentMatchers);
        }

        for (int i = 0; i < expressions.Length; i++)
        {
            var argument = expressions[i];
            var parameterType = parameterTypes[i];

            if (argument.Type == parameterType)
                continue;

            if (Nullable.GetUnderlyingType(parameterType) != null && Nullable.GetUnderlyingType(argument.Type) == null)
                expressions[i] = Expression.Convert(argument, parameterType);
            else if (argument.Type.IsValueType && !parameterType.IsValueType)
                expressions[i] = Expression.Convert(argument, parameterType);
            else if (argument.Type != parameterType && !parameterType.IsAssignableFrom(argument.Type))
                expressions[i] = Expression.Convert(argument, parameterType);
        }

        return expressions;

        object? GetDefaultValue(Type matcherType, Type parameterType)
        {
            object? defaultValue = matcherType.GetDefaultValue();

            try
            {
                return Convert.ChangeType(defaultValue, parameterType);
            }
            catch
            {
                // intentionally empty
            }

            return defaultValue;
        }

        bool CanDistribute(int argumentMatcherIndex, int startArgumentIndex)
        {
            var argumentMatcher = argumentMatchers[argumentMatcherIndex];

            for (int i = startArgumentIndex; i < expressions.Length; ++i)
            {
                if (parameterTypes[i].IsAssignableFrom(argumentMatcher.Type)
                    && CanDistribute(argumentMatcherIndex + 1, i + 1))
                    return true;
            }

            return false;
        }
    }

    private sealed class Interceptor : IInterceptor
    {
        private readonly ArgumentMatcherObserver _observer;
        private readonly int _startCounter;
        private int? _invocationCounter;

        public Invocation? Invocation { get; private set; }

        public IEnumerable<ArgumentMatcher> ArgumentMatchers => _observer.GetArgumentMatchersBetween(_startCounter, _invocationCounter!.Value);

        public Interceptor(ArgumentMatcherObserver observer)
        {
            _observer = observer;
            _startCounter = observer.GetCounter();
        }

        public void Intercept(Invocation invocation)
        {
            Invocation = invocation;
            _invocationCounter = _observer.GetCounter();

            var returnType = invocation.Method.ReturnType;
            if (returnType != typeof(void))
                throw new NotSupportedException("The return type of the last member is not void.");
        }
    }
}
