namespace Issue3441;

public class Tests
{
    [Test]
    public void IsEquivalentTransitiveWhenNestedInDictionary()
    {
        HashSet<string> actual1 = new HashSet<string>() { "B", "C" };
        HashSet<string> expected1 = new HashSet<string>() { "B", "C" };

        Assert.That(actual1, Is.EquivalentTo(expected1), "Test Case 1 Failed");

        Dictionary<string, HashSet<string>> actual2 = new Dictionary<string, HashSet<string>> { { "A", new HashSet<string>() { "B", "C" } } };
        Dictionary<string, HashSet<string>> expected2 = new Dictionary<string, HashSet<string>> { { "A", new HashSet<string>() { "B", "C" } } };

        Assert.That(actual2, Is.EquivalentTo(expected2), "Test Case 2 Failed");

        HashSet<string> actual3 = new HashSet<string>() { "B", "C" };
        HashSet<string> expected3 = new HashSet<string>() { "C", "B" };

        Assert.That(actual3, Is.EquivalentTo(expected3), "Test Case 3 Failed");

        Dictionary<string, HashSet<string>> actual4 = new Dictionary<string, HashSet<string>> { { "A", new HashSet<string>() { "B", "C" } } };
        Dictionary<string, HashSet<string>> expected4 = new Dictionary<string, HashSet<string>> { { "A", new HashSet<string>() { "C", "B" } } };

        Assert.That(actual4, Is.EquivalentTo(expected4), "Test Case 4 Failed");
    }
}
