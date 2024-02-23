namespace Mimic;

[PublicAPI]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IProceed : IFluent
{
    IProceedResult Proceed();
}
