namespace Mimic;

[PublicAPI]
public interface IGenericMatcher
{
    bool Matches(Type genericType);
}
