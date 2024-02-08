
namespace Issue4613;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(42)]
    public void Test1(int x)
    {
        Assert.That(x, Is.EqualTo(42));
    }

    [TestCase(42)]
    public void Test2(int x)
    {
        Assert.AreEqual(42,x);
    }
}