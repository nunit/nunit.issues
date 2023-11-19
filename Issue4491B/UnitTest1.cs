namespace Issue4491B;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [Repeat(10)]
    public void Test0()
    {
        Thread.Sleep(100);
        TestContext.WriteLine($"Test1 repeatcount: {TestContext.CurrentContext.CurrentRepeatCount}");
        Assert.Pass();
    }


    [Test]
    [Repeat(6)]
    public void Test1()
    {
        if (TestContext.CurrentContext.CurrentRepeatCount == 3)
        {
            Assert.Fail();
        }
        TestContext.WriteLine($"Test1 repeatcount: {TestContext.CurrentContext.CurrentRepeatCount}");
        Assert.Pass();
    }
}