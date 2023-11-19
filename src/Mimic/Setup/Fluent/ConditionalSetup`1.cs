using Mimic.Expressions;

namespace Mimic.Setup.Fluent;

internal sealed class ConditionalSetup<TMimic> : IConditionalSetup<TMimic>
    where TMimic : class
{
    private readonly Mimic<TMimic> _mimic;
    private readonly Func<bool> _condition;

    public ConditionalSetup(Mimic<TMimic> mimic, Func<bool> condition)
    {
        _mimic = mimic;
        _condition = condition;
    }

    public ISetup<TMimic> Setup(Expression<Action<TMimic>> expression)
    {
        var setup = Mimic<TMimic>.Setup(_mimic, expression, _condition);
        return new VoidSetup<TMimic>(setup);
    }

    public ISetup<TMimic, TResult> Setup<TResult>(Expression<Func<TMimic, TResult>> expression)
    {
        var setup = Mimic<TMimic>.Setup(_mimic, expression, _condition);
        return new NonVoidSetup<TMimic, TResult>(setup);
    }

    public IGetterSetup<TMimic, TProperty> SetupGet<TProperty>(Expression<Func<TMimic, TProperty>> expression)
    {
        Guard.NotNull(expression);

        if (expression.Body is not (IndexExpression or MethodCallExpression { Method.IsSpecialName: true }))
        {
            if (expression.Body is not MemberExpression { Member: PropertyInfo property })
                throw MimicException.ExpressionNotProperty(expression);

            if (!property.CanReadProperty(out _, out _))
                throw MimicException.ExpressionNotPropertyGetter(property);
        }

        var setup = Mimic<TMimic>.Setup(_mimic, expression, _condition);
        return new NonVoidSetup<TMimic, TProperty>(setup);
    }

    public ISetup<TMimic> SetupSet(Action<TMimic> setterExpression)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression);
        Mimic<TMimic>.ValidateSetterExpression(expression);

        var setup = Mimic<TMimic>.Setup(_mimic, expression, _condition);
        return new VoidSetup<TMimic>(setup);
    }

    public ISetterSetup<TMimic, TProperty> SetupSet<TProperty>(Action<TMimic> setterExpression)
    {
        Guard.NotNull(setterExpression);

        var expression = SetterExpressionConstructor.ConstructFromAction(setterExpression);
        Mimic<TMimic>.ValidateSetterExpression(expression);

        var setup = Mimic<TMimic>.Setup(_mimic, expression, _condition);
        return new SetterSetup<TMimic, TProperty>(setup);
    }
}
