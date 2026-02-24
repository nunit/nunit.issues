namespace Issue5136;

public class Tests
{
    [TestCase(null)]
    [TestCase([null])]
    [TestCase((string?)null)]
    public void Test1(params string?[] members)
    {
        Assert.That(members, Is.Not.Null);
    }


}

public class Tests2
{
    [Test]
    public void Verify()
    {
        var test = new Tests();
        test.Test1((string?)null);
    }
}
