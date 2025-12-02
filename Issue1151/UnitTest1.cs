namespace Issue1151;

public class Tests
{
    /// <summary>
    /// Repro for Issue 1151 : Include differences in output for Is.EquivalentTo
    /// https://github.com/nunit/nunit/issues/1151
    /// </summary>
    [Test]
    public void EquivalenceInformationTest()
    {
        var expected = Enumerable.Range('a', 12).Select(n => (char)n).ToList();
        var actual = expected.Take(11);

        var ex = Assert.Throws<AssertionException>(()=>Assert.That(actual, Is.EquivalentTo(expected)));
        Assert.That(ex.Message, Does.Contain("Missing (1): < 'l' >"));
    }
}
