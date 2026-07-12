namespace Issue5340;

public class Tests
{
    

    [Test]
    public void Test1()
    {
        var whatever = new Whatever
        {
            Capacity = 5
        };
        Assert.That(whatever, Has.Property(nameof(Whatever.Capacity)).EqualTo(5));
    }

    [Test]
    public void Test2()
    {
        var whatever = new Whatever
        {
            Capacity = 5
        };
        Assert.That(whatever.Capacity, Is.EqualTo(5));
    }

    
}

public class Whatever
{
    public nint Capacity { get; set; }
}
