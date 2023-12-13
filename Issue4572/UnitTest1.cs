
namespace Issue4572;

class TestClass
{
    public int Prop { get; set; } = 0;
}

public class Tests
{ 
    [Category("Whatever")]
    [Test]
    public void CompareObjects()
    {
        TestClass a = new() { Prop = 2 };
        TestClass b = new() { Prop = 2 };

        // Assert.That(b, Is.Not.EqualTo(a));  // Value Equality - they are identical
        Assert.That(b, Is.Not.SameAs(a));      // Reference Equality - they are different objects
    }
}