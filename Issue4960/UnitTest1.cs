namespace Issue4960;

public class Tests
{
    [Test]
    public void Test1()
    {
        TestContext.Out.Write(new string('\n', 3));
        Assert.Pass("The Pass message to be shown");
    }

    [Test]
    public void Test2()
    {
        Assert.Fail("The Fail message to be shown");
    }

    [Test]
    public void Test3()
    {
        Assert.Warn("The Warn message to be shown");
    }
}
