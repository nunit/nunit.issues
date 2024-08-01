namespace Issue548;


public class TestIssue1358
{
    [TestCase(5 )]
    [Property("Answer", 42)]
    public void TestCaseCanAccessItsOwnProperties(int x)
    {
        var answers = TestContext.CurrentContext.Test.AllPropertyValues("Answer");
        var answer = answers.FirstOrDefault() as int?;
        Assert.That(answer, Is.EqualTo(42));
        var parentProps = TestContext.CurrentContext.Test.Parent.Properties.Get("Answer") as int?;
        Assert.That(parentProps, Is.EqualTo(42));
        var props = TestContext.CurrentContext.Test.AllPropertyValues("Answer").ToList();
        Assert.That(props,Has.Count.EqualTo(1));
        var prop = props[0] as int?;
        Assert.That(prop,Is.EqualTo(42));
    }

    [SpecialTestCaseAttribute(5)]
    public void TestCaseCanAccessItsOwnPropertiesSpecial(int x)
    {
        var answer = TestContext.CurrentContext.Test.Properties.Get("Answer") as int?;
        Assert.That(answer, Is.EqualTo(42));
        var parentProps = TestContext.CurrentContext.Test.Parent.Properties.Get("Answer") as int?;
        Assert.That(parentProps, Is.Null);
    }
}


public class SpecialTestCaseAttribute : TestCaseAttribute
{
    public SpecialTestCaseAttribute(int x) : base(x)
    {
        Properties.Add("Answer", 42);
    }
}