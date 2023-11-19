using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IGetterCallbackResult<in TProperty> : IGetterReturns<TProperty>, IThrows, ILimitable, IVerifiable, IFluent
{
}
