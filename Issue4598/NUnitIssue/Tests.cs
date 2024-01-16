using NUnit.Framework;

namespace NUnitIssue;

[TestFixture]
public class Tests
{
    [Test]
    public void Test1()
    {
        TestContext.WriteLine("line1");
        Assert.That(1, Is.EqualTo(0));
    }

    [TearDown]
    public void TearDown()
    {
    }
}