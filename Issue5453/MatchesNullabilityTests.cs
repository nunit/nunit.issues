namespace Issue5453;

public class MatchesNullabilityTests
{
    // From the issue: compiles on 4.6.1, CS8602 (error with WarningsAsErrors=nullable) on 5.0.0,
    // because ConstraintExpression.Matches<T> changed from Predicate<T> to Predicate<T?> in #5218.
    [Test]
    public void HasOneMatches()
    {
        List<string> testList = ["hello", "world"];
        Assert.That(testList, Has.One.Matches<string>(word => word.Length == 5));
    }

    [Test]
    public void HasAllMatches()
    {
        List<string> testList = ["hello", "world"];
        Assert.That(testList, Has.All.Matches<string>(word => word.Length == 5));
    }
}
