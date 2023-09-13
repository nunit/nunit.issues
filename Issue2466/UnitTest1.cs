namespace NUnitIssue2466;

public class Tests
{
    [OneTimeSetUp]
    public void Setup()
    {
        SomeOtherMethod();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    public void SomeOtherMethod()
    {
        throw new Exception("SomeOtherMethod");
    }   
}