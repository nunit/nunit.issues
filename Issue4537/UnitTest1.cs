using NUnit.Framework.Legacy;

namespace Issue4537;

public class TearDownTests
{
    [Test]
    public void Assertion()
    {
        ClassicAssert.IsTrue(false);
    }

    [Test]
    public void Exception()
    {
        throw new Exception("Test exception");
    }

    [Test]
    public void Pass()
    {

    }

    [Test]
    public void ExceptionInMultiple()
    {
        Assert.Multiple(()=> 
        {
            Assert.That(1, Is.EqualTo(2));
            throw new Exception("Test exception");
            Assert.That(1, Is.EqualTo(3));
            Assert.That(1, Is.EqualTo(4));
        });
        
    }


    [TearDown]
    public void TearDown()
    {
        Assert.Warn("Warning");
    }
}