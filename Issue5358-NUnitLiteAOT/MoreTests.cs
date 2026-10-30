using System.Collections;
using NUnit.Framework;

public enum Color { Red, Green }

public record Point(int X, int Y);

public class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

[TestFixture]
public class AsyncLifecycleTests
{
    private int _oneTimeValue;
    private int _value;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await Task.Yield();
        _oneTimeValue = 42;
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown() => await Task.Yield();

    [SetUp]
    public async Task SetUp()
    {
        await Task.Yield();
        _value = 1;
    }

    [TearDown]
    public async ValueTask TearDown() => await Task.Yield();

    [Test]
    public void LifecycleRan()
    {
        Assert.That(_oneTimeValue, Is.EqualTo(42));
        Assert.That(_value, Is.EqualTo(1));
    }

    [TestCase(2, ExpectedResult = 4)]
    public async Task<int> AsyncTaskOfInt(int value)
    {
        await Task.Yield();
        return value * 2;
    }

    [TestCase(ExpectedResult = "a")]
    public async Task<string> AsyncTaskOfString()
    {
        await Task.Yield();
        return "a";
    }

    [TestCase(3, ExpectedResult = 9)]
    public int SyncExpectedResult(int value) => value * value;
}

[TestFixture]
public class ParameterTests
{
    [Test]
    public void ValuesInt([Values(1, 2)] int value) => Assert.That(value, Is.Positive);

    [Test]
    public void ValuesBool([Values] bool value) => Assert.That(value, Is.True.Or.False);

    [Test]
    public void ValuesEnum([Values] Color color) => Assert.That(Enum.IsDefined(color));

    [Test]
    public void RangeInt([Range(1, 3)] int value) => Assert.That(value, Is.InRange(1, 3));

    [Test]
    public void RangeDouble([Range(0.0, 1.0, 0.5)] double value) => Assert.That(value, Is.InRange(0.0, 1.0));

    [Test, Combinatorial]
    public void Combinatorial([Values(1, 2)] int a, [Values("x", "y")] string b) => Assert.That(b, Has.Length.EqualTo(1));

    [Test, Sequential]
    public void Sequential([Values(1, 2)] int a, [Values(2, 4)] int b) => Assert.That(b, Is.EqualTo(a * 2));

    private static readonly int[] Numbers = [1, 2];

    [Test]
    public void ValueSource([ValueSource(nameof(Numbers))] int value) => Assert.That(value, Is.Positive);

    private static IEnumerable<TestCaseData> DataSource()
    {
        yield return new TestCaseData(1, 2).Returns(3);
        yield return new TestCaseData(2, 2).Returns(4).SetName("TwoPlusTwo");
    }

    [TestCaseSource(nameof(DataSource))]
    public int TestCaseDataSource(int a, int b) => a + b;

    private static object[] ObjectArraySource = [new object[] { 1, "a" }, new object[] { 2, "b" }];

    [TestCaseSource(nameof(ObjectArraySource))]
    public void ObjectArrayCases(int i, string s) => Assert.That(s, Is.Not.Empty);

    [TestCaseSource(typeof(ExternalSource))]
    public void ClassSource(int value) => Assert.That(value, Is.Positive);

    [TestCaseSource(typeof(ExternalSource), nameof(ExternalSource.Named))]
    public void NamedClassSource(string value) => Assert.That(value, Is.Not.Empty);

    [TestCase(Color.Green)]
    public void TestCaseEnum(Color color) => Assert.That(color, Is.EqualTo(Color.Green));

    [TestCase(1, 2L, 3.0f)]
    public void TestCaseWidening(int a, long b, double c) => Assert.That(a + b, Is.EqualTo(c));

    [TestCase(null)]
    public void TestCaseNull(string? value) => Assert.That(value, Is.Null);

    [TestCase(new[] { 1, 2 })]
    public void TestCaseArray(int[] values) => Assert.That(values, Has.Length.EqualTo(2));

    [TestCase("a", "b", "c")]
    public void TestCaseParams(params string[] values) => Assert.That(values, Has.Length.EqualTo(3));
}

public class ExternalSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return 1;
        yield return 2;
    }

    public static IEnumerable<string> Named() => ["x"];
}

[TestFixture]
public class AssertionTests
{
    [Test]
    public void ThrowsSync() => Assert.Throws<InvalidOperationException>(() => throw new InvalidOperationException());

    [Test]
    public void ThrowsAsync() => Assert.ThrowsAsync<InvalidOperationException>(async () =>
    {
        await Task.Yield();
        throw new InvalidOperationException();
    });

    [Test]
    public void ThrowsConstraint() => Assert.That(() => throw new ArgumentException("bad", "p"),
        Throws.ArgumentException.With.Property(nameof(ArgumentException.ParamName)).EqualTo("p"));

    [Test]
    public void ThrowsNothingAsync() => Assert.That(async () => await Task.Delay(1), Throws.Nothing);

    [Test]
    public void PropertyOnBclType()
    {
        Assert.That("abc", Has.Property("Length").EqualTo(3));
        Assert.That(new List<int> { 1 }, Has.Property("Count").EqualTo(1));
    }

    [Test]
    public void PropertyOnUserType() => Assert.That(new Person { Name = "a" }, Has.Property(nameof(Person.Name)).EqualTo("a"));

    [Test]
    public void Ordered()
    {
        Assert.That(new[] { 1, 2, 3 }, Is.Ordered);
        Assert.That(new[] { "c", "b" }, Is.Ordered.Descending);
        Assert.That(new[] { new Person { Age = 1 }, new Person { Age = 2 } }, Is.Ordered.By(nameof(Person.Age)));
    }

    [Test]
    public void Tolerance()
    {
        Assert.That(1.001, Is.EqualTo(1.0).Within(0.01));
        Assert.That(DateTime.Today.AddSeconds(1), Is.EqualTo(DateTime.Today).Within(TimeSpan.FromSeconds(2)));
    }

    [Test]
    public void Records() => Assert.That(new Point(1, 2), Is.EqualTo(new Point(1, 2)));

    [Test]
    public void PropertiesComparer() =>
        Assert.That(new Person { Name = "a", Age = 1 }, Is.EqualTo(new Person { Name = "a", Age = 1 }).UsingPropertiesComparer());

    [Test]
    public void DictionaryEquality() =>
        Assert.That(new Dictionary<string, int> { ["a"] = 1 }, Is.EqualTo(new Dictionary<string, int> { ["a"] = 1 }));

    [Test]
    public void CollectionConstraintsWithoutUnique()
    {
        Assert.That(new[] { 1, 2, 3 }, Has.Member(2));
        Assert.That(new[] { 1, 2, 3 }, Is.SubsetOf(new[] { 1, 2, 3, 4 }));
        Assert.That(new[] { 1, 2, 3 }, Has.Exactly(1).EqualTo(2));
        Assert.That(new[] { 1, 2, 3 }, Is.All.Positive);
    }

    [Test]
    public void StringConstraints() => Assert.That("Hello", Does.StartWith("He").And.Contain("ll").IgnoreCase);

    [Test]
    public void MultipleScope()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(1, Is.EqualTo(1));
            Assert.That(2, Is.EqualTo(2));
        }
    }

    [Test]
    public void AssumeInconclusive()
    {
        Assume.That(false, "Expected to be inconclusive");
    }

    [Test]
    public void IgnoreAtRuntime() => Assert.Ignore("Expected to be ignored");

    [Test]
    public void WarningResult() => Assert.Warn("Expected warning");
}

[TestFixture]
public class AttributeTests
{
    private static int _retryCount;

    [Test, Retry(3)]
    public void RetrySucceedsSecondTime()
    {
        _retryCount++;
        Assert.That(_retryCount, Is.GreaterThan(1));
    }

    [Test, Repeat(3)]
    public void Repeat() => Assert.Pass();

    [Test, CancelAfter(5000)]
    public async Task CancelAfterToken(CancellationToken token)
    {
        await Task.Delay(1, token);
        Assert.That(token.CanBeCanceled, Is.True);
    }

    [Test, Category("Fast"), Description("Has metadata")]
    public void Metadata() => Assert.That(TestContext.CurrentContext.Test.Properties.Get("Category"), Is.EqualTo("Fast"));

    [Test]
    public void TestContextData() => Assert.That(TestContext.CurrentContext.Test.Name, Is.EqualTo(nameof(TestContextData)));

    [Test, Ignore("Expected to be ignored")]
    public void IgnoredByAttribute() { }

    [Test, Explicit]
    public void ExplicitTest() => Assert.Fail("Should not run");
}

[TestFixture, Parallelizable(ParallelScope.All)]
public class ParallelTests
{
    [Test]
    public async Task ParallelA() => await Task.Delay(10);

    [Test]
    public async Task ParallelB() => await Task.Delay(10);

    [TestCase(1)]
    [TestCase(2)]
    public void ParallelCases(int value) => Assert.That(value, Is.Positive);
}

[TestFixture]
public class TheoryTests
{
    [DatapointSource]
    public int[] Values = [1, 2, 3];

    [Theory]
    public void Positive(int value) => Assert.That(value, Is.Positive);
}

[TestFixture("a")]
[TestFixture("b")]
public class ParameterizedFixture
{
    private readonly string _value;

    public ParameterizedFixture(string value) => _value = value;

    [Test]
    public void HasValue() => Assert.That(_value, Is.Not.Empty);
}
