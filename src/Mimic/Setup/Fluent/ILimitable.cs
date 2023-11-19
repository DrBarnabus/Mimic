﻿using System.ComponentModel;
using JetBrains.Annotations;

namespace Mimic.Setup.Fluent;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ILimitable : IFluent
{
    IVerifiable Limit(int executionLimit = 1);
}