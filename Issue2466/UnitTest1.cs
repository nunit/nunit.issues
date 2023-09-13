namespace NUnitIssue2466;

public class Tests
{
    [OneTimeSetUp]
    public void Setup()
    {
        throw new Exception("OneTimeSetup");
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}