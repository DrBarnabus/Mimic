using Mimic;

var mimic = new Mimic<ITypeToMimic>();

mimic.Setup(m => m.StringMethod(Arg.Any<string>()))
    .Callback(() => Console.WriteLine($"Callback before {nameof(ITypeToMimic.StringMethod)} returns"))
    .Returns((string a) => $"Value is: {a}")
    .Callback((string a) => Console.WriteLine($"Callback after {nameof(ITypeToMimic.StringMethod)} returns. Called with {a}"))
    .Limit(2);

bool shouldThrow = false;
mimic
    .When(() => shouldThrow)
    .Setup(m => m.ThrowsException(Arg.Any<string>()))
    .Throws((string innerMessage) => new Exception($"Test Exception {innerMessage}"));

mimic
    .When(() => !shouldThrow)
    .Setup(m => m.ThrowsException(Arg.Any<string>()))
    .Callback(() => Console.WriteLine("Shouldn't have thrown?"))
    .Verifiable();

mimic.Setup(m => m.VoidMethod())
    .Verifiable();

var reference = mimic.Setup(m => m.Sequence()).AsSequence()
    .Next()
    .Next();

reference.Throws(new Exception("Test Exception from void?"));

mimic.SetupAllProperties();

var mimickedObject = mimic.Object;
mimickedObject.VoidMethod();

string result = await mimickedObject.StringMethod("other");
Console.WriteLine(result);

string secondResult = await mimickedObject.StringMethod("constant");
Console.WriteLine(secondResult);

mimickedObject.ThrowsException("shouldn't throw");

shouldThrow = true;

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

mimickedObject.Sequence();
mimickedObject.Sequence();

try
{
    mimickedObject.Sequence();
}
catch (Exception ex)
{
    Console.Write($"Exception thrown with message: {ex.Message}");
}

mimic.Verify();

public interface ITypeToMimic
{
    string Property { get; set; }

    void VoidMethod();

    Task<string> StringMethod(string value);

    void ThrowsException(string innerMessage);

    void Sequence();
}
