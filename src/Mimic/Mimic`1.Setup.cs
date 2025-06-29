using Mimic.Expressions;
using Mimic.Setup;
using Mimic.Setup.Fluent;
using SetupBase = Mimic.Setup.SetupBase;

namespace Mimic;

public partial class Mimic<T>
{
    /// <summary>
    /// Sets up a method or property on the mocked object to be configured with behaviours.
    /// </summary>
    /// <param name="expression">An expression that specifies the method or property to set up.</param>
    /// <returns>An <see cref="ISetup{T}"/> that can be used to configure the behaviour of the specified method or property.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="expression"/> is null.</exception>
    public ISetup<T> Setup(Expression<Action<T>> expression)
    {
        var setup = Setup(this, expression);
        return new Setup<T>(setup);
    }

    /// <summary>
    /// Sets up a method or property on the mocked object that returns a value to be configured with behaviours.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value of the method or property being set up.</typeparam>
    /// <param name="expression">An expression that specifies the method or property to set up.</param>
    /// <returns>An <see cref="ISetup{T, TResult}"/> that can be used to configure the behaviour and return value of the specified method or property.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="expression"/> is null.</exception>
    public ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression)
    {
        var setup = Setup(this, expression);
        return new Setup<T, TResult>(setup);
    }

    /// <summary>
    /// Sets up a property getter on the mocked object to be configured with behaviours.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being set up.</typeparam>
    /// <param name="expression">An expression that specifies the property getter to set up.</param>
    /// <returns>An <see cref="IGetterSetup{T, TProperty}"/> that can be used to configure the behaviour of the specified property getter.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="expression"/> is null.</exception>
    /// <exception cref="MimicException">Thrown when the expression does not represent a property or the property cannot be read.</exception>
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

    /// <summary>
    /// Sets up a property setter on the mocked object to be configured with behaviours.
    /// </summary>
    /// <param name="setterExpression">An action that specifies the property setter to set up.</param>
    /// <returns>An <see cref="ISetup{T}"/> that can be used to configure the behaviour of the specified property setter.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="setterExpression"/> is null.</exception>
    /// <exception cref="MimicException">Thrown when the expression does not represent a valid property setter.</exception>
    public ISetup<T> SetupSet(Action<T> setterExpression)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression, ConstructorArguments);
        ValidateSetterExpression(expression);

        var setup = Setup(this, expression);
        return new Setup<T>(setup);
    }

    /// <summary>
    /// Sets up a property setter on the mocked object to be configured with behaviours, with strongly typed property support.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being set up.</typeparam>
    /// <param name="setterExpression">An action that specifies the property setter to set up.</param>
    /// <returns>An <see cref="ISetterSetup{T, TProperty}"/> that can be used to configure the behaviour of the specified property setter.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="setterExpression"/> is null.</exception>
    /// <exception cref="MimicException">Thrown when the expression does not represent a valid property setter.</exception>
    public ISetterSetup<T, TProperty> SetupSet<TProperty>(Action<T> setterExpression)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression, ConstructorArguments);
        ValidateSetterExpression(expression);

        var setup = Setup(this, expression);
        return new SetterSetup<T, TProperty>(setup);
    }

    /// <summary>
    /// Sets up a property on the mocked object to behave like a real property with automatic getter and setter behaviour.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being set up.</typeparam>
    /// <param name="propertyExpression">An expression that specifies the property to set up.</param>
    /// <param name="initialValue">The initial value to assign to the property. Defaults to the default value of <typeparamref name="TProperty"/>.</param>
    /// <returns>The current <see cref="Mimic{T}"/> instance for method chaining.</returns>
    /// <exception cref="Guard.AssertionException">Thrown when <paramref name="propertyExpression"/> is null.</exception>
    /// <exception cref="MimicException">Thrown when the expression does not represent a property, or the property cannot be read or written.</exception>
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

    /// <summary>
    /// Sets up all properties on the mocked object to behave like real properties with automatic getter and setter behaviour.
    /// </summary>
    /// <returns>The current <see cref="Mimic{T}"/> instance for method chaining.</returns>
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
