
namespace Issue4572;

class TestClass
{
    public int Prop { get; set; } = 0;
}

public class PropertyTester
{ 
    [Test]
    public void CompareObjects()
    {
        TestClass a = new() { Prop = 2 };
        TestClass b = new() { Prop = 2 };

        Assert.That(b, Is.EqualTo(a));  // Value Equality - they are identical
        Assert.That(b, Is.Not.SameAs(a));      // Reference Equality - they are different objects
    }

    [Test]
    public void CompareUsingEquals()
    {
        TestClass a = new() { Prop = 2 };
        TestClass b = new() { Prop = 2 };

        Assert.That(b.Equals(a),Is.False);  // Reference Equality - they are not identical
        //Assert.That(b, Is.Not.SameAs(a));      // Reference Equality - they are different objects
    }

}

public class TestClassWithFields
{
    int _field;

    public TestClassWithFields(int n)
    {
        _field = n;
    }
}

public class FieldsTester
{
    [Test]
    public void CompareObjects()
    {
        TestClassWithFields a = new(2);
        TestClassWithFields b = new(2);

        Assert.That(b, Is.EqualTo(a));  // Value Equality - they are identical
        //Assert.That(b, Is.Not.SameAs(a));      // Reference Equality - they are different objects
    }

    [Test]
    public void CompareUsingEquals()
    {
        TestClassWithFields a = new(2);
        TestClassWithFields b = new(2);

        Assert.That(b.Equals(a),Is.False);  // Value Equality - they are identical
        //Assert.That(b, Is.Not.SameAs(a));      // Reference Equality - they are different objects
    }
}
