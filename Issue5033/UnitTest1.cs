namespace Issue5033;

public class Tests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void Test1()
    {
        int x = 1;
        Assert.That(x, Is.EqualTo(42));
    }

    [TearDown]
    public void TearDown()
    {
        var stacktrace = TestContext.CurrentContext.Result.StackTrace;
        Console.WriteLine(stacktrace);
    }
}
