using System.Diagnostics;
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
    .WithDelay(TimeSpan.FromMilliseconds(400))
    .Expected();

mimic.Setup(m => m.Generic<string>())
    .WithDelay(TimeSpan.FromMilliseconds(800), TimeSpan.FromMilliseconds(1600))
    .Expected();

mimic.Setup(m => m.Generic<byte>())
    .Expected();

SetupRef(mimic);

mimic.Setup(m => m.NestingTest().NestedMethod())
    .Returns("Returned from the nested method call")
    .Expected();

var mimickedObject = mimic.Object;
mimickedObject.VoidMethod();

bool isMimickedNestedType = mimickedObject.NestingTest() is IMimicked<INestedType>;
Console.WriteLine($"Is IMimicked<INestedType> {isMimickedNestedType}");
Console.WriteLine(mimickedObject.NestingTest().NestedMethod());

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

var sw = Stopwatch.StartNew();
mimickedObject.Generic<int>();
Console.WriteLine($"Time taken with int {sw.ElapsedMilliseconds}ms");

sw = Stopwatch.StartNew();
mimickedObject.Generic<string>();
Console.WriteLine($"Time taken with string {sw.ElapsedMilliseconds}ms");

sw = Stopwatch.StartNew();
mimickedObject.Generic<byte>();
Console.WriteLine($"Time taken with byte {sw.ElapsedMilliseconds}ms");

int refInt = 10;
mimickedObject.Ref(ref refInt, out string outResult);
Console.WriteLine(outResult);

mimic.VerifyExpectedReceived();

ExampleUsingAnAbstractClass();

static void SetupRef(Mimic<ITypeToMimic> mimic1)
{
    string outValue = "OutValue?";
    mimic1.Setup(m => m.Ref(ref Arg.Ref<int>.Any, out outValue));
}

static void ExampleUsingAnAbstractClass()
{
    var mimic = new Mimic<AbstractClass>();

    mimic.Setup(m => m.VirtualVoidMethod()).AsSequence()
        .Next()
        .Proceed()
        .Next();

    mimic.Setup(m => m.VirtualStringMethod())
        .Proceed();

    var mimickedObject = mimic.Object;

    mimickedObject.VirtualVoidMethod();
    mimickedObject.VirtualVoidMethod();
    mimickedObject.VirtualVoidMethod();

    Console.WriteLine($"Result from {nameof(AbstractClass.VirtualStringMethod)}: {mimickedObject.VirtualStringMethod()}");
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

    INestedType NestingTest();
}

public interface INestedType
{
    public string NestedMethod();
}

public abstract class AbstractClass
{
    public virtual void VirtualVoidMethod() => Console.WriteLine($"Hello from {nameof(VirtualVoidMethod)}");

    public virtual string VirtualStringMethod() => "ValueFromBase";
}
