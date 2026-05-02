namespace Issue5146;


readonly record struct Distance(int Metres)
{
    public Distance Length => new(Math.Abs(Metres));

    public override string ToString()  // To avoid killing the watch window in VS, and then crash due to that.
    {
        return "";
    }
}
public class Tests
{
    

    [Test]
    public void Test1()
    {
        var sut = new Distance(-5);
        var sut2 = new Distance(5);
        Assert.That(sut, Is.EqualTo(sut2));
    }
}
