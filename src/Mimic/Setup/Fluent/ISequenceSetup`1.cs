using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISequenceSetup<in TResult> : IReturns<TResult, ISequenceSetup<TResult>>, IThrows<ISequenceSetup<TResult>>, IVerifiable, IFluent
{
}
