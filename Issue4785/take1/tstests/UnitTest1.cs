using tdlib;

namespace tstests;

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

    [TearDown]
    public void TearDown()
    {
        var u = new SomeUtilityClass();
        bool found = u.SomeUtilityMethod();
        Assert.That(found, Is.True);
    }
}