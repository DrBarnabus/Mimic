using Mimic.Expressions;
using Mimic.Setup;
using Mimic.Setup.Fluent;
using SetupBase = Mimic.Setup.SetupBase;

namespace Mimic;

public partial class Mimic<T>
{
    public ISetup<T> Setup(Expression<Action<T>> expression)
    {
        var setup = Setup(this, expression);
        return new Setup<T>(setup);
    }

    public ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression)
    {
        var setup = Setup(this, expression);
        return new Setup<T, TResult>(setup);
    }

    public IGetterSetup<T, TProperty> SetupGet<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        Guard.NotNull(expression);

        if (expression.Body is not (IndexExpression or MethodCallExpression { Method.IsSpecialName: true }))
        {
            if (expression.Body is not MemberExpression { Member: PropertyInfo property })
                throw MimicException.ExpressionNotProperty(expression);

            if (!property.CanReadProperty(out _, out _))
                throw MimicException.ExpressionNotPropertyGetter(property);
        }

        var setup = Setup(this, expression);
        return new Setup<T, TProperty>(setup);
    }

    public ISetup<T> SetupSet(Action<T> setterExpression)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression, ConstructorArguments);
        ValidateSetterExpression(expression);

        var setup = Setup(this, expression);
        return new Setup<T>(setup);
    }

    public ISetterSetup<T, TProperty> SetupSet<TProperty>(Action<T> setterExpression)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression, ConstructorArguments);
        ValidateSetterExpression(expression);

        var setup = Setup(this, expression);
        return new SetterSetup<T, TProperty>(setup);
    }

    public Mimic<T> SetupProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty? initialValue = default)
    {
        Guard.NotNull(propertyExpression);

        if (propertyExpression.Body is not MemberExpression { Member: PropertyInfo property })
            throw MimicException.ExpressionNotProperty(propertyExpression);

        if (!property.CanReadProperty(out var getter, out _))
            throw MimicException.ExpressionNotPropertyGetter(property);

        if (!property.CanWriteProperty(out var setter, out _))
            throw MimicException.ExpressionNotPropertySetter(property);

        SetupRecursive(this, propertyExpression, (target, _, _) =>
        {
            var setup = new PropertyStubSetup(target, propertyExpression, getter, setter, initialValue);
            target.Setups.Add(setup);
            return setup;
        });

        return this;
    }

    public Mimic<T> SetupAllProperties()
    {
        _setups.Add(new AllPropertiesStubSetup(this));
        return this;
    }

    internal static MethodCallSetup Setup(Mimic<T> mimic, LambdaExpression expression, Func<bool>? condition = null)
    {
        Guard.NotNull(expression);

        return SetupRecursive(mimic, expression, (target, originalExpression, expectation) =>
        {
            var setup = new MethodCallSetup(originalExpression, target, expectation, condition);
            target.Setups.Add(setup);
            return setup;
        });
    }

    internal static void ValidateSetterExpression(Expression<Action<T>> expression)
    {
        Guard.NotNull(expression);

        if (expression.Body is BinaryExpression { NodeType: ExpressionType.Assign, Left: MemberExpression or IndexExpression })
                return;

        if (expression.Body.NodeType is ExpressionType.Call)
        {
            var call = (MethodCallExpression)expression.Body;
            if (call.Method.IsSetter())
                return;
        }

        throw MimicException.ExpressionNotPropertySetter(expression);
    }

    private static TSetup SetupRecursive<TSetup>(
        Mimic<T> mimic, LambdaExpression expression, Func<IMimic, Expression, MethodExpectation, TSetup> setup)
        where TSetup : SetupBase
    {
        Guard.NotNull(mimic);
        Guard.NotNull(expression);
        Guard.NotNull(setup);

        return SetupRecursive(mimic, expression, ExpressionSplitter.Split(expression), setup);
    }

    private static TSetup SetupRecursive<TSetup>(IMimic mimic, LambdaExpression originalExpression, Stack<MethodExpectation> expectations, Func<IMimic, Expression, MethodExpectation, TSetup> setup) where TSetup : SetupBase
    {
        while (true)
        {
            var expectation = expectations.Pop();
            if (expectations.Count == 0)
                return setup(mimic, originalExpression, expectation);

            var nested = mimic.Setups.FindLast(s => s.Expectation.Equals(expectation))?.GetNested().SingleOrDefault();
            if (nested is null)
            {
                object? returnValue = GetReturnValueForMethod(expectation.MethodInfo, mimic, out nested);
                if (returnValue is null || nested is null)
                    throw MimicException.TypeCannotBeMimicked(expectation.MethodInfo.ReturnType);

                mimic.Setups.Add(new NestedSetup(originalExpression, mimic, expectation, returnValue));
            }

            Guard.NotNull(nested);
            mimic = nested;
        }

        static object? GetReturnValueForMethod(MethodInfo method, IMimic mimic, out IMimic? nested)
        {
            Guard.NotNull(method);
            Guard.NotNull(method.ReturnType);
            Guard.Assert(method.ReturnType != typeof(void));

            nested = null;

            object? emptyValue = DefaultValueFactory.GetDefaultValue(method.ReturnType);
            if (emptyValue != null)
                return emptyValue;

            if (!method.ReturnType.CanBeMimicked())
                return null;

            var newMimicType = typeof(Mimic<>).MakeGenericType(method.ReturnType);
            var newMimic = (IMimic?)Activator.CreateInstance(newMimicType, args: [mimic.Strict]);
            Guard.NotNull(newMimic);

            nested = newMimic;
            return newMimic.Object;
        }
    }
}
