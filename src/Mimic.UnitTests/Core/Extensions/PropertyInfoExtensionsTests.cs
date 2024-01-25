using Mimic.Core.Extensions;

namespace Mimic.UnitTests.Core.Extensions;

public class PropertyInfoExtensionsTests
{
    [Fact]
    public void CanReadProperty_WhenGetterIsAccessible_ShouldReturnsTrueAndCorrectOutParameters()
    {
        var property = typeof(ISubject).GetProperty(nameof(ISubject.Getter))!;

        bool result = property.CanReadProperty(out var getter, out var getterProperty);

        result.ShouldBeTrue();
        getter.ShouldNotBeNull();
        getter.ShouldBeSameAs(property.GetGetMethod(true));
        getterProperty.ShouldNotBeNull();
        getterProperty.ShouldBeSameAs(property);
    }

    [Fact]
    public void CanReadProperty_WhenGetterIsInaccessible_ShouldReturnsTrueAndCorrectOutParameters()
    {
        var property = typeof(ISubject).GetProperty(nameof(ISubject.Setter))!;

        bool result = property.CanReadProperty(out var getter, out var getterProperty);

        result.ShouldBeFalse();
        getter.ShouldBeNull();
        getterProperty.ShouldBeNull();
    }

    [Fact]
    public void CanReadProperty_WhenGetterIsAccessibleInDeclaringType_ShouldReturnsTrueAndCorrectOutParameters()
    {
        var property = typeof(SubjectWithOnlySetter).GetProperty(nameof(SubjectWithOnlySetter.VirtualProperty))!;

        bool result = property.CanReadProperty(out var getter, out var getterProperty);

        result.ShouldBeTrue();
        getter.ShouldNotBeNull();
        getter.ShouldBeSameAs(typeof(Subject).GetProperty(nameof(Subject.VirtualProperty))!.GetGetMethod(true));
        getterProperty.ShouldNotBeNull();
        getterProperty.ShouldBeSameAs(typeof(Subject).GetProperty(nameof(Subject.VirtualProperty)));
    }

    [Fact]
    public void CanWriteProperty_WhenSetterIsAccessible_ShouldReturnsTrueAndCorrectOutParameters()
    {
        var property = typeof(ISubject).GetProperty(nameof(ISubject.Setter))!;

        bool result = property.CanWriteProperty(out var getter, out var getterProperty);

        result.ShouldBeTrue();
        getter.ShouldNotBeNull();
        getter.ShouldBeSameAs(property.GetSetMethod(true));
        getterProperty.ShouldNotBeNull();
        getterProperty.ShouldBeSameAs(property);
    }

    [Fact]
    public void CanWriteProperty_WhenSetterIsInaccessible_ShouldReturnsTrueAndCorrectOutParameters()
    {
        var property = typeof(ISubject).GetProperty(nameof(ISubject.Getter))!;

        bool result = property.CanWriteProperty(out var getter, out var getterProperty);

        result.ShouldBeFalse();
        getter.ShouldBeNull();
        getterProperty.ShouldBeNull();
    }

    [Fact]
    public void CanWriteProperty_WhenSetterIsAccessibleInDeclaringType_ShouldReturnsTrueAndCorrectOutParameters()
    {
        var property = typeof(SubjectWithOnlyGetter).GetProperty(nameof(SubjectWithOnlyGetter.VirtualProperty))!;

        bool result = property.CanWriteProperty(out var getter, out var getterProperty);

        result.ShouldBeTrue();
        getter.ShouldNotBeNull();
        getter.ShouldBeSameAs(typeof(Subject).GetProperty(nameof(Subject.VirtualProperty))!.GetSetMethod(true));
        getterProperty.ShouldNotBeNull();
        getterProperty.ShouldBeSameAs(typeof(Subject).GetProperty(nameof(Subject.VirtualProperty)));
    }
}

file interface ISubject
{
    public object Getter { get; }

    public object Setter { set; }
}

file class Subject
{
    public virtual object? VirtualProperty { get; set; }
}

file class SubjectWithOnlyGetter : Subject
{
    public override object? VirtualProperty { get => base.VirtualProperty; }
}

file class SubjectWithOnlySetter : Subject
{
    public override object? VirtualProperty { set => base.VirtualProperty = value; }
}
