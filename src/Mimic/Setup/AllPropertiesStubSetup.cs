using System.Collections.Concurrent;
using Mimic.Expressions;
using Expr = System.Linq.Expressions.Expression;

namespace Mimic.Setup;

internal sealed class AllPropertiesStubSetup : SetupBase
{
    private readonly ConcurrentDictionary<string, object?> _currentValues = new();

    public AllPropertiesStubSetup(IMimic mimic)
        : base(null, mimic, new AllPropertiesStubExpectation(mimic))
    {
    }

    protected override void ExecuteCore(Invocation invocation)
    {
        string propertyName = invocation.Method.Name[4..];

        if (invocation.Method.ReturnType == typeof(void))
        {
            Guard.Assert(invocation.Method.IsSetter());
            Guard.Assert(invocation.Arguments.Length == 1);

            _currentValues[propertyName] = invocation.Arguments[0];
        }
        else
        {
            Guard.Assert(invocation.Method.IsGetter());

            object? currentValue = _currentValues.GetOrAdd(propertyName, invocation.Method.ReturnType.GetDefaultValue());
            invocation.SetReturnValue(currentValue);
        }
    }

    internal override void VerifyMatched(Predicate<SetupBase> predicate, HashSet<IMimic> verified)
    {
        // intentionally empty
    }

    internal override IReadOnlyList<IMimic> GetNested() => _currentValues.Values.OfType<IMimicked>().Select(m => m.Mimic).ToList();

    private sealed class AllPropertiesStubExpectation : IExpectation
    {
        public LambdaExpression Expression { get; }

        public AllPropertiesStubExpectation(IMimic mimic)
        {
            var mimicType = mimic.GetType();
            var mimickedType = mimicType.GetGenericArguments()[0];

            var setupAllPropertiesMethod = mimicType.GetMethod(nameof(Mimic<object>.SetupAllProperties), BindingFlags.Public | BindingFlags.Instance)!;
            var fromObjectMethod = mimicType.GetMethod(nameof(Mimic<object>.FromObject), BindingFlags.Public | BindingFlags.Static)!;

            var parameter = Expr.Parameter(mimickedType, "m");
            Expression = Expr.Lambda(Expr.Call(Expr.Call(fromObjectMethod, parameter), setupAllPropertiesMethod), parameter);
        }

        public bool MatchesInvocation(Invocation invocation)
        {
            var method = invocation.Method;
            int parameterCount = method.GetParameters().Length;

            return (method.IsGetter() && parameterCount == 0) || (method.IsSetter() && parameterCount == 1);
        }

        public override bool Equals(object? obj) => obj is AllPropertiesStubExpectation other &&  Equals(other);

        public bool Equals(IExpectation? obj) =>
            obj is AllPropertiesStubExpectation other && ExpressionEqualityComparer.Default.Equals(Expression, other.Expression);

        public override int GetHashCode() => typeof(AllPropertiesStubExpectation).GetHashCode();
    }
}
