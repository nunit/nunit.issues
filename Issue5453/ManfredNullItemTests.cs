namespace Issue5453;

public class ManfredNullItemTests
{
    // Manfred's counter-example: with Predicate<string> (4.6.1) the lambda claims non-null,
    // yet NUnit passes the null element into it at runtime.
    [Test]
    public void NullElementReachesNonNullablePredicate()
    {
        List<string?> testList = ["Hello", null];
        Assert.That(testList, Has.One.Matches<string>(x => x is null));
    }

    [Test]
    public void NullElementDereferencedInNonNullablePredicate()
    {
        List<string?> testList = ["Hello", null];
        Assert.That(testList, Has.One.Matches<string>(x => x.Length == 5));
    }
}
