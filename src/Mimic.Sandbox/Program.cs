using Mimic;

var mimic = new Mimic<ITypeToMimic>();

mimic.Setup(m => m.StringMethod(Arg.Any<string>()))
    .Returns((string a) => Task.FromResult($"Value is: {a}"));

var mimickedObject = mimic.Object;
mimickedObject.VoidMethod();

string result = await mimickedObject.StringMethod("other");
Console.WriteLine(result);

string secondResult = await mimickedObject.StringMethod("constant");
Console.WriteLine(secondResult);

public interface ITypeToMimic
{
    void VoidMethod();

    Task<string> StringMethod(string value);
}
