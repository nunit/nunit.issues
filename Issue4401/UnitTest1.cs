namespace Issue4401;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        double actual = 42.0;
        double expected = 13.0;
        Assert.GreaterOrEqual(actual,expected);
        Assert.AreEqual(expected,actual,0.1);

        decimal a = 5.0M;
        decimal b = 6M;
        Assert.AreEqual(a,b);

    }
}