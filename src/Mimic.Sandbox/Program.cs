using Mimic;

var mimic = new Mimic<ITypeToMimic>();

mimic.Setup(m => m.StringMethod(Arg.Is<string[]>(v => v[0] == "constant" && v[1] == "otherconstant")))
    .Returns(Task.FromResult("TestValueConstant"));

mimic.Setup(m => m.StringMethod("other"))
    .Returns(Task.FromResult("TestValue"));

var mimickedObject = mimic.Object;
mimickedObject.VoidMethod();

string result = await mimickedObject.StringMethod("other");
Console.WriteLine(result);

string secondResult = await mimickedObject.StringMethod("constant", "otherconstant");
Console.WriteLine(secondResult);

public interface ITypeToMimic
{
    void VoidMethod();

    Task<string> StringMethod(params string?[] values);
}
