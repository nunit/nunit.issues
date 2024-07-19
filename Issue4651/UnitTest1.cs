namespace Issue4651;

[TestFixture, Category("TestsA")]
public class TestsA
{
    [Test]
    public void TestInA()
    {
        Assert.Pass();
    }
}

[TestFixture, Category("TestsB")]
public class TestsB
{
    [Test]
    public void TestInB()
    {
        Assert.Fail();
    }
}

[TestFixture, Category("TestsC")]
public class TestsC
{
    [Test]
    public void TestInC()
    {
        Assert.Pass();
    }
}