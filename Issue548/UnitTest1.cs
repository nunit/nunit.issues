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
            yield return new TestCaseData(456).SetProperty("SomeProperty","Whatever");
        }
    }

    [TestCaseSource(nameof(TestCases))]
    [Property("TestCaseID", "id2")]
    public void TestWithDataSource(int number)
    {
        var x = TestContext.CurrentContext.Test.PropertyHierarchy();
        Assert.That(x.Count, number == 456 ? Is.EqualTo(4) : Is.EqualTo(3));
        var propsa = TestContext.CurrentContext.Test.PropertyValues("TestCaseID").Select(o=>o.Values).ToList();
        var props = propsa.SelectMany(o=>o.Cast<string>()).ToList();
        Assert.That(props,Is.Not.Null);
        TestContext.WriteLine($"{string.Join(',',props)}");
        Assert.That(props, Does.Contain("id2"));
    }
}
;