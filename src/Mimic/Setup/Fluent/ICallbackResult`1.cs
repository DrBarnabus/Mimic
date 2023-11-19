using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallbackResult<in TResult> : IReturns<TResult>, IThrows, ILimitable, IVerifiable, IFluent
{
}
