using NUnit.Framework;

return new NUnitLite.AutoRun(typeof(CustomTests).Assembly).Execute(args);

[TestFixture]
public class CustomTests
{
    [Test]
    public void Test1()
    {
        Assert.That(1, Is.EqualTo(1));
    }

    [TestCase(1, 2, 3)]
    [TestCase(2, 3, 5)]
    public void TestCaseInts(int a, int b, int expected) => Assert.That(a + b, Is.EqualTo(expected));

    // Needs conversion from string to DateTime/decimal at runtime
    [TestCase("2026-10-21", 1.5)]
    public void TestCaseConversion(DateTime date, decimal value)
    {
        Assert.That(date.Year, Is.EqualTo(2026));
        Assert.That(value, Is.EqualTo(1.5m));
    }

    [TestCase(1)]
    [TestCase("a")]
    public void GenericTestMethod<T>(T value) => Assert.That(value, Is.Not.Null);

    private static IEnumerable<int> Source() => [1, 2, 3];

    [TestCaseSource(nameof(Source))]
    public void TestCaseSourceTest(int value) => Assert.That(value, Is.Positive);

    [Test]
    public async Task AsyncTask()
    {
        await Task.Yield();
        Assert.That(true, Is.True);
    }

    [Test]
    public async ValueTask AsyncValueTask()
    {
        await Task.Yield();
        Assert.That(true, Is.True);
    }

    [Test]
    public void CollectionConstraints()
    {
        Assert.That(new[] { 1, 2, 3 }, Is.Unique);
        Assert.That(new List<string> { "a", "b" }, Is.EquivalentTo(new[] { "b", "a" }));
        Assert.That(new Dictionary<string, int> { ["a"] = 1 }, Does.ContainKey("a").WithValue(1));
    }

    [Test]
    public void TupleEquality()
    {
        Assert.That((1, "a"), Is.EqualTo((1, "a")));
        Assert.That(Tuple.Create(1, "a"), Is.EqualTo(Tuple.Create(1, "a")));
    }

    [Test]
    public void FailureMessageFormatting()
    {
        // Expected to fail: checks that the failure message formats a KeyValuePair
        Assert.That(new KeyValuePair<string, int>("a", 1), Is.EqualTo(new KeyValuePair<string, int>("a", 2)));
    }
}

[TestFixture(typeof(int))]
[TestFixture(typeof(string))]
public class GenericFixture<T>
{
    [Test]
    public void TypeIsKnown() => Assert.That(typeof(T), Is.Not.Null);
}
