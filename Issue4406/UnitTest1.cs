namespace Issue4406;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SingleTest()
    {
        int x = 42;
        Assert.That(x,Is.EqualTo(2));
    }


    [Test]
    public void Test1()
    {
        int x = 42;
        int y = 5;
        Assert.That(x,Is.GreaterThan(2));
        Assert.Multiple(() =>
        {
            Assert.That(x,Is.EqualTo(2));
            Assert.That(x,Is.EqualTo(3));
            Assert.That(x, Is.EqualTo(42));
            Assert.That(x,Is.EqualTo(4));
            Assert.That(y, Is.EqualTo(55));
        });
    }
}