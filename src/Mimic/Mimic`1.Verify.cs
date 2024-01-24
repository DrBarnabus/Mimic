using Mimic.Expressions;
using Mimic.Setup;

namespace Mimic;

public partial class Mimic<T>
{
    public void Verify() => Verify(s => s.Verifiable);

    public void VerifyAll() => Verify((SetupBase _) => true);

    public void VerifyNoOtherCalls() => VerifyNoOtherCalls(this);

    #region Verify

    public void Verify(Expression<Action<T>> expression) =>
        Verify(this, expression, CallCount.AtLeastOnce());

    public void Verify(Expression<Action<T>> expression, CallCount callCount) =>
        Verify(this, expression, callCount);

    public void Verify(Expression<Action<T>> expression, Func<CallCount> callCount) =>
        Verify(this, expression, callCount());

    public void Verify(Expression<Action<T>> expression, string failureMessage) =>
        Verify(this, expression, CallCount.AtLeastOnce(), failureMessage);

    public void Verify(Expression<Action<T>> expression, CallCount callCount, string failureMessage) =>
        Verify(this, expression, callCount, failureMessage);

    public void Verify(Expression<Action<T>> expression, Func<CallCount> callCount, string failureMessage) =>
        Verify(this, expression, callCount(), failureMessage);

    #endregion

    #region Verify<TResult>

    public void Verify<TResult>(Expression<Func<T, TResult>> expression) =>
        Verify(this, expression, CallCount.AtLeastOnce());

    public void Verify<TResult>(Expression<Func<T, TResult>> expression, CallCount callCount) =>
        Verify(this, expression, callCount);

    public void Verify<TResult>(Expression<Func<T, TResult>> expression, Func<CallCount> callCount) =>
        Verify(this, expression, callCount());

    public void Verify<TResult>(Expression<Func<T, TResult>> expression, string failureMessage) =>
        Verify(this, expression, CallCount.AtLeastOnce(), failureMessage);

    public void Verify<TResult>(Expression<Func<T, TResult>> expression, CallCount callCount, string failureMessage) =>
        Verify(this, expression, callCount, failureMessage);

    public void Verify<TResult>(Expression<Func<T, TResult>> expression, Func<CallCount> callCount, string failureMessage) =>
        Verify(this, expression, callCount(), failureMessage);

    #endregion

    #region VerifyGet<TProperty>

    public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression) =>
        VerifyGet(this, expression, CallCount.AtLeastOnce());

    public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, CallCount callCount) =>
        VerifyGet(this, expression, callCount);

    public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Func<CallCount> callCount) =>
        VerifyGet(this, expression, callCount());

    public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, string failureMessage) =>
        VerifyGet(this, expression, CallCount.AtLeastOnce(), failureMessage);

    public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, CallCount callCount, string failureMessage) =>
        VerifyGet(this, expression, callCount, failureMessage);

    public void VerifyGet<TProperty>(Expression<Func<T, TProperty>> expression, Func<CallCount> callCount, string failureMessage) =>
        VerifyGet(this, expression, callCount(), failureMessage);

    #endregion

    #region VerifySet

    public void VerifySet(Action<T> setterExpression) =>
        VerifySet(this, setterExpression, CallCount.AtLeastOnce());

    public void VerifySet(Action<T> setterExpression, CallCount callCount) =>
        VerifySet(this, setterExpression, callCount);

    public void VerifySet(Action<T> setterExpression, Func<CallCount> callCount) =>
        VerifySet(this, setterExpression, callCount());

    public void VerifySet(Action<T> setterExpression, string failureMessage) =>
        VerifySet(this, setterExpression, CallCount.AtLeastOnce(), failureMessage);

    public void VerifySet(Action<T> setterExpression, CallCount callCount, string failureMessage) =>
        VerifySet(this, setterExpression, callCount, failureMessage);

    public void VerifySet(Action<T> setterExpression, Func<CallCount> callCount, string failureMessage) =>
        VerifySet(this, setterExpression, callCount(), failureMessage);

    #endregion

    private void Verify(Predicate<SetupBase> predicate)
    {
        lock (_invocations)
            foreach (var invocation in _invocations)
                invocation.MarkVerified(predicate);

        foreach (var setup in _setups.FindAll(predicate))
            setup.Verify();
    }

    private static void VerifyNoOtherCalls(Mimic<T> mimic)
    {
        lock (mimic._invocations)
        {
            var unverifiedInvocations = mimic._invocations.Where(i => !i.Verified).ToList();

            if (unverifiedInvocations.Any())
                throw MimicException.UnverifiedInvocations(mimic, unverifiedInvocations);
        }
    }

    private static void Verify(Mimic<T> mimic, LambdaExpression expression, CallCount callCount, string? failureMessage = null)
    {
        var matchingInvocations = FindMatchingInvocations(mimic, expression);
        if (!callCount.Validate(matchingInvocations.Count))
            throw MimicException.NoMatchingInvocations(mimic, expression, callCount, matchingInvocations.Count, failureMessage);

        foreach (var matchingInvocation in matchingInvocations)
            matchingInvocation.MarkVerified();
    }

    private static void VerifyGet(Mimic<T> mimic, LambdaExpression expression, CallCount callCount, string? failureMessage = null)
    {
        if (expression.Body is not (IndexExpression or MethodCallExpression { Method.IsSpecialName: true }))
        {
            if (expression.Body is not MemberExpression { Member: PropertyInfo property })
                throw MimicException.ExpressionNotProperty(expression);

            if (!property.CanReadProperty(out _, out _))
                throw MimicException.ExpressionNotPropertyGetter(property);
        }

        Verify(mimic, expression, callCount, failureMessage);
    }

    private static void VerifySet(Mimic<T> mimic, Action<T> setterExpression, CallCount callCount, string? failureMessage = null)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression);
        Guard.NotNull(expression);

        if (expression.Body is not BinaryExpression { NodeType: ExpressionType.Assign, Left: MemberExpression or IndexExpression })
        {
            if (expression.Body.NodeType is not ExpressionType.Call)
                throw MimicException.ExpressionNotPropertySetter(expression);

            var call = (MethodCallExpression)expression.Body;
            if (!call.Method.IsSetter())
                throw MimicException.ExpressionNotPropertySetter(expression);
        }

        Verify(mimic, expression, callCount, failureMessage);
    }

    private static List<Invocation> FindMatchingInvocations(Mimic<T> mimic, LambdaExpression expression)
    {
        Guard.NotNull(mimic);
        Guard.NotNull(expression);

        var expectations = ExpressionSplitter.Split(expression);
        if (expectations.Count != 1)
            throw new UnsupportedExpressionException(expression);

        var expectation = expectations.Pop();

        lock (mimic._invocations)
            return mimic._invocations.Where(expectation.MatchesInvocation).ToList();
    }
}
