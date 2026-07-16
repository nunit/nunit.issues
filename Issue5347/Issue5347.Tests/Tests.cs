using NUnit.Framework;

[TestFixture]
public class MyTests
{
    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Category("ExampleCategory")]
    [Test]
    public void Test2()
    {
        Assert.Pass();
    }

    [Property("Whatever", 42)]
    [Category("ExampleCategoryTwo")]
    [Test]
    public void Test3()
    {
        Assert.Pass();
    }
}

[TestFixture]
public class MyNonTests
{
    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Category("ExampleCategory")]
    [Test]
    public void Test2()
    {
        Assert.Pass();
    }

    [Property("Whatever", 42)]
    [Category("ExampleCategoryTwo")]
    [Test]
    public void Test3()
    {
        Assert.Pass();
    }
}