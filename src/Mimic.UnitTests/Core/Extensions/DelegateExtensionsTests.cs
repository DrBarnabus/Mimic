using System.Reflection;
using Mimic.Core.Extensions;
using Mimic.Exceptions;

namespace Mimic.UnitTests.Core.Extensions;

public class DelegateExtensionsTests
{
    private delegate string DelegateThatDoesNotThrow(string strValue, int intValue);

    private delegate void DelegateThatThrows();

    private delegate void DelegateWithParameters(string sValue, int iValue, bool bValue);

    private delegate long DelegateWithParametersAndLongReturn();

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

    [Fact]
    public void ValidateDelegateParameterCount_WhenDelegateHasExpectedNumberOfParameters_ShouldNotThrow()
    {
        Delegate @delegate = new DelegateWithParameters((_, _, _) => {});

        Should.NotThrow(() => @delegate.ValidateDelegateParameterCount(3));
    }

    [Fact]
    public void ValidateDelegateParameterCount_WhenDelegateHasWrongNumberOfParameters_ShouldThrow()
    {
        Delegate @delegate = new DelegateWithParameters((_, _, _) => {});

        var ex = Should.Throw<MimicException>(() => @delegate.ValidateDelegateParameterCount(2));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with 2 expected parameter(s) cannot invoke a callback method with 3 parameter(s).");
    }

    [Fact]
    public void ValidateDelegateParameterCount_WhenCalledWithDelegateOfAnExtensionMethod_AndCorrectNumberOfParametersIgnoringThisParameter_ShouldNotThrow()
    {
        Delegate @delegate = DelegateExtensions.ValidateDelegateParameterCount;

        Should.NotThrow(() => @delegate.ValidateDelegateParameterCount(1));
    }

    [Fact]
    public void ValidateDelegateReturnType_WhenCalledWithDelegateThatReturnsVoid_ShouldThrow()
    {
        Delegate @delegate = new DelegateWithParameters((_, _, _) => {});

        var ex = Should.Throw<MimicException>(() => @delegate.ValidateDelegateReturnType(typeof(string)));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with return type 'string' cannot invoke a callback method with a void return type.");
    }

    [Fact]
    public void ValidateDelegateReturnType_WhenCalledWithDelegateThatReturnsLong_AndIntIsExpectedReturnType_ShouldThrow()
    {
        Delegate @delegate = new DelegateWithParametersAndLongReturn(() => 10L);

        var ex = Should.Throw<MimicException>(() => @delegate.ValidateDelegateReturnType(typeof(int)));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with return type 'int' cannot invoke a callback method with return type 'long'.");
    }

    [Fact]
    public void ValidateDelegateReturnType_WhenCalledWithDelegateThatReturnsLong_AndLongIsExpectedReturnType_ShouldNotThrow()
    {
        Delegate @delegate = new DelegateWithParametersAndLongReturn(() => 10L);

        Should.NotThrow(() => @delegate.ValidateDelegateReturnType(typeof(long)));
    }

    [Fact]
    public void ValidateDelegateReturnType_WhenCalledWithDelegateThatReturnsInt_AndLongIsExpectedReturnType_ShouldNotThrow()
    {
        Delegate @delegate = new DelegateWithParametersAndLongReturn(() => 10);

        Should.NotThrow(() => @delegate.ValidateDelegateReturnType(typeof(long)));
    }
}
