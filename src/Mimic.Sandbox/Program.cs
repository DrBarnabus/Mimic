using Mimic;

var mimic = new Mimic<ITypeToMimic>();

mimic.Setup(m => m.StringMethod(Arg.Any<string>()))
    .Returns((string a) => Task.FromResult($"Value is: {a}"));

mimic.Setup(m => m.ThrowsException(Arg.Any<string>()))
    .Throws((string innerMessage) => new Exception($"Test Exception {innerMessage}"));

var mimickedObject = mimic.Object;
mimickedObject.VoidMethod();

string result = await mimickedObject.StringMethod("other");
Console.WriteLine(result);

string secondResult = await mimickedObject.StringMethod("constant");
Console.WriteLine(secondResult);

try
{
    mimickedObject.ThrowsException("with inner message");
}
catch (Exception ex)
{
    Console.WriteLine($"Exception thrown with message: {ex.Message}");
}

public interface ITypeToMimic
{
    void VoidMethod();

    Task<string> StringMethod(string value);

    void ThrowsException(string innerMessage);
}
