using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterReturns<TMimic, in TProperty> : IFluent
    where TMimic : class
{
    IReturnsResult<TMimic> Returns(TProperty? value);

    IReturnsResult<TMimic> Returns(Func<TProperty?> valueFunction);
}
