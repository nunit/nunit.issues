using NUnit.Framework;

namespace Repro;

public class FilterRepro
{
    [TestCaseSource(nameof(Cases))]
    public void EscapedOperator(string value) => Assert.That(value, Is.Not.Null);

    private static IEnumerable<TestCaseData> Cases()
    {
        yield return new TestCaseData("a").SetName("{m}(Case 1: X = Y)");
    }
}
