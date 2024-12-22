namespace otsu;

public class Tests
{
    

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        Assert.Fail();
    }

}
