namespace Issue4880;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }


    [TestCase(43,"abcd")]
    [TestCase(42,"abc")]
    public void Test1(int x,string s)
    {
        Assert.Pass();
    }
}
