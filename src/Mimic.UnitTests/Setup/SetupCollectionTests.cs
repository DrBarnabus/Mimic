using System.Collections;
using Mimic.Setup;

namespace Mimic.UnitTests.Setup;

public class SetupCollectionTests
{
    [Fact]
    public void Constructor_ShouldSuccessfullyConstruct()
    {
        var setupCollection = new SetupCollection();

        setupCollection.Count.ShouldBe(0);

        // ReSharper disable once GenericEnumeratorNotDisposed
        var enumerator = (setupCollection as IEnumerable).GetEnumerator();
        enumerator.ShouldNotBeNull();
    }

    [Fact]
    public void Add_WhenNoExistingMatchingExpectation_ShouldAddSetupToCollection()
    {
        var setupCollection = new SetupCollection();
        var (setup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(default, default!, default, default!));

        setupCollection.Add(setup);

        setupCollection.Count.ShouldBe(1);
        setupCollection[0].ShouldBeSameAs(setup);
    }

    [Fact]
    public void Add_WhenExistingMatchingExpectation_ShouldAddSetupToCollectionAndMarkExistingAsOverridden()
    {
        var setupCollection = new SetupCollection();

        var (originalSetup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(default, default!, default, default!));

        var (newSetup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(default, default!, default, default!));

        setupCollection.Add(originalSetup);
        setupCollection.Add(newSetup);

        setupCollection.Count.ShouldBe(2);
        setupCollection[0].ShouldBeSameAs(originalSetup);
        setupCollection[1].ShouldBeSameAs(newSetup);

        originalSetup.Overridden.ShouldBeTrue();
        newSetup.Overridden.ShouldBeFalse();
    }

    [Fact]
    public void Add_WhenExistingMatchingExpectations_ShouldAddSetupToCollectionAndMarkAllExistingAsOverridden()
    {
        var setupCollection = new SetupCollection();

        var (originalSetup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(default, default!, default, default!));

        var (intermediateSetup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(default, default!, default, default!));

        var (newSetup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(default, default!, default, default!));

        setupCollection.Add(originalSetup);
        setupCollection.Add(intermediateSetup);
        setupCollection.Add(newSetup);

        setupCollection.Count.ShouldBe(3);
        setupCollection[0].ShouldBeSameAs(originalSetup);
        setupCollection[1].ShouldBeSameAs(intermediateSetup);
        setupCollection[2].ShouldBeSameAs(newSetup);

        originalSetup.Overridden.ShouldBeTrue();
        intermediateSetup.Overridden.ShouldBeTrue();
        newSetup.Overridden.ShouldBeFalse();
    }

    [Fact]
    public void FindAll_WhenNoSetups_ShouldReturnAnEmptyList()
    {
        var setupCollection = new SetupCollection();

        var results = setupCollection.FindAll(_ => true);
        results.ShouldNotBeNull();
        results.ShouldBeEmpty();
    }

    [Fact]
    public void FindAll_WhenContainsSetups_ShouldReturnAllMatchingNonOverridenSetups()
    {
        var (overridenSetup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(default, default!, default, default!));

        var (matchingSetup, _, _, expectation) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicVoidMethod(default, default!, default, default!));

        var (nonMatchingSetup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicNonVoidMethod(default, default!, default, default!));

        SetupCollection setupCollection = [overridenSetup, matchingSetup, nonMatchingSetup];

        var results = setupCollection.FindAll(s => s.Expectation.Equals(expectation));
        results.ShouldNotBeNull();
        results.ShouldNotBeEmpty();
        results.Count.ShouldBe(1);
        results[0].ShouldBeSameAs(matchingSetup);
    }

    [Fact]
    public void FindLast_WhenNoSetups_ShouldReturnNull()
    {
        var setupCollection = new SetupCollection();

        setupCollection.FindLast(_ => true).ShouldBeNull();
    }

    [Fact]
    public void FindLast_WhenNoMatchingSetup_ShouldReturnNull()
    {
        var (nonMatchingSetup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicNonVoidMethod(default, default!, default, default!));

        SetupCollection setupCollection = [nonMatchingSetup];

        setupCollection.FindLast(_ => false).ShouldBeNull();
    }

    [Fact]
    public void FindLast_WhenMatchingSetup_ShouldReturnNull()
    {
        var (matchingSetup, _, _, expectation) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicNonVoidMethod(default, default!, default, default!));

        SetupCollection setupCollection = [matchingSetup];

        var result = setupCollection.FindLast(s => s.Expectation.Equals(expectation));
        result.ShouldNotBeNull();
        result.ShouldBeSameAs(matchingSetup);
    }

    [Fact]
    public void FindLast_WhenOverridenMatchingSetup_ShouldReturnNull()
    {
        var (overridenMatchingSetup, _, _, _) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicNonVoidMethod(default, default!, default, default!));

        var (matchingSetup, _, _, expectation) = MethodCallSetupTests.ConstructMethodCallSetup(m =>
            m.BasicNonVoidMethod(default, default!, default, default!));

        SetupCollection setupCollection = [overridenMatchingSetup, matchingSetup];

        var result = setupCollection.FindLast(s => s.Expectation.Equals(expectation));
        result.ShouldNotBeNull();
        result.ShouldBeSameAs(matchingSetup);
    }
}
