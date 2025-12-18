namespace NUnitIssue2466;

public class Tests
{
    [OneTimeSetUp]
    public void Setup()
    {
        SomeOtherMethod();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    public void SomeOtherMethod()
    {
        throw new SpecialException("SomeOtherMethod");
    }
}

public class SpecialException(string someothermethod) : Exception(someothermethod);

public class SqrtTests
{
    [DatapointSource]
    public TestWrapper[] values =
    {
        new (new Entity(42.0),true),  // Case med Entity fylt ut
        new (null,false)               // Case med Entity null
    };

    [Theory]
    public void SquareRootDefinition(TestWrapper num)
    {
        if (num.Entity is null)
        {
            Assert.That(num.Expected, Is.False);
        }
        else
        {
            Assert.That(num.Entity, Is.Not.Null);
        }
    }
}

public class TestWrapper
{

    public IEntity? Entity { get; set; }
    public bool Expected { get; set; }

    public TestWrapper(IEntity? entity, bool exp)
    {
        Entity = entity;
        Expected = exp;
    }
}

public class Entity : IEntity
{
    public double X { get; set; }

    public Entity(double x)
    {
        X = x;
    }
}

public interface IEntity
{
    double X { get; }
}

