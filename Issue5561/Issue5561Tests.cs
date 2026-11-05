using NUnit.Framework;

namespace Issue5561;

public class Issue5561Tests
{
    [Test]
    public void CaughtAssertPassSetsResultToPassed()
    {
        try
        {
            Assert.Pass("pass message");
        }
        catch (SuccessException)
        {
        }

        var result = TestContext.CurrentContext.Result;
        TestContext.Out.WriteLine($"Outcome: {result.Outcome}, Message: '{result.Message}'");
        Assert.That(result.Outcome.Status, Is.EqualTo(NUnit.Framework.Interfaces.TestStatus.Passed));
    }
}
