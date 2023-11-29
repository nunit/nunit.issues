
namespace Issue4572;

class TestClass
{
    public int Prop { get; set; } = 0;
}

public class Tests
{ 
    [Test]
    public void CompareObjects()
    {
        TestClass a = new() { Prop = 2 };
        TestClass b = new() { Prop = 2 };

        Assert.That(b, Is.Not.EqualTo(a));
    }
}