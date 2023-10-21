namespace Mimic.Setup;

internal class NonVoidSetup<T, TResult> : SetupBase, ISetup<T, TResult>
    where T : class
{
    public NonVoidSetup(MethodCallSetup setup)
        : base(setup)
    {
    }

    // TODO: Temporary will be replaced with a proper API
    public ISetup<T, TResult> Returns(TResult? value)
    {
        Setup.SetReturnValueBehaviour(value);
        return this;
    }
}
