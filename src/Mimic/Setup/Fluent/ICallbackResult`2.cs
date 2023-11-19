using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallbackResult<TMimic, in TResult> : IReturns<TMimic, TResult>, IThrows, ILimitable, IVerifiable, IFluent
    where TMimic : class
{
}
