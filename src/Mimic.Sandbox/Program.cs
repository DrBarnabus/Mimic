using Mimic;

var mimickedType = new Mimic<Wrapper.ITypeToMimic<int>>();
UseInterface(mimickedType.Object);

void UseInterface(Wrapper.ITypeToMimic<int> typeToMimic)
{
    typeToMimic.Method();

    var mock = (typeToMimic as IMimicked<Wrapper.ITypeToMimic<int>>).Mimic;
    Console.WriteLine($"Mock: {mock.Name}");
}

public static class Wrapper
{
    public interface ITypeToMimic<TValue>
    {
        void Method();
    }
}
