using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallbackResult : IThrows, IThrowsResult, ILimitable, IVerifiable, IFluent
{
}
