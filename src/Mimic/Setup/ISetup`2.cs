namespace Mimic.Setup;

public interface ISetup<T, TResult>
    where T : class
{
    // TODO: Temporary will be replaced with a proper API
    public ISetup<T, TResult> Returns(TResult? value);
}
