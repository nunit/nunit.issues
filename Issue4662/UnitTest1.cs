namespace Issue4662;

public class Tests
{
    

    [TestCase("dhdhdh")]
    public void Test1(string s)
    {
        Assert.That(s,Is.Not.Null.And.Not.Empty);
    }
    
    [TestCase("   ")]
    [TestCase(null)]
    public void Test2(string? s)
    {
        Assert.That(s, Is.Null.Or.Empty);
    }
}