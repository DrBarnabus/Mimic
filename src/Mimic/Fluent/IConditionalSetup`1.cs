namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IConditionalSetup<TMimic>
    where TMimic : class
{
    ISetup<TMimic> Setup(Expression<Action<TMimic>> expression);

    ISetup<TMimic, TResult> Setup<TResult>(Expression<Func<TMimic, TResult>> expression);

    IGetterSetup<TMimic, TProperty> SetupGet<TProperty>(Expression<Func<TMimic, TProperty>> expression);

    ISetup<TMimic> SetupSet(Action<TMimic> setterExpression);

    ISetterSetup<TMimic, TProperty> SetupSet<TProperty>(Action<TMimic> setterExpression);
}
