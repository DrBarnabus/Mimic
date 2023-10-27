using Mimic;

var mimic = new Mimic<ITypeToMimic>();

mimic.Setup(m => m.StringMethod(Arg.Any<string>()))
    .Callback(() => Console.WriteLine($"Callback before {nameof(ITypeToMimic.StringMethod)} returns"))
    .Returns((string a) =>
    {
        Console.WriteLine($"The return function for {nameof(ITypeToMimic.StringMethod)} which was called with {a}");
        return Task.FromResult($"Value is: {a}");
    })
    .Callback((string a) => Console.WriteLine($"Callback after {nameof(ITypeToMimic.StringMethod)} returns. Called with {a}"));

mimic.Setup(m => m.ThrowsException(Arg.Any<string>()))
    .Throws((string innerMessage) => new Exception($"Test Exception {innerMessage}"));

mimic.SetupAllProperties();

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

mimickedObject.Property = "Test?";
Console.WriteLine(mimickedObject.Property);

public interface ITypeToMimic
{
    string Property { get; set; }

    void VoidMethod();

    Task<string> StringMethod(string value);

    void ThrowsException(string innerMessage);
}
