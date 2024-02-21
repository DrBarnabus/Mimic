using System.Linq.Expressions;
using Mimic.Core;
using Mimic.Exceptions;
using Mimic.Setup;

namespace Mimic.UnitTests.Setup;

public class MethodCallSetupTests
{
    [Theory]
    [AutoData]
    public void Constructor_ShouldSuccessfullyConstruct(int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, mimic, originalExpression, expectation) = ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(iValue, sValue, dValue, bValues));

        setup.OriginalExpression.ShouldBeSameAs(originalExpression);
        setup.Mimic.ShouldBeSameAs(mimic);
        setup.Expectation.ShouldBeSameAs(expectation);
        setup.Expression.ShouldBeSameAs(expectation.Expression);
        setup.Matched.ShouldBeFalse();
        setup.Overridden.ShouldBeFalse();
        setup.Expected.ShouldBeFalse();
        setup.MethodInfo.ShouldBeSameAs(expectation.MethodInfo);
    }

    #region MatchesInvocation

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenConditionFuncIsNull_WhenCalledWithMatchingInvocation_ShouldReturnTrue(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(iValue, Arg.AnyNotNull<string>(), dValue, Arg.Is<List<bool>>(l => l.Count == bValues.Count)));

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenConditionFuncReturnsFalse_WhenCalledWithMatchingInvocation_ShouldReturnFalse(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m =>
                m.BasicVoidMethod(iValue, Arg.AnyNotNull<string>(), dValue, Arg.Is<List<bool>>(l => l.Count == bValues.Count)),
            () => false);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenConditionFuncReturnsTrue_WhenCalledWithMatchingInvocation_ShouldReturnTrue(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m =>
                m.BasicVoidMethod(iValue, Arg.AnyNotNull<string>(), dValue, Arg.Is<List<bool>>(l => l.Count == bValues.Count)),
            () => true);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.MatchesInvocation(invocation).ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenConditionFuncIsNull_WhenCalledWithNonMatchingInvocation_ShouldReturnFalse(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(iValue, Arg.AnyNotNull<string>(), dValue, Arg.Is<List<bool>>(l => l.Count != bValues.Count)));

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenConditionFuncReturnsFalse_WhenCalledWithNonMatchingInvocation_ShouldReturnFalse(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m =>
                m.BasicVoidMethod(iValue, Arg.AnyNotNull<string>(), dValue, Arg.Is<List<bool>>(l => l.Count != bValues.Count)),
            () => false);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.MatchesInvocation(invocation).ShouldBeFalse();
    }

    [Theory]
    [AutoData]
    public void MatchesInvocation_WhenConditionFuncReturnsTrue_WhenCalledWithNonMatchingInvocation_ShouldReturnFalse(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m =>
                m.BasicVoidMethod(iValue, Arg.AnyNotNull<string>(), dValue, Arg.Is<List<bool>>(l => l.Count != bValues.Count)),
            () => true);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.MatchesInvocation(invocation).ShouldBeFalse();
    }

    #endregion

    #region Execute

    [Theory]
    [AutoData]
    public void Execute_ShouldSetMatchedToTrue(int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(iValue, Arg.AnyNotNull<string>(), dValue, Arg.Is<List<bool>>(l => l.Count == bValues.Count)));

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.Execute(invocation);

        setup.Matched.ShouldBeTrue();
        invocation.MatchedSetup.ShouldBeSameAs(setup);
    }

    [Theory]
    [AutoData]
    public void Execute_WhenMethodHasOutParameters_ShouldCorrectlyUpdateInvocationArgumentsWithOutParameterValues(
        decimal dValue, int iValue, bool bValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m =>
            m.OutParametersMethod(dValue, ref bValue, out iValue));

        object[] arguments = [dValue, !bValue, ~iValue];
        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.OutParametersMethod), arguments);
        setup.Execute(invocation);

        arguments[0].ShouldBe(dValue);
        arguments[1].ShouldBe(!bValue);
        arguments[2].ShouldBe(iValue);
    }

    [Fact]
    public void Execute_WhenSetupHasExeuctionLimit_AndExecutionLimitIsNotExceeded_ShouldNotThrow()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        setup.SetExecutionLimitBehaviour(1);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod));
        Should.NotThrow(() => setup.Execute(invocation));
    }

    [Theory]
    [InlineData(1, "execution")]
    [InlineData(3, "executions")]
    public void Execute_WhenSetupHasExeuctionLimit_AndExecutionLimitIsExceeded_ShouldThrow(int executionLimit, string expectedPlural)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        setup.SetReturnValueBehaviour(null);
        setup.SetExecutionLimitBehaviour(executionLimit);

        for (int i = 0; i < executionLimit; i++)
            setup.Execute(InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod)));

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        var ex = Should.Throw<MimicException>(() => setup.Execute(invocation));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe($"Setup 'MethodCallSetupTests.ISubject m => m.ParameterlessMethod()' has been limited to {executionLimit} {expectedPlural} but was actually executed {executionLimit + 1} times");
    }

    [Fact]
    public void Execute_WhenSetupHasCallbacks_ShouldInvokeCallbacksAndTheyShouldBeInvokedInTheCorrectOrder()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        // Pre-return callback
        bool preReturnCallbackCalled = false;
        setup.SetCallbackBehaviour(() =>
        {
            preReturnCallbackCalled = true;
        });

        setup.SetReturnValueBehaviour(null);

        // Post-return callback
        bool postReturnCallbackCalled = false;
        setup.SetCallbackBehaviour(() =>
        {
            preReturnCallbackCalled.ShouldBeTrue();
            postReturnCallbackCalled = true;
        });

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        setup.Execute(invocation);

        preReturnCallbackCalled.ShouldBeTrue();
        postReturnCallbackCalled.ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void Execute_WhenSetupHasCallback_ShouldCorrectlyInvokeTheCallbackWithTheInvocationsArguments(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicVoidMethod(iValue, sValue, dValue, bValues));

        bool preReturnCallbackCalled = false;
        setup.SetCallbackBehaviour((int iVal, string sVal, double dVal, List<bool> bVals) =>
        {
            preReturnCallbackCalled = true;

            iVal.ShouldBe(iValue);
            sVal.ShouldBe(sValue);
            dVal.ShouldBe(dValue);
            bVals.ShouldBeSameAs(bValues);
        });

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.Execute(invocation);

        preReturnCallbackCalled.ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void Execute_WhenSetupHasReturnValue_ShouldCorrectlySetReturnValueOfInvocation(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        setup.SetReturnValueBehaviour(returnValue);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        setup.Execute(invocation);

        invocation.ReturnValue.ShouldBe(returnValue);
    }

    [Fact]
    public void Execute_WhenSetupHasNoReturnValue_AndMethodIsNonVoidReturn_AndStrictIsTrue_ShouldThrow()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod(), strict: true);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        var ex = Should.Throw<MimicException>(() => setup.Execute(invocation));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Invocation of 'MethodCallSetupTests.ISubject.ParameterlessMethod()' failed. Invocation needs to return a non-void value but there is no corresponding setup that provides one");
    }

    [Fact]
    public void Execute_WhenSetupHasNoReturnValue_AndMethodIsNonVoidReturn_AndStrictIsFalse_ShouldNotThrowAndReturnsDefaultValue()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod(), strict: false);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        Should.NotThrow(() => setup.Execute(invocation));

        invocation.ReturnValue.ShouldBe(null);
    }

    [Fact]
    public void Execute_WhenSetupHasNoReturnValue_AndMethodIsVoidReturn_AndStrictIsTrue_ShouldNotThrow()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessVoidMethod(), strict: true);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessVoidMethod));
        Should.NotThrow(() => setup.Execute(invocation));
    }

    #endregion

    #region SetBehaviours


    #region SetReturnValueBehaviour

    [Theory]
    [AutoData]
    public void SetReturnValueBehaviour_WhenMethodIsVoidReturnType_ShouldThrowAssertionException(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessVoidMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetReturnValueBehaviour(returnValue));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("MethodInfo.ReturnType != typeof(void)");
    }

    [Theory]
    [AutoData]
    public void SetReturnValueBehaviour_WhenReturnValueIsAlreadySet_ShouldThrowAssertionException(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());
        setup.SetThrowExceptionBehaviour(new Exception());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetReturnValueBehaviour(returnValue));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("_returnOrThrow is null");
    }

    [Theory]
    [AutoData]
    public void SetReturnValueBehaviour_ShouldCorrectlySetReturnValueOnInvocationAfterExecution(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        setup.SetReturnValueBehaviour(returnValue);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        setup.Execute(invocation);

        invocation.ReturnValue.ShouldBe(returnValue);
    }

    #endregion

    #region SetReturnComputedValueBehaviour

    [Theory]
    [AutoData]
    public void SetReturnComputedValueBehaviour_WhenMethodIsVoidReturnType_ShouldThrowAssertionException(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessVoidMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetReturnComputedValueBehaviour(() => returnValue));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("MethodInfo.ReturnType != typeof(void)");
    }

    [Theory]
    [AutoData]
    public void SetReturnComputedValueBehaviour_WhenReturnValueIsAlreadySet_ShouldThrowAssertionException(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());
        setup.SetThrowExceptionBehaviour(new Exception());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetReturnComputedValueBehaviour(() => returnValue));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("_returnOrThrow is null");
    }

    [Theory]
    [AutoData]
    public void SetReturnComputedValueBehaviour_WithNoArguments_ShouldCorrectlySetReturnValueOnInvocationAfterExecution(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        setup.SetReturnComputedValueBehaviour(() => returnValue);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        setup.Execute(invocation);

        invocation.ReturnValue.ShouldBe(returnValue);
    }

    [Fact]
    public void SetReturnComputedValueBehaviour_WithReturnTypeOfDelegate_ShouldCorrectlySetReturnValueOnInvocationAfterExecution()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.DelegateReturningMethod());

        Delegate returnValue = () => {};
        setup.SetReturnComputedValueBehaviour(returnValue);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.DelegateReturningMethod));
        setup.Execute(invocation);

        invocation.ReturnValue.ShouldBeSameAs(returnValue);
    }

    [Theory]
    [AutoData]
    public void SetReturnComputedValueBehaviour_WithArguments_ShouldCorrectlySetReturnValueOnInvocationAfterExecution(
        int iValue, string sValue, double dValue, List<bool> bValues, int returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        setup.SetReturnComputedValueBehaviour((int _, string _, double _, List<bool> _) => returnValue);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicNonVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.Execute(invocation);

        invocation.ReturnValue.ShouldBe(returnValue);
    }

    [Theory]
    [AutoData]
    public void SetReturnComputedValueBehaviour_WithIncorrectDelegateParameterCount_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues, string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var ex = Should.Throw<MimicException>(() => setup.SetReturnComputedValueBehaviour((int _, string _) => returnValue));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with 4 expected parameter(s) cannot invoke a callback method with 2 parameter(s)");
    }

    [Theory]
    [AutoData]
    public void SetReturnComputedValueBehaviour_WithIncorrectDelegateReturnType_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues, byte returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var ex = Should.Throw<MimicException>(() => setup.SetReturnComputedValueBehaviour((int _, string _, double _, List<bool> _) => returnValue));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with return type 'int' cannot invoke a callback method with return type 'byte'");
    }

    #endregion

    #region SetThrowExceptionBehaviour

    [Fact]
    public void SetThrowExceptionBehaviour_WhenExceptionIsNull_ShouldThrowAssertionException()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetThrowExceptionBehaviour(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("exception must not be null");
    }

    [Fact]
    public void SetThrowExceptionBehaviour_WhenReturnValueIsAlreadySet_ShouldThrowAssertionException()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());
        setup.SetReturnValueBehaviour(null);

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetThrowExceptionBehaviour(new Exception()));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("_returnOrThrow is null");
    }

    [Theory]
    [AutoData]
    public void SetThrowExceptionBehaviour_ShouldThrowExceptionOnExecution(string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var exception = new Exception(message);
        setup.SetThrowExceptionBehaviour(exception);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        var ex = Should.Throw<Exception>(() => setup.Execute(invocation));
        ex.ShouldBeSameAs(exception);
    }

    #endregion

    #region SetThrowComputedExceptionBehaviour

    [Fact]
    public void SetThrowComputedExceptionBehaviour_WhenDelegateIsNull_ShouldThrowAssertionException()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetThrowComputedExceptionBehaviour(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("exceptionFactory must not be null");
    }

    [Theory]
    [AutoData]
    public void SetThrowComputedExceptionBehaviour_WhenReturnValueIsAlreadySet_ShouldThrowAssertionException(string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());
        setup.SetReturnValueBehaviour(null);

        var exception = new Exception(message);

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetThrowComputedExceptionBehaviour(() => exception));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("_returnOrThrow is null");
    }

    [Theory]
    [AutoData]
    public void SetThrowComputedExceptionBehaviour_WithIncorrectDelegateParameterCount_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues, string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var exception = new Exception(message);
        var ex = Should.Throw<MimicException>(() => setup.SetThrowComputedExceptionBehaviour((int _, string _, double _) => exception));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with 4 expected parameter(s) cannot invoke a callback method with 3 parameter(s)");
    }

    [Theory]
    [AutoData]
    public void SetThrowComputedExceptionBehaviour_WithIncorrectDelegateReturnType_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues, string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var ex = Should.Throw<MimicException>(() => setup.SetThrowComputedExceptionBehaviour((int _, string _, double _, List<bool> _) => message));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with return type 'Exception' cannot invoke a callback method with return type 'string'");
    }

    [Theory]
    [AutoData]
    public void SetThrowComputedExceptionBehaviour_WithNoArguments_ShouldThrowExceptionOnExecution(string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var exception = new Exception(message);
        setup.SetThrowComputedExceptionBehaviour(() => exception);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));

        var ex = Should.Throw<Exception>(() => setup.Execute(invocation));
        ex.ShouldBeSameAs(exception);
    }

    [Theory]
    [AutoData]
    public void SetThrowComputedExceptionBehaviour_WithArguments_ShouldThrowExceptionOnExecution(
        int iValue, string sValue, double dValue, List<bool> bValues, string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var exception = new Exception(message);
        setup.SetThrowComputedExceptionBehaviour((int _, string _, double _, List<bool> _) => exception);

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicNonVoidMethod), [iValue, sValue, dValue, bValues]);

        var ex = Should.Throw<Exception>(() => setup.Execute(invocation));
        ex.ShouldBeSameAs(exception);
    }

    #endregion

    #region SetCallbackBehaviour

    [Fact]
    public void SetCallbackBehaviour_WhenDelegateIsNull_ShouldThrowAssertionException()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetCallbackBehaviour(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("callbackFunction must not be null");
    }

    [Theory]
    [AutoData]
    public void SetCallbackBehaviour_WithIncorrectDelegateParameterCount_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var ex = Should.Throw<MimicException>(() => setup.SetCallbackBehaviour((int _) => {}));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with 4 expected parameter(s) cannot invoke a callback method with 1 parameter(s)");
    }

    [Theory]
    [AutoData]
    public void SetCallbackBehaviour_WithIncorrectDelegateArgumentType_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var ex = Should.Throw<MimicException>(() => setup.SetCallbackBehaviour((int _, /* should be string*/ byte _, double  _, List<bool> _) => {}));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with parameter(s) (int, string, double, List<bool>) cannot invoke a callback method with the wrong parameter type(s) (int, byte, double, List<bool>)");
    }

    [Theory]
    [AutoData]
    public void SetCallbackBehaviour_WithIncorrectDelegateReturnType_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues, int returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var ex = Should.Throw<MimicException>(() => setup.SetCallbackBehaviour((int _, string _, double _, List<bool> _) => returnValue));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method cannot invoke a callback method with a non-void return type");
    }

    #endregion

    #region SetExecutionLimitBehaviour

    [Fact]
    public void SetExecutionLimitBehaviour_WithExecutionLimitLessThanOne_ShouldThrowAssertionException()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetExecutionLimitBehaviour(0));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("executionLimit >= 1");
    }

    [Fact]
    public void SetExecutionLimitBehaviour_WhenExecutionLimitIsAlreadySet_ShouldThrowAssertionException()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());
        setup.SetExecutionLimitBehaviour(1);

        var ex = Should.Throw<Guard.AssertionException>(() => setup.SetExecutionLimitBehaviour(1));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("_executionLimit is null");
    }

    #endregion

    #endregion

    #region AddBehaviours

    #region AddReturnValueBehaviour

    [Theory]
    [AutoData]
    public void AddReturnValueBehaviour_WhenMethodIsVoidReturnType_ShouldThrowAssertionException(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessVoidMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.AddReturnValueBehaviour(returnValue));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("MethodInfo.ReturnType != typeof(void)");
    }

    [Theory]
    [AutoData]
    public void AddReturnValueBehaviour_WhenReturnValueIsAlreadySetAndIsNotSequenceBehaviour_ShouldThrowAssertionException(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());
        setup.SetReturnValueBehaviour(returnValue);

        var ex = Should.Throw<Guard.AssertionException>(() => setup.AddReturnValueBehaviour(returnValue));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("_returnOrThrow is null or SequenceBehaviour");
    }

    [Theory]
    [AutoData]
    public void AddReturnValueBehaviour_ShouldCorrectlySetReturnValuesOnInvocationsAfterExecution(string returnValue, string secondReturnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        setup.AddReturnValueBehaviour(returnValue);
        setup.AddReturnValueBehaviour(secondReturnValue);

        var firstInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        setup.Execute(firstInvocation);
        firstInvocation.ReturnValue.ShouldBe(returnValue);

        var secondInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        setup.Execute(secondInvocation);
        secondInvocation.ReturnValue.ShouldBe(secondReturnValue);
    }

    #endregion

    #region AddReturnComputedValueBehaviour

    [Theory]
    [AutoData]
    public void AddReturnComputedValueBehaviour_WhenMethodIsVoidReturnType_ShouldThrowAssertionException(string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessVoidMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.AddReturnComputedValueBehaviour(() => returnValue));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("MethodInfo.ReturnType != typeof(void)");
    }

    [Theory]
    [AutoData]
    public void AddReturnComputedValueBehaviour_WithIncorrectDelegateParameterCount_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues, string returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var ex = Should.Throw<MimicException>(() => setup.AddReturnComputedValueBehaviour((int _, string _) => returnValue));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with 4 expected parameter(s) cannot invoke a callback method with 2 parameter(s)");
    }

    [Theory]
    [AutoData]
    public void AddReturnComputedValueBehaviour_WithIncorrectDelegateReturnType_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues, byte returnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var ex = Should.Throw<MimicException>(() => setup.AddReturnComputedValueBehaviour((int _, string _, double _, List<bool> _) => returnValue));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with return type 'int' cannot invoke a callback method with return type 'byte'");
    }

    [Theory]
    [AutoData]
    public void AddReturnComputedValueBehaviour_ShouldCorrectlySetReturnValueOnInvocationAfterExecution(
        int iValue, string sValue, double dValue, List<bool> bValues, int firstReturnvalue, int secondReturnValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        setup.AddReturnComputedValueBehaviour(() => firstReturnvalue);
        setup.AddReturnComputedValueBehaviour((int _, string _, double _, List<bool> _) => secondReturnValue);

        var firstInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicNonVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.Execute(firstInvocation);
        firstInvocation.ReturnValue.ShouldBe(firstReturnvalue);

        var secondInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicNonVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.Execute(secondInvocation);
        secondInvocation.ReturnValue.ShouldBe(secondReturnValue);
    }

    #endregion

    #region AddThrowExceptionBehaviour

    [Fact]
    public void AddThrowExceptionBehaviour_WhenExceptionIsNull_ShouldThrowAssertionException()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.AddThrowExceptionBehaviour(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("exception must not be null");
    }

    [Theory]
    [AutoData]
    public void AddThrowExceptionBehaviour_WhenReturnValueIsAlreadySetAndIsNotSequenceBehaviour_ShouldThrowAssertionException(string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());
        setup.SetReturnValueBehaviour(message);

        var ex = Should.Throw<Guard.AssertionException>(() => setup.AddThrowExceptionBehaviour(new Exception(message)));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("_returnOrThrow is null or SequenceBehaviour");
    }

    [Theory]
    [AutoData]
    public void AddThrowExceptionBehaviour_ShouldCorrectlyThrowExceptionsAfterExecution(string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var firstException = new Exception(message);
        setup.AddThrowExceptionBehaviour(firstException);

        var secondException = new Exception(message);
        setup.AddThrowExceptionBehaviour(secondException);

        var firstInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        Should.Throw<Exception>(() => setup.Execute(firstInvocation)).ShouldBeSameAs(firstException);

        var secondInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessMethod));
        Should.Throw<Exception>(() => setup.Execute(secondInvocation)).ShouldBeSameAs(secondException);
    }

    #endregion

    #region AddThrowComputedExceptionBehaviour

    [Fact]
    public void AddThrowComputedExceptionBehaviour_WhenDelegateIsNull_ShouldThrowAssertionException()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.AddThrowComputedExceptionBehaviour(null!));
        ex.ShouldNotBeNull();
        ex.Message.ShouldContain("exceptionFactory must not be null");
    }

    [Theory]
    [AutoData]
    public void AddThrowComputedExceptionBehaviour_WhenReturnValueIsAlreadySetAndIsNotSequenceBehaviour_ShouldThrowAssertionException(string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());
        setup.SetReturnValueBehaviour(message);

        var ex = Should.Throw<Guard.AssertionException>(() => setup.AddThrowComputedExceptionBehaviour(() => new Exception(message)));
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("_returnOrThrow is null or SequenceBehaviour");
    }

    [Theory]
    [AutoData]
    public void AddThrowComputedExceptionBehaviour_WithIncorrectDelegateParameterCount_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues, string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var exception = new Exception(message);
        var ex = Should.Throw<MimicException>(() => setup.AddThrowComputedExceptionBehaviour((int _, string _, double _) => exception));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with 4 expected parameter(s) cannot invoke a callback method with 3 parameter(s)");
    }

    [Theory]
    [AutoData]
    public void AddThrowComputedExceptionBehaviour_WithIncorrectDelegateReturnType_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues, string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var ex = Should.Throw<MimicException>(() => setup.AddThrowComputedExceptionBehaviour((int _, string _, double _, List<bool> _) => message));
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe("Setup on method with return type 'Exception' cannot invoke a callback method with return type 'string'");
    }

    [Theory]
    [AutoData]
    public void AddThrowComputedExceptionBehaviour_ShouldThrowExceptionsOnExecution(
        int iValue, string sValue, double dValue, List<bool> bValues, string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicNonVoidMethod(iValue, sValue, dValue, bValues));

        var firstException = new Exception(message);
        setup.AddThrowComputedExceptionBehaviour(() => firstException);

        var secondException = new Exception(message);
        setup.AddThrowComputedExceptionBehaviour((int _, string _, double _, List<bool> _) => secondException);

        var firstInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicNonVoidMethod), [iValue, sValue, dValue, bValues]);
        Should.Throw<Exception>(() => setup.Execute(firstInvocation)).ShouldBeSameAs(firstException);

        var secondInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicNonVoidMethod), [iValue, sValue, dValue, bValues]);
        Should.Throw<Exception>(() => setup.Execute(secondInvocation)).ShouldBeSameAs(secondException);
    }

    #endregion

    #region AddNoOpBehaviour

    [Fact]
    public void AddNoOpBehaviour_WhenMethodIsVoidReturnType_ShouldThrowAssertionException()
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessMethod());

        var ex = Should.Throw<Guard.AssertionException>(() => setup.AddNoOpBehaviour());
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("MethodInfo.ReturnType == typeof(void)");
    }

    [Theory]
    [AutoData]
    public void AddNoOpBehaviour_WhenReturnValueIsAlreadySetAndIsNotSequenceBehaviour_ShouldThrowAssertionException(string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessVoidMethod());
        setup.SetThrowExceptionBehaviour(new Exception(message));

        var ex = Should.Throw<Guard.AssertionException>(() => setup.AddNoOpBehaviour());
        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("_returnOrThrow is null or SequenceBehaviour");
    }

    [Theory]
    [AutoData]
    public void AddNoOpBehaviour_ShouldCorrectlyPerformNoOpOnExecution(string message)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.ParameterlessVoidMethod());

        setup.AddNoOpBehaviour();
        setup.AddThrowExceptionBehaviour(new Exception(message));
        setup.AddNoOpBehaviour();

        var firstInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessVoidMethod));
        Should.NotThrow(() => setup.Execute(firstInvocation));

        var secondInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessVoidMethod));
        Should.Throw<Exception>(() => setup.Execute(secondInvocation)).Message.ShouldBe(message);

        var thirdInvocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.ParameterlessVoidMethod));
        Should.NotThrow(() => setup.Execute(thirdInvocation));
    }

    #endregion

    #endregion

    #region SetupBase

    [Theory]
    [AutoData]
    public void Override_ShouldSetOverriddenToTrue(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicVoidMethod(iValue, sValue, dValue, bValues));

        setup.Override();

        setup.Overridden.ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void FlagAsExpected_ShouldSetExpectedToTrue(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicVoidMethod(iValue, sValue, dValue, bValues));

        setup.FlagAsExpected();

        setup.Expected.ShouldBeTrue();
    }

    [Theory]
    [AutoData]
    public void VerifyMatched_WhenNotMatched_ShouldThrow(
        int iValue, string sValue, double dValue)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicVoidMethod(iValue, sValue, dValue, Arg.Any<List<bool>>()));

        var ex = Should.Throw<MimicException>(() => setup.VerifyMatched());
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe($"""Setup 'MethodCallSetupTests.ISubject m => m.BasicVoidMethod({iValue}, "{sValue}", {dValue}, Any())' which was marked as expected has not been matched""");
    }

    [Theory]
    [AutoData]
    public void VerifyMatched_WhenMatched_ShouldNotThrow(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicVoidMethod(iValue, sValue, dValue, bValues));

        var invocation = InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]);
        setup.Execute(invocation);

        Should.NotThrow(() => setup.VerifyMatched());
    }

    [Theory]
    [AutoData]
    public void VerifyMatched_WhenMatched_ButSequenceBehaviourIsNotExhausted_ShouldThrow(
        int iValue, string sValue, double dValue, List<bool> bValues)
    {
        var (setup, _, _, _) = ConstructMethodCallSetup(m => m.BasicVoidMethod(iValue, sValue, dValue, Arg.Any<List<bool>>()));

        setup.AddNoOpBehaviour();
        setup.AddNoOpBehaviour();

        setup.Execute(InvocationFixture.ForMethod<ISubject>(nameof(ISubject.BasicVoidMethod), [iValue, sValue, dValue, bValues]));

        var ex = Should.Throw<MimicException>(() => setup.VerifyMatched());

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe($"""Setup 'MethodCallSetupTests.ISubject m => m.BasicVoidMethod({iValue}, "{sValue}", {dValue}, Any())' with sequence which was marked as expected has not been matched (1 seqeuence result has not been used).""");
    }

    #endregion

    private static (MethodCallSetup Setup, Mimic<ISubject> Mimic, MethodCallExpression OriginalExpression, MethodExpectation Expectation) ConstructMethodCallSetup(
        Expression<Action<ISubject>> expression, Func<bool>? condition = null, bool strict = true)
    {
        var mimic = new Mimic<ISubject> { Strict = strict };
        var methodCallExpression = (MethodCallExpression)expression.Body;
        var methodExpectation = new MethodExpectation(expression, methodCallExpression.Method, methodCallExpression.Arguments);
        var setup = new MethodCallSetup(methodCallExpression, mimic, methodExpectation, condition);

        return (setup, mimic, methodCallExpression, methodExpectation);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface ISubject
    {
        public void BasicVoidMethod(int iValue, string sValue, double dValue, List<bool> bValues);

        public int BasicNonVoidMethod(int iValue, string sValue, double dValue, List<bool> bValues);

        public void OutParametersMethod(decimal value, ref bool refValue, out int outValue);

        public string ParameterlessMethod();

        public void ParameterlessVoidMethod();

        public Delegate DelegateReturningMethod();
    }
}
