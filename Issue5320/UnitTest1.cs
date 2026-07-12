namespace Issue5320;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(1, 2, 3, TestName = "{m}(Scenario A)")]
    [TestCase(4, 5, 6, TestName = "{m}(Scenario B)")]
    public void Test1(int a, int b, int c)
    {
        Assert.Pass();
    }
}
