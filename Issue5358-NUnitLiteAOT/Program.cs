using NUnit.Framework;

new NUnitLite.AutoRun(typeof(CustomTests).Assembly).Execute(args);

[TestFixture]
public class CustomTests
{
    [Test]
    public void Test1()
    {
        Assert.That(1, Is.EqualTo(1));
    }
}