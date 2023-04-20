namespace SetupFixture.Level2;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestNSLevel2()
    {
        Console.WriteLine("Level2");
        Assert.Pass();
    }
}


[SetUpFixture]
public class SetupFixture
{
    [OneTimeSetUp]
    public void Setup()
    {
        Console.WriteLine("SetupFixture at level 2");
    }
}