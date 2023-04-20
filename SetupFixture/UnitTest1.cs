namespace SetupFixture;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestNSSetupFixture()
    {
        Console.WriteLine("TestNSSetupFixture");
        Assert.Pass();
    }
}