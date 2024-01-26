using System.Diagnostics.CodeAnalysis;

namespace Mimic.UnitTests;

public class CallCountTests
{
    [Theory]
    [InlineData(1, true, null)]
    [InlineData(int.MaxValue, true, null)]
    [InlineData(0, false, "Expected at least one invocation on the mimic, but it was never invoked: ")]
    public void AtLeastOnce_ShouldReturnCorrectResults(int count, bool expectedResult, string? expectedMessage)
    {
        var subject = CallCount.AtLeastOnce();

        subject.Validate(count).ShouldBe(expectedResult);
        subject.GetExceptionMessage(count).ShouldBe(expectedMessage);
    }

    [Theory]
    [InlineData(3, true, null)]
    [InlineData(int.MaxValue, true, null)]
    [InlineData(0, false, "Expected at least 3 invocation(s) on the mimic, but it was invoked 0 time(s): ")]
    [InlineData(2, false, "Expected at least 3 invocation(s) on the mimic, but it was invoked 2 time(s): ")]
    public void AtLeast_ShouldReturnCorrectResults(int count, bool expectedResult, string? expectedMessage)
    {
        var subject = CallCount.AtLeast(3);

        subject.Validate(count).ShouldBe(expectedResult);
        subject.GetExceptionMessage(count).ShouldBe(expectedMessage);
    }

    [Theory]
    [InlineData(0, true, null)]
    [InlineData(2, true, null)]
    [InlineData(3, true, null)]
    [InlineData(4, false, "Expected at most 3 invocation(s) on the mimic, but it was invoked 4 time(s): ")]
    public void AtMost_ShouldReturnCorrectResults(int count, bool expectedResult, string? expectedMessage)
    {
        var subject = CallCount.AtMost(3);

        subject.Validate(count).ShouldBe(expectedResult);
        subject.GetExceptionMessage(count).ShouldBe(expectedMessage);
    }

    [Theory]
    [InlineData(0, true, null)]
    [InlineData(1, true, null)]
    [InlineData(2, false, "Expected at most one invocation on the mimic, but it was invoked 2 times: ")]
    public void AtMostOnce_ShouldReturnCorrectResults(int count, bool expectedResult, string? expectedMessage)
    {
        var subject = CallCount.AtMostOnce();

        subject.Validate(count).ShouldBe(expectedResult);
        subject.GetExceptionMessage(count).ShouldBe(expectedMessage);
    }

    [Theory]
    [InlineData(3, true, null)]
    [InlineData(4, true, null)]
    [InlineData(5, true, null)]
    [InlineData(2, false, "Expected between 3 and 5 invocations (Inclusive) on the mimic, but it was invoked 2 times: ")]
    [InlineData(6, false, "Expected between 3 and 5 invocations (Inclusive) on the mimic, but it was invoked 6 times: ")]
    public void InclusiveBetween_ShouldReturnCorrectResults(int count, bool expectedResult, string? expectedMessage)
    {
        var subject = CallCount.InclusiveBetween(3, 5);

        subject.Validate(count).ShouldBe(expectedResult);
        subject.GetExceptionMessage(count).ShouldBe(expectedMessage);
    }

    [Theory]
    [InlineData(4, true, null)]
    [InlineData(3, false, "Expected between 3 and 5 invocations (Exclusive) on the mimic, but it was invoked 3 times: ")]
    [InlineData(5, false, "Expected between 3 and 5 invocations (Exclusive) on the mimic, but it was invoked 5 times: ")]
    public void ExclusiveBetween_ShouldReturnCorrectResults(int count, bool expectedResult, string? expectedMessage)
    {
        var subject = CallCount.ExclusiveBetween(3, 5);

        subject.Validate(count).ShouldBe(expectedResult);
        subject.GetExceptionMessage(count).ShouldBe(expectedMessage);
    }

    [Theory]
    [InlineData(3, true, null)]
    [InlineData(2, false, "Expected exactly 3 invocation(s) on the mimic, but it was invoked 2 time(s): ")]
    [InlineData(4, false, "Expected exactly 3 invocation(s) on the mimic, but it was invoked 4 time(s): ")]
    public void Exactly_ShouldReturnCorrectResults(int count, bool expectedResult, string? expectedMessage)
    {
        var subject = CallCount.Exactly(3);

        subject.Validate(count).ShouldBe(expectedResult);
        subject.GetExceptionMessage(count).ShouldBe(expectedMessage);
    }

    [Theory]
    [InlineData(1, true, null)]
    [InlineData(0, false, "Expected a single invocation on the mimic, but it was invoked 0 times: ")]
    [InlineData(2, false, "Expected a single invocation on the mimic, but it was invoked 2 times: ")]
    public void Once_ShouldReturnCorrectResults(int count, bool expectedResult, string? expectedMessage)
    {
        var subject = CallCount.Once();

        subject.Validate(count).ShouldBe(expectedResult);
        subject.GetExceptionMessage(count).ShouldBe(expectedMessage);
    }

    [Theory]
    [InlineData(0, true, null)]
    [InlineData(1, false, $"Expected zero invocations on the mimic, but it was invoked 1 time(s): ")]
    public void Never_ShouldReturnCorrectResults(int count, bool expectedResult, string? expectedMessage)
    {
        var subject = CallCount.Never();

        subject.Validate(count).ShouldBe(expectedResult);
        subject.GetExceptionMessage(count).ShouldBe(expectedMessage);
    }

    [Fact]
    [SuppressMessage("ReSharper", "EqualExpressionComparison", Justification = "Method is testing equality so this is expected")]
    public void Equals_WhenObjectsAreSame_ShouldReturnTrue()
    {
        (CallCount.Once() == CallCount.Once()).ShouldBeTrue();
        (CallCount.Once() == CallCount.Exactly(1)).ShouldBeTrue();

        CallCount.Once().Equals(CallCount.Once()).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WhenObjectsAreNotSame_ShouldReturnFalse()
    {
        (CallCount.Once() == CallCount.AtMostOnce()).ShouldBeFalse();
        (CallCount.Once() == CallCount.ExclusiveBetween(5, 9)).ShouldBeFalse();

        CallCount.Once().Equals(CallCount.Never()).ShouldBeFalse();
        CallCount.Once().Equals(new object()).ShouldBeFalse();
    }

    [Fact]
    public void NotEqual_WhenObjectsAreDifferent_ShouldReturnTrue()
    {
        (CallCount.Once() != CallCount.Never()).ShouldBeTrue();
        (CallCount.Once() != CallCount.AtLeast(1)).ShouldBeTrue();
    }

    [Fact]
    [SuppressMessage("ReSharper", "EqualExpressionComparison", Justification = "Method is testing equality so this is expected")]
    public void NotEqual_WhenObjectsAreSame_ShouldReturnFalse()
    {
        (CallCount.Once() != CallCount.Once()).ShouldBeFalse();
        (CallCount.InclusiveBetween(10, 30) != CallCount.InclusiveBetween(10, 30)).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameHashCodeForEquivelentObjects()
    {
        CallCount.Once().GetHashCode().ShouldBe(CallCount.Once().GetHashCode());
    }
}
