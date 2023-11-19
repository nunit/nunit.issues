namespace Issue4491;

public class Tests
{
    //[Test]
    //[Repeat(30)]
    //public void Test1()
    //{
    //    bool x = true;
    //    Assert.That(x, Is.False);
    //}

    [Test]
    [Repeat(5)]
    public void Test2()
    {
        bool x = true;
        Assert.That(x, Is.True);
        TestContext.WriteLine(TestContext.CurrentContext.Test.ID);
    }

   
    [Test]
    public void Test3()
    {
        bool x = true;
        Assert.That(x, Is.True);
    }

}