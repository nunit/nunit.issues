using NUnit.Framework;

Console.WriteLine("Hello, World!");

new NUnitLite.AutoRun().Execute(args);

[TestFixture]
public class MyTests
{
    [Test]
    public void Test1()
    {
        Assert.AreEqual(2, 1 + 1);
    }
    [Test]
    public void Test2()
    {
        Assert.IsTrue(true);
    }
}