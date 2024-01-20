namespace Issue3849;

public class Tests
{

    [Test]
    public void Multi()
    {
        Assert.Multiple(() =>
        {
            Assert.Catch<ArgumentException>(() => throw new InvalidOperationException("x"));
            Assert.That(1, Is.EqualTo(2));
        });
    }



    [Test]
    public void AssertMultiple()
    {
        Assert.Multiple(() =>
        {
            Assert.That(1, Is.EqualTo(2));
            Assert.Catch<ArgumentException>(() => throw new InvalidOperationException("Hello"));
            Assert.Catch<ArgumentException>(() => { });
            Assert.That("A", Is.EqualTo("B"));
        });
    }
}