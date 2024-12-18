using NUnit.Framework;

namespace NunitTest;

[TestFixture]
public class Fixture
{
    [Test]
    public void Test_Bugged()
    {
        TestClass testClass = new TestClass(1);
        TestClass testClassOther = new TestClass(1);
        Assert.That(testClassOther, Is.EqualTo(testClass));
    }
    [Test]
    public void Test_ok_explicit_caset()
    {
        TestClass testClass = new TestClass(1);
        TestClass testClassOther = new TestClass(1);
        Assert.That((object)testClassOther, Is.EqualTo((object)testClass));
    }
    [Test]
    public void Test_Ok()
    {
        TestClass testClass = new TestClass(1);
        TestClass testClassOther = new TestClass(1);

        //This is ok
        Assert.That(testClassOther.AsString(), Is.EqualTo(testClass.AsString()));
    }
}

public class TestClass
{
    private string _id;

    public TestClass(int number)
    {
        _id = "Test_" + number;
    }

    public override string ToString()
    {
        return _id;
    }

    public static implicit operator string(TestClass testClass)
    {
        return testClass._id;
    }

    public string AsString()
    {
        return _id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;

        var identity = obj as TestClass;

        if (identity != null)
        {
            return Equals(identity);
        }

        return false;
    }

    public bool Equals(TestClass other)
    {
        if (other != null)
        {
            return other._id.Equals(_id);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}