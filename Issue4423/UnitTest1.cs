namespace Issue4423;


public class Testit
{
    private readonly SomeStuff[] items;

    public Testit()
    {
        items = new SomeStuff[]
        {
            new("Name 1"),
            new("Name 2")
        };
    }



    [Test]
    public void Dummy1()
    {

        // OK
        Assert.That(items, Has.Exactly(2).Items);
        Assert.That(items, Has.ItemAt(0).Property(nameof(SomeStuff.Name)).EqualTo("Name 1"));
        Assert.That(items, Has.ItemAt(1).Property(nameof(SomeStuff.Name)).EqualTo("Name 2"));
    }

    [Test]
    public void Dummy2()
    {

        // OK
        Assert.That(items, Has.Exactly(2).Items
            .And.One.Property(nameof(SomeStuff.Name)).EqualTo("Name 1")
            .And.One.Property(nameof(SomeStuff.Name)).EqualTo("Name 2"));
    }

    [Test]
    public void Dummy3()
    {
        // NOT OK 1    Returns Stack Empty
        Assert.That(items, Has.Exactly(2).Items
            .And.ItemAt(0).Property(nameof(SomeStuff.Name)).EqualTo("Name 1"));
        //  .And.ItemAt(1).Property(nameof(SomeStuff.Name)).EqualTo("Name 2"));
    }
    [Test]
    public void Dummy3b()
    {
        // NOT OK 1
        Assert.That(items, Has.Exactly(2).Items
            .And.ItemAt(0).Not.Empty);
    }
    [Test]
    public void Dummy4()
    {
        // NOT OK 2  Returns System.ArgumentException : Default indexer accepting arguments < 0 > was not found on Issue4423.SomeStuff.
        Assert.That(items, Has.Exactly(2).Items
            .And.One.ItemAt(0).Property(nameof(SomeStuff.Name)).EqualTo("Name 1")
            .And.One.ItemAt(1).Property(nameof(SomeStuff.Name)).EqualTo("Name 2"));
    }

    [Test]
    public void Dummy5()
    {
        var sut = new SomeStuff("AName");
        Assert.That(sut,Has.Property(nameof(SomeStuff.Name)).EqualTo("AName"));
    }


}

public sealed record SomeStuff(string Name);