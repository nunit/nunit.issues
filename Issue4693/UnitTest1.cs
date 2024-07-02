using NUnit.Framework.Legacy;

namespace Issue4693;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        ClassicAssert.IsTrue(true);
    }
    [Test]
    public void Test2()
    {
        ClassicAssert.IsTrue(true);
    }
    [Test]
    public void Test3()
    {
        ClassicAssert.IsTrue(true);
    }
}