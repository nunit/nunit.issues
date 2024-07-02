namespace Issue4736;

using NUnit.Framework;

[TestFixture, Category("IntegrationTest")]
[Explicit]
[Ignore("Ignoring")]
public class MyTestClass
{
    [Test]
    public void MyTest()
    {
        Assert.Pass();
    }
}

[TestFixture, Category("IntegrationTest")]
[Explicit]
public class AnotherTestClass
{
    [Test]
    public void AnotherTest()
    {
        Assert.Pass();
    }
}

[TestFixture, Category("IntegrationTest")]
public class YetAnotherTestClass
{
    [Test]
    public void YetAnotherTest()
    {
        Assert.Pass();
    }
}
