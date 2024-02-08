using Mimic.Expressions;
using Mimic.Setup;

namespace Mimic;

public partial class Mimic<T>
{
    public void VerifyExpectedReceived() => VerifyReceived(s => s.Expected);

    public void VerifyAllSetupsReceived() => VerifyReceived((SetupBase _) => true);

    public void VerifyNoOtherCallsReceived()
    {
        lock (_invocations)
        {
            var unverifiedInvocations = _invocations.Where(i => !i.Verified).ToList();
            if (unverifiedInvocations.Any())
                throw MimicException.UnverifiedInvocations(this, unverifiedInvocations);
        }
    }

    #region VerifyReceived

    public void VerifyReceived(Expression<Action<T>> expression) =>
        VerifyReceivedInternal(expression, CallCount.AtLeastOnce());

    public void VerifyReceived(Expression<Action<T>> expression, CallCount callCount) =>
        VerifyReceivedInternal(expression, callCount);

    public void VerifyReceived(Expression<Action<T>> expression, Func<CallCount> callCount) =>
        VerifyReceivedInternal(expression, callCount());

    public void VerifyReceived(Expression<Action<T>> expression, string failureMessage) =>
        VerifyReceivedInternal(expression, CallCount.AtLeastOnce(), failureMessage);

    public void VerifyReceived(Expression<Action<T>> expression, CallCount callCount, string failureMessage) =>
        VerifyReceivedInternal(expression, callCount, failureMessage);

    public void VerifyReceived(Expression<Action<T>> expression, Func<CallCount> callCount, string failureMessage) =>
        VerifyReceivedInternal(expression, callCount(), failureMessage);

    #endregion

    #region VerifyReceived<TResult>

    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression) =>
        VerifyReceivedInternal(expression, CallCount.AtLeastOnce());

    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression, CallCount callCount) =>
        VerifyReceivedInternal(expression, callCount);

    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression, Func<CallCount> callCount) =>
        VerifyReceivedInternal(expression, callCount());

    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression, string failureMessage) =>
        VerifyReceivedInternal(expression, CallCount.AtLeastOnce(), failureMessage);

    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression, CallCount callCount, string failureMessage) =>
        VerifyReceivedInternal(expression, callCount, failureMessage);

    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression, Func<CallCount> callCount, string failureMessage) =>
        VerifyReceivedInternal(expression, callCount(), failureMessage);

    #endregion

    #region VerifyGetReceived<TProperty>

    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression) =>
        VerifyGetReceivedInternal(expression, CallCount.AtLeastOnce());

    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression, CallCount callCount) =>
        VerifyGetReceivedInternal(expression, callCount);

    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression, Func<CallCount> callCount) =>
        VerifyGetReceivedInternal(expression, callCount());

    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression, string failureMessage) =>
        VerifyGetReceivedInternal(expression, CallCount.AtLeastOnce(), failureMessage);

    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression, CallCount callCount, string failureMessage) =>
        VerifyGetReceivedInternal(expression, callCount, failureMessage);

    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression, Func<CallCount> callCount, string failureMessage) =>
        VerifyGetReceivedInternal(expression, callCount(), failureMessage);

    #endregion

    #region VerifySetReceived

    public void VerifySetReceived(Action<T> setterExpression) =>
        VerifySetReceivedInternal(setterExpression, CallCount.AtLeastOnce());

    public void VerifySetReceived(Action<T> setterExpression, CallCount callCount) =>
        VerifySetReceivedInternal(setterExpression, callCount);

    public void VerifySetReceived(Action<T> setterExpression, Func<CallCount> callCount) =>
        VerifySetReceivedInternal(setterExpression, callCount());

    public void VerifySetReceived(Action<T> setterExpression, string failureMessage) =>
        VerifySetReceivedInternal(setterExpression, CallCount.AtLeastOnce(), failureMessage);

    public void VerifySetReceived(Action<T> setterExpression, CallCount callCount, string failureMessage) =>
        VerifySetReceivedInternal(setterExpression, callCount, failureMessage);

    public void VerifySetReceived(Action<T> setterExpression, Func<CallCount> callCount, string failureMessage) =>
        VerifySetReceivedInternal(setterExpression, callCount(), failureMessage);

    #endregion

    private void VerifyReceived(Predicate<SetupBase> predicate)
    {
        lock (_invocations)
            foreach (var invocation in _invocations)
                invocation.MarkVerified(predicate);

        foreach (var setup in _setups.FindAll(predicate))
            setup.VerifyMatched();
    }

    private void VerifyReceivedInternal(LambdaExpression expression, CallCount callCount, string? failureMessage = null)
    {
        Guard.NotNull(expression);

        var matchingInvocations = FindMatchingInvocations(expression);
        if (!callCount.Validate(matchingInvocations.Count))
            throw MimicException.NoMatchingInvocations(this, expression, callCount, matchingInvocations.Count, failureMessage);

        foreach (var matchingInvocation in matchingInvocations)
            matchingInvocation.MarkVerified();
    }

    private void VerifyGetReceivedInternal(LambdaExpression expression, CallCount callCount, string? failureMessage = null)
    {
        if (expression.Body is not (IndexExpression or MethodCallExpression { Method.IsSpecialName: true }))
        {
            if (expression.Body is not MemberExpression { Member: PropertyInfo property })
                throw MimicException.ExpressionNotProperty(expression);

            if (!property.CanReadProperty(out _, out _))
                throw MimicException.ExpressionNotPropertyGetter(property);
        }

        VerifyReceivedInternal(expression, callCount, failureMessage);
    }

    private void VerifySetReceivedInternal(Action<T> setterExpression, CallCount callCount, string? failureMessage = null)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression);
        if (expression.Body is not BinaryExpression { NodeType: ExpressionType.Assign, Left: MemberExpression or IndexExpression })
        {
            if (expression.Body.NodeType is not ExpressionType.Call)
                throw MimicException.ExpressionNotPropertySetter(expression);

            var call = (MethodCallExpression)expression.Body;
            if (!call.Method.IsSetter())
                throw MimicException.ExpressionNotPropertySetter(expression);
        }

        VerifyReceivedInternal(expression, callCount, failureMessage);
    }

    private List<Invocation> FindMatchingInvocations(LambdaExpression expression)
    {
        var expectations = ExpressionSplitter.Split(expression);
        if (expectations.Count != 1)
            throw new UnsupportedExpressionException(expression);

        var expectation = expectations.Pop();

        lock (_invocations)
            return _invocations.Where(expectation.MatchesInvocation).ToList();
    }
}
