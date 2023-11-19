using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturnsResult<TMimic> : ICallback, ILimitable, IVerifiable, IFluent
    where TMimic : class
{
}
