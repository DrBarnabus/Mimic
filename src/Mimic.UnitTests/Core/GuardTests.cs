using Mimic.Core;

namespace Mimic.UnitTests.Core;

// ReSharper disable ConvertToConstant.Local
public class GuardTests
{
    #region Assert

    [Fact]
    public void AssertWithInterpolatedMessage_ConditionIsTrue_ShouldNotThrowAnException()
    {
        string testValue = "Example";

        Should.NotThrow(() => Guard.Assert(testValue is "Example", $"TestValue was {testValue}"));
    }

    [Fact]
    public void AssertWithInterpolatedMessage_ConditionIsFalse_ShouldThrowGuardAssertionException()
    {
        string testValue = "Example";

        var ex = Should.Throw<Guard.AssertionException>(() =>
            Guard.Assert(testValue is not "Example", $"TestValue was {testValue}"));

        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("testValue is not \"Example\"");
        ex.Message.ShouldBe("TestValue was Example (Expression 'testValue is not \"Example\"')");
    }

    [Fact]
    public void AssertWithMessage_ConditionIsTrue_ShouldNotThrowAnException()
    {
        string testValue = "Example";

        Should.NotThrow(() => Guard.Assert(testValue is "Example", "TestValue was not 'Example'"));
    }

    [Fact]
    public void AssertWithMessage_ConditionIsFalse_ShouldThrowGuardAssertionException()
    {
        string testValue = "Example";

        var ex = Should.Throw<Guard.AssertionException>(() =>
            Guard.Assert(testValue is not "Example", "TestValue was not 'Example'"));

        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("testValue is not \"Example\"");
        ex.Message.ShouldBe("TestValue was not 'Example' (Expression 'testValue is not \"Example\"')");
    }

    [Fact]
    public void AssertWithDefaultMessage_ConditionIsTrue_ShouldNotThrowAnException()
    {
        string testValue = "Example";

        Should.NotThrow(() => Guard.Assert(testValue is "Example"));
    }

    [Fact]
    public void AssertWithDefaultMessage_ConditionIsFalse_ShouldThrowGuardAssertionException()
    {
        string testValue = "Example";

        var ex = Should.Throw<Guard.AssertionException>(() =>
            Guard.Assert(testValue is not "Example"));

        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("testValue is not \"Example\"");
        ex.Message.ShouldBe("Assertion failed (Expression 'testValue is not \"Example\"')");
    }

    #endregion

    #region NotNull

    [Fact]
    public void NotNullWithInterpolatedMessage_ValueIsNotNull_ShouldNotThrowAnException()
    {
        string testValue = "Example";

        Should.NotThrow(() => Guard.NotNull(testValue, $"TestValue was {testValue}"));
    }

    [Fact]
    public void NotNullWithInterpolatedMessage_ValueIsNull_ShouldThrowGuardAssertionException()
    {
        string? testValue = null;

        var ex = Should.Throw<Guard.AssertionException>(() => Guard.NotNull(testValue, $"TestValue was {testValue ?? "null"}"));

        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("testValue");
        ex.Message.ShouldBe("TestValue was null (Expression 'testValue')");
    }

    [Fact]
    public void NotNullWithMessage_ValueIsNotNull_ShouldNotThrowAnException()
    {
        string testValue = "Example";

        Should.NotThrow(() => Guard.NotNull(testValue, "TestValue was null"));
    }

    [Fact]
    public void NotNullWithMessage_ValueIsNull_ShouldThrowGuardAssertionException()
    {
        string? testValue = null;

        var ex = Should.Throw<Guard.AssertionException>(() => Guard.NotNull(testValue, "TestValue was null"));

        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("testValue");
        ex.Message.ShouldBe("TestValue was null (Expression 'testValue')");
    }

    [Fact]
    public void NotNullWithDefaultMessage_ValueIsNotNull_ShouldNotThrowAnException()
    {
        string testValue = "Example";

        Should.NotThrow(() => Guard.NotNull(testValue));
    }

    [Fact]
    public void NotNullWithDefaultMessage_ValueIsNull_ShouldThrowGuardAssertionException()
    {
        string? testValue = null;

        var ex = Should.Throw<Guard.AssertionException>(() => Guard.NotNull(testValue));

        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("testValue");
        ex.Message.ShouldBe("testValue must not be null (Expression 'testValue')");
    }

    #endregion

    #region NotNullOrEmpty

    [Fact]
    public void NotNullOrEmptyWithInterpolatedMessage_ValueIsNotNullOrEmpty_ShouldNotThrowAnException()
    {
        string testValue = "Example";

        Should.NotThrow(() => Guard.NotNullOrEmpty(testValue, $"TestValue was {testValue}"));
    }

    [Theory]
    [InlineData(null, "null")]
    [InlineData("", "empty")]
    public void NotNullOrEmptyWithInterpolatedMessage_ValueIsNullOrEmpty_ShouldThrowGuardAssertionException(string? testValue, string expected)
    {
        var ex = Should.Throw<Guard.AssertionException>(() => Guard.NotNullOrEmpty(testValue, $"TestValue was {expected}"));

        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("testValue");
        ex.Message.ShouldBe($"TestValue was {expected} (Expression 'testValue')");
    }

    [Fact]
    public void NotNullOrEmptyWithMessage_ValueIsNotNullOrEmpty_ShouldNotThrowAnException()
    {
        string testValue = "Example";

        Should.NotThrow(() => Guard.NotNullOrEmpty(testValue, "TestValue was null or empty"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NotNullOrEmptyWithMessage_ValueIsNullOrEmpty_ShouldThrowGuardAssertionException(string? testValue)
    {
        var ex = Should.Throw<Guard.AssertionException>(() => Guard.NotNullOrEmpty(testValue, "TestValue was null or empty"));

        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("testValue");
        ex.Message.ShouldBe("TestValue was null or empty (Expression 'testValue')");
    }

    [Fact]
    public void NotNullOrEmptyWithDefaultMessage_ValueIsNotNullOrEmpty_ShouldNotThrowAnException()
    {
        string testValue = "Example";

        Should.NotThrow(() => Guard.NotNullOrEmpty(testValue));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NotNullOrEmptyWithDefaultMessage_ValueIsNullOrEmpty_ShouldThrowGuardAssertionException(string? testValue)
    {
        var ex = Should.Throw<Guard.AssertionException>(() => Guard.NotNullOrEmpty(testValue));

        ex.ShouldNotBeNull();
        ex.Expression.ShouldBe("testValue");
        ex.Message.ShouldBe("testValue must not be null or empty (Expression 'testValue')");
    }

    #endregion

    /// <summary>
    /// Tests <see cref="Guard.InterpolatedStringHandler{TValue, TCondition}"/> via
    /// <see cref="Guard.Assert(bool, ref Guard.InterpolatedStringHandler{bool, Guard.BooleanCondition}, string?)"/>
    /// </summary>
    public class InterpolatedStringHandlerTests
    {
        private class ToStringable
        {
            private int _callCount;

            public int CallCount => _callCount;

            public override string ToString()
            {
                Interlocked.Increment(ref _callCount);
                return string.Empty;
            }
        }

        [Fact]
        public void ConditionEvaluatesToTrue_ShouldNotAppend()
        {
            var toStringable = new ToStringable();

            Guard.Assert(true, $"This should not be appended {toStringable}");

            toStringable.CallCount.ShouldBe(0);
        }

        [Fact]
        public void ConditionEvaluatesToFalse_ShouldAppend()
        {
            var toStringable = new ToStringable();

            Should.Throw<Guard.AssertionException>(() => Guard.Assert(false, $"This should not be appended {toStringable}"));

            toStringable.CallCount.ShouldBe(1);
        }

        [Fact]
        public void Template_ShouldCorrectlyFormatString()
        {
            const string StringValue = "Example";
            const int IntValue = 15;

            var ex = Should.Throw<Guard.AssertionException>(() =>
                Guard.Assert(false, $"String Value is {StringValue} and Int Value is {IntValue}"));

            ex.ShouldNotBeNull();
            ex.Message.ShouldBe("String Value is Example and Int Value is 15 (Expression 'false')");
        }

        [Fact]
        public void TemplateWithFormat_ShouldCorrectlyFormatString()
        {
            const string StringValue = "Example";
            const int IntValue = 15;

            var ex = Should.Throw<Guard.AssertionException>(() =>
                Guard.Assert(false, $"String Value is {StringValue} and Int Value is {IntValue:x4}"));

            ex.ShouldNotBeNull();
            ex.Message.ShouldBe("String Value is Example and Int Value is 000f (Expression 'false')");
        }

        [Fact]
        public void TemplateWithAlignment_ShouldCorrectlyFormatString()
        {
            const string StringValue = "Example";
            const int IntValue = 15;

            var ex = Should.Throw<Guard.AssertionException>(() =>
                Guard.Assert(false, $"String Value is {StringValue,-8} and Int Value is {IntValue,-4}"));

            ex.ShouldNotBeNull();
            ex.Message.ShouldBe("String Value is Example  and Int Value is 15   (Expression 'false')");
        }

        [Fact]
        public void TemplateWithFormatAndAlignment_ShouldCorrectlyFormatString()
        {
            const string StringValue = "Example";
            const int IntValue = 15;

            var ex = Should.Throw<Guard.AssertionException>(() =>
                Guard.Assert(false, $"String Value is {StringValue,-8} and Int Value is {IntValue,-4:X2}"));

            ex.ShouldNotBeNull();
            ex.Message.ShouldBe("String Value is Example  and Int Value is 0F   (Expression 'false')");
        }

        [Fact]
        public void TemplateWithReadOnlySpanChar_ShouldCorrectlyFormatString()
        {
            var ex = Should.Throw<Guard.AssertionException>(() =>
            {
                ReadOnlySpan<char> stringValue = "Example";
                Guard.Assert(false, $"ReadOnlySpan<char> is {stringValue}");
            });

            ex.ShouldNotBeNull();
            ex.Message.ShouldBe("ReadOnlySpan<char> is Example (Expression 'false')");
        }

        [Fact]
        public void TemplateWithReadOnlySpanCharAndAlignment_ShouldCorrectlyFormatString()
        {
            var ex = Should.Throw<Guard.AssertionException>(() =>
            {
                ReadOnlySpan<char> stringValue = "Example";
                Guard.Assert(false, $"ReadOnlySpan<char> is {stringValue,12}");
            });

            ex.ShouldNotBeNull();
            ex.Message.ShouldBe("ReadOnlySpan<char> is      Example (Expression 'false')");
        }
    }
}
