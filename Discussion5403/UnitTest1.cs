namespace Discussion5403;

public class Tests
{
    [Test]
    public void Test1()
    {
        int x = 42;
        Assert.That(x,Is.EqualTo(42));
        Assert.Ignore("The last ignore");
    }
}
