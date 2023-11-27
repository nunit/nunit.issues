using System.Collections;

namespace Issue548;

public class Tests
{
    [Test]
    [Property("TestCaseID", "id1")]
    public void TestWithOutDataSource()
    {
        var prop = TestContext.CurrentContext.Test.Properties["TestCaseID"].FirstOrDefault()??"";
        TestContext.WriteLine($"{prop}");
        Assert.That(prop, Is.EqualTo("id1"));
    }
}

public class Tests2
{
    public static IEnumerable TestCases
    {

        get
        {
            yield return new TestCaseData(123);
            yield return new TestCaseData(456);
        }
    }

    [TestCaseSource(nameof(TestCases))]
    [Property("TestCaseID", "id2")]
    public void TestWithDataSource(int number)
    {
        var prop = TestContext.CurrentContext.Test.Properties["TestCaseID"].FirstOrDefault() ?? "";
        var test = TestContext.CurrentContext.Test;
        TestContext.WriteLine($"{prop}");
        Assert.That(prop, Is.EqualTo("id2"));
    }
}
