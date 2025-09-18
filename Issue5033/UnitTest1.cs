namespace Issue5033;

public class Tests
{
    

    [Test]
    public void Test1()
    {
        int x = 1;
        Assert.That(x, Is.EqualTo(42));
    }
}
