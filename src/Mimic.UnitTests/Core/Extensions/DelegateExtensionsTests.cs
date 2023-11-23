using System.Reflection;
using Mimic.Core.Extensions;

namespace Mimic.UnitTests.Core.Extensions;

public class DelegateExtensionsTests
{
    private delegate string DelegateThatDoesNotThrow(string strValue, int intValue);
    private delegate void DelegateThatThrows();

    [Theory]
    [AutoData]
    public void Invoke_WithDelegateThatDoesNotThrow_ReturnsCorrectResponse(string strValue, int intValue)
    {
        var @delegate = new DelegateThatDoesNotThrow((s, i) => $"{s} - {i}") as Delegate;
        @delegate.Invoke(new object[] { strValue, intValue }).ShouldBe($"{strValue} - {intValue}");
    }

    [Theory]
    [AutoData]
    public void Invoke_WithDelegateThatThrows_ThrowsInnerExceptionFromDelegate(string message)
    {
        var @delegate = new DelegateThatThrows(() => throw new Exception(message)) as Delegate;

        var ex = Should.Throw<Exception>(() => @delegate.Invoke());
        ex.ShouldNotBeOfType<TargetInvocationException>();
        ex.Message.ShouldBe(message);
    }

    [Fact]
    public void CompareParameterTypesTo_WithMatchingParameterTypes_ShouldReturnTrue()
    {
        var delegateWithParameters = new DelegateThatDoesNotThrow((s, i) => $"{s} - {i}") as Delegate;
        delegateWithParameters.CompareParameterTypesTo(new[] { typeof(string), typeof(int) }).ShouldBeTrue();

        var delegateThatThrows = new DelegateThatThrows(() => throw new Exception()) as Delegate;
        delegateThatThrows.CompareParameterTypesTo(Type.EmptyTypes).ShouldBeTrue();
    }

    [Fact]
    public void CompareParameterTypesTo_WithNonMatchingParameterTypes_ShouldReturnFalse()
    {
        var delegateWithParameters = new DelegateThatDoesNotThrow((s, i) => $"{s} - {i}") as Delegate;
        delegateWithParameters.CompareParameterTypesTo(new[] { typeof(object), typeof(char) }).ShouldBeFalse();

        var delegateThatThrows = new DelegateThatThrows(() => throw new Exception()) as Delegate;
        delegateThatThrows.CompareParameterTypesTo(new[] { typeof(int) }).ShouldBeFalse();
    }
}
