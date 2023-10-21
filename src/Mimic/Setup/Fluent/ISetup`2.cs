﻿using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetup<TMimic, in TResult> : IReturns<TMimic, TResult>, IThrows, IFluent
    where TMimic : class
{
}
