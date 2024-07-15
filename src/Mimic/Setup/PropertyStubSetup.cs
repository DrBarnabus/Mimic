using Mimic.Expressions;

namespace Mimic.Setup;

internal sealed class PropertyStubSetup : SetupBase
{
    private object? _currentValue;

    public PropertyStubSetup(IMimic mimic, LambdaExpression expression, MethodInfo getter, MethodInfo setter, object? initialValue)
        : base(null, mimic, new PropertyStubExpectation(expression, getter, setter))
    {
        _currentValue = initialValue;
    }

    protected override void ExecuteCore(Invocation invocation)
    {
        if (invocation.Method.ReturnType == typeof(void))
        {
            Guard.Assert(invocation.Method.IsSetter());
            Guard.Assert(invocation.Arguments.Length == 1);

            _currentValue = invocation.Arguments[0];
        }
        else
        {
            Guard.Assert(invocation.Method.IsGetter());

            invocation.SetReturnValue(_currentValue);
        }
    }

    internal override void VerifyMatched(Predicate<SetupBase> predicate, HashSet<IMimic> verified)
    {
        // intentionally empty
    }

    internal override IReadOnlyList<IMimic> GetNested() => (_currentValue as IMimicked)?.Mimic is { } mimic ? [mimic] : [];

    private sealed class PropertyStubExpectation : IExpectation
    {
        private readonly MethodInfo _getter;
        private readonly MethodInfo _setter;

        public LambdaExpression Expression { get; }

        public PropertyStubExpectation(LambdaExpression expression, MethodInfo getter, MethodInfo setter)
        {
            Expression = expression;
            _getter = getter;
            _setter = setter;
        }

        public bool MatchesInvocation(Invocation invocation)
            => invocation.Method.Name == _getter.Name || invocation.Method.Name == _setter.Name;

        public override bool Equals(object? obj) => obj is PropertyStubExpectation other &&  Equals(other);

        public bool Equals(IExpectation? obj) =>
            obj is PropertyStubExpectation other && ExpressionEqualityComparer.Default.Equals(Expression, other.Expression);

        public override int GetHashCode() => typeof(PropertyStubExpectation).GetHashCode();
    }
}
