namespace Issue4554;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}

[SetUpFixture]
public class Init
{
    [OneTimeSetUpAttribute]
    public void OneTimeSetUp() 
    { 

    }
}