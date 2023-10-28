using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using Mimic.Core;
using Mimic.Core.Extensions;
using Mimic.Proxy;
using Expr = System.Linq.Expressions.Expression;

namespace Mimic.Setup;

internal sealed class AllPropertiesStubSetup : SetupBase
{
    private readonly ConcurrentDictionary<string, object?> _currentValues = new();

    public AllPropertiesStubSetup(IMimic mimic)
        : base(null, mimic, new AllPropertiesStubExpectation(mimic))
    {
    }

    protected override void ExecuteCore(IInvocation invocation)
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

            object? currentValue = _currentValues.GetOrAdd(propertyName, () => invocation.Method.ReturnType.GetDefaultValue());
            invocation.SetReturnValue(currentValue);
        }
    }

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

        public bool MatchesInvocation(IInvocation invocation)
        {
            var method = invocation.Method;
            int parameterCount = method.GetParameters().Length;

            return (method.IsGetter() && parameterCount == 0) || (method.IsSetter() && parameterCount == 1);
        }
    }
}
