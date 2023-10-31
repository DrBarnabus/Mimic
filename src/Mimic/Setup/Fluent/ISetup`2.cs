using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetup<TMimic, in TResult> : ICallback<TMimic, TResult>, IReturns<TMimic, TResult>, IThrows, IVerifiable, IFluent
    where TMimic : class
{
}
