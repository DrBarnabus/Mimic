﻿using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetterSetup<TMimic, out TProperty> : ISetterCallback<TMimic, TProperty>, ILimitable, IVerifiable, IFluent
    where TMimic : class
{
}
