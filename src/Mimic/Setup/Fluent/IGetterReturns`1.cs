using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterReturns<in TProperty> : IFluent
{
    IReturnsResult Returns(TProperty? value);

    IReturnsResult Returns(Func<TProperty?> valueFunction);
}
