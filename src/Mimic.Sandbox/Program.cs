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
    .Expected();

mimic.Setup(m => m.VoidMethod())
    .Expected();

var reference = mimic.Setup(m => m.Sequence()).AsSequence()
    .Next()
    .Next();

reference.Throws(new Exception("Test Exception from void?"));

mimic.SetupAllProperties();

mimic.Setup(m => m.Generic<Generic.AnyType>())
    .Expected();

SetupRef(mimic);

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
    Console.WriteLine($"Exception thrown with message: {ex.Message}");
}

mimickedObject.Generic<int>();

int refInt = 10;
mimickedObject.Ref(ref refInt, out string outResult);
Console.WriteLine(outResult);

mimic.VerifyExpectedReceived();

static void SetupRef(Mimic<ITypeToMimic> mimic1)
{
    string outValue = "OutValue?";
    mimic1.Setup(m => m.Ref(ref Arg.Ref<int>.Any, out outValue));
}

public interface ITypeToMimic
{
    string Property { get; set; }

    void VoidMethod();

    Task<string> StringMethod(string value);

    void ThrowsException(string innerMessage);

    void Sequence();

    void Generic<T>();

    void Ref(ref int reference, out string outValue);
}
