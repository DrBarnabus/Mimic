﻿using Mimic.Setup.Fluent;

namespace Mimic.Setup;

internal sealed class SetterSetup<TMimic, TProperty> : VoidSetup<TMimic>, ISetterSetup<TMimic, TProperty>
    where TMimic : class
{
    public SetterSetup(MethodCallSetup setup)
        : base(setup)
    {
    }

    public ICallbackResult Callback(Action<TProperty> callback)
    {
        Setup.SetCallbackBehaviour(callback);
        return this;
    }
}
