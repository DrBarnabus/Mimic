using Mimic.Expressions;
using Mimic.Setup;

namespace Mimic;

public partial class Mimic<T>
{
    /// <summary>
    /// Verifies that the mimic has received all expected setups.
    /// </summary>
    public void VerifyExpectedReceived() => VerifyReceived(s => s.Expected, []);

    /// <summary>
    /// Verifies that the mimic has received all configured setups.
    /// </summary>
    public void VerifyAllSetupsReceived() => VerifyReceived((SetupBase _) => true, []);

    /// <summary>
    /// Verifies that the mimic has received no other calls beyond those already verified.
    /// </summary>
    /// <exception cref="MimicException">Thrown when unverified invocations are found.</exception>
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

    /// <summary>
    /// Verifies that the specified method call expression was received at least once.
    /// </summary>
    /// <param name="expression">The expression representing the method call to verify.</param>
    /// <exception cref="Guard.AssertionException">Thrown when the expression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expected invocation was not received.</exception>
    public void VerifyReceived(Expression<Action<T>> expression) =>
        VerifyReceivedInternal(expression, CallCount.AtLeastOnce());

    /// <summary>
    /// Verifies that the specified method call expression was received the expected number of times.
    /// </summary>
    /// <param name="expression">The expression representing the method call to verify.</param>
    /// <param name="callCount">The expected number of calls.</param>
    /// <exception cref="Guard.AssertionException">Thrown when the expression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expected invocation count was not met.</exception>
    public void VerifyReceived(Expression<Action<T>> expression, CallCount callCount) =>
        VerifyReceivedInternal(expression, callCount);

    /// <summary>
    /// Verifies that the specified method call expression was received at least once, with a custom failure message.
    /// </summary>
    /// <param name="expression">The expression representing the method call to verify.</param>
    /// <param name="failureMessage">The custom message to display when verification fails.</param>
    /// <exception cref="Guard.AssertionException">Thrown when the expression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expected invocation was not received.</exception>
    public void VerifyReceived(Expression<Action<T>> expression, string failureMessage) =>
        VerifyReceivedInternal(expression, CallCount.AtLeastOnce(), failureMessage);

    /// <summary>
    /// Verifies that the specified method call expression was received the expected number of times, with a custom failure message.
    /// </summary>
    /// <param name="expression">The expression representing the method call to verify.</param>
    /// <param name="callCount">The expected number of calls.</param>
    /// <param name="failureMessage">The custom message to display when verification fails.</param>
    /// <exception cref="Guard.AssertionException">Thrown when the expression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expected invocation count was not met.</exception>
    public void VerifyReceived(Expression<Action<T>> expression, CallCount callCount, string failureMessage) =>
        VerifyReceivedInternal(expression, callCount, failureMessage);

    #endregion

    #region VerifyReceived<TResult>

    /// <summary>
    /// Verifies that the specified method call expression returning a value was received at least once.
    /// </summary>
    /// <typeparam name="TResult">The return type of the method being verified.</typeparam>
    /// <param name="expression">The expression representing the method call to verify.</param>
    /// <exception cref="Guard.AssertionException">Thrown when the expression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expected invocation was not received.</exception>
    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression) =>
        VerifyReceivedInternal(expression, CallCount.AtLeastOnce());

    /// <summary>
    /// Verifies that the specified method call expression returning a value was received the expected number of times.
    /// </summary>
    /// <typeparam name="TResult">The return type of the method being verified.</typeparam>
    /// <param name="expression">The expression representing the method call to verify.</param>
    /// <param name="callCount">The expected number of calls.</param>
    /// <exception cref="Guard.AssertionException">Thrown when the expression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expected invocation count was not met.</exception>
    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression, CallCount callCount) =>
        VerifyReceivedInternal(expression, callCount);

    /// <summary>
    /// Verifies that the specified method call expression returning a value was received at least once, with a custom failure message.
    /// </summary>
    /// <typeparam name="TResult">The return type of the method being verified.</typeparam>
    /// <param name="expression">The expression representing the method call to verify.</param>
    /// <param name="failureMessage">The custom message to display when verification fails.</param>
    /// <exception cref="Guard.AssertionException">Thrown when the expression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expected invocation was not received.</exception>
    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression, string failureMessage) =>
        VerifyReceivedInternal(expression, CallCount.AtLeastOnce(), failureMessage);

    /// <summary>
    /// Verifies that the specified method call expression returning a value was received the expected number of times, with a custom failure message.
    /// </summary>
    /// <typeparam name="TResult">The return type of the method being verified.</typeparam>
    /// <param name="expression">The expression representing the method call to verify.</param>
    /// <param name="callCount">The expected number of calls.</param>
    /// <param name="failureMessage">The custom message to display when verification fails.</param>
    /// <exception cref="Guard.AssertionException">Thrown when the expression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expected invocation count was not met.</exception>
    public void VerifyReceived<TResult>(Expression<Func<T, TResult>> expression, CallCount callCount, string failureMessage) =>
        VerifyReceivedInternal(expression, callCount, failureMessage);

    #endregion

    #region VerifyGetReceived<TProperty>

    /// <summary>
    /// Verifies that the specified property getter was accessed at least once.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being verified.</typeparam>
    /// <param name="expression">The expression representing the property getter to verify.</param>
    /// <exception cref="MimicException">Thrown when the expression is not a valid property getter or when the expected access was not received.</exception>
    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression) =>
        VerifyGetReceivedInternal(expression, CallCount.AtLeastOnce());

    /// <summary>
    /// Verifies that the specified property getter was accessed the expected number of times.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being verified.</typeparam>
    /// <param name="expression">The expression representing the property getter to verify.</param>
    /// <param name="callCount">The expected number of accesses.</param>
    /// <exception cref="MimicException">Thrown when the expression is not a valid property getter or when the expected access count was not met.</exception>
    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression, CallCount callCount) =>
        VerifyGetReceivedInternal(expression, callCount);

    /// <summary>
    /// Verifies that the specified property getter was accessed at least once, with a custom failure message.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being verified.</typeparam>
    /// <param name="expression">The expression representing the property getter to verify.</param>
    /// <param name="failureMessage">The custom message to display when verification fails.</param>
    /// <exception cref="MimicException">Thrown when the expression is not a valid property getter or when the expected access was not received.</exception>
    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression, string failureMessage) =>
        VerifyGetReceivedInternal(expression, CallCount.AtLeastOnce(), failureMessage);

    /// <summary>
    /// Verifies that the specified property getter was accessed the expected number of times, with a custom failure message.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being verified.</typeparam>
    /// <param name="expression">The expression representing the property getter to verify.</param>
    /// <param name="callCount">The expected number of accesses.</param>
    /// <param name="failureMessage">The custom message to display when verification fails.</param>
    /// <exception cref="MimicException">Thrown when the expression is not a valid property getter or when the expected access count was not met.</exception>
    public void VerifyGetReceived<TProperty>(Expression<Func<T, TProperty>> expression, CallCount callCount, string failureMessage) =>
        VerifyGetReceivedInternal(expression, callCount, failureMessage);

    #endregion

    #region VerifySetReceived

    /// <summary>
    /// Verifies that the specified property setter was called at least once.
    /// </summary>
    /// <param name="setterExpression">The expression representing the property setter to verify.</param>
    /// <exception cref="Guard.AssertionException">Thrown when setterExpression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expression is not a valid property setter or when the expected call was not received.</exception>
    public void VerifySetReceived(Action<T> setterExpression) =>
        VerifySetReceivedInternal(setterExpression, CallCount.AtLeastOnce());

    /// <summary>
    /// Verifies that the specified property setter was called the expected number of times.
    /// </summary>
    /// <param name="setterExpression">The expression representing the property setter to verify.</param>
    /// <param name="callCount">The expected number of calls.</param>
    /// <exception cref="Guard.AssertionException">Thrown when setterExpression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expression is not a valid property setter or when the expected call count was not met.</exception>
    public void VerifySetReceived(Action<T> setterExpression, CallCount callCount) =>
        VerifySetReceivedInternal(setterExpression, callCount);

    /// <summary>
    /// Verifies that the specified property setter was called at least once, with a custom failure message.
    /// </summary>
    /// <param name="setterExpression">The expression representing the property setter to verify.</param>
    /// <param name="failureMessage">The custom message to display when verification fails.</param>
    /// <exception cref="Guard.AssertionException">Thrown when setterExpression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expression is not a valid property setter or when the expected call was not received.</exception>
    public void VerifySetReceived(Action<T> setterExpression, string failureMessage) =>
        VerifySetReceivedInternal(setterExpression, CallCount.AtLeastOnce(), failureMessage);

    /// <summary>
    /// Verifies that the specified property setter was called the expected number of times, with a custom failure message.
    /// </summary>
    /// <param name="setterExpression">The expression representing the property setter to verify.</param>
    /// <param name="callCount">The expected number of calls.</param>
    /// <param name="failureMessage">The custom message to display when verification fails.</param>
    /// <exception cref="Guard.AssertionException">Thrown when setterExpression is null.</exception>
    /// <exception cref="MimicException">Thrown when the expression is not a valid property setter or when the expected call count was not met.</exception>
    public void VerifySetReceived(Action<T> setterExpression, CallCount callCount, string failureMessage) =>
        VerifySetReceivedInternal(setterExpression, callCount, failureMessage);

    #endregion

    private void VerifyReceived(Predicate<SetupBase> predicate, HashSet<IMimic> verified)
    {
        if (!verified.Add(this))
            return;

        lock (_invocations)
            foreach (var invocation in _invocations)
                invocation.MarkVerified(predicate);

        foreach (var setup in _setups.FindAll(predicate))
            setup.VerifyMatched(predicate, verified);
    }

    private void VerifyReceivedInternal(LambdaExpression expression, CallCount callCount, string? failureMessage = null)
    {
        Guard.NotNull(expression);

        FindMatchingInvocations(this, expression, out int matchingInvocationCount, out var matchingInvocations);

        if (!callCount.Validate(matchingInvocationCount))
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

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression, ConstructorArguments);
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

    private static void FindMatchingInvocations(
        IMimic mimic,
        LambdaExpression expression,
        out int matchingInvocationCount,
        out List<Invocation> matchingInvocations)
    {
        Guard.NotNull(mimic);
        Guard.NotNull(expression);

        var expectations = new ImmutableStack<MethodExpectation>(ExpressionSplitter.Split(expression));

        matchingInvocations = [];
        matchingInvocationCount = FindMatchingInvocations(mimic, expectations, [], matchingInvocations);
    }

    private static int FindMatchingInvocations(
        IMimic mimic,
        ImmutableStack<MethodExpectation> expectations,
        HashSet<IMimic> visitedNestedMimics,
        List<Invocation> matchingInvocations)
    {
        Guard.NotNull(mimic);
        Guard.Assert(!expectations.IsEmpty);
        Guard.NotNull(visitedNestedMimics);

        if (!visitedNestedMimics.Add(mimic))
            return 0;

        var expectation = expectations.Pop(out var remainingExpectations);

        int matchedCount = 0;
        foreach (var matchingInvocation in mimic.Invocations.Where(expectation.MatchesInvocation))
        {
            matchingInvocations.Add(matchingInvocation);

            if (remainingExpectations.IsEmpty)
            {
                ++matchedCount;
                continue;
            }

            Guard.Assert(matchingInvocation.Method.ReturnType != typeof(void));
            if (matchingInvocation.ReturnValue is IMimicked mimicked)
            {
                matchedCount += FindMatchingInvocations(mimicked.Mimic, remainingExpectations, visitedNestedMimics, matchingInvocations);
            }
        }

        return matchedCount;
    }
}
