namespace Issue2363;

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Api;
using NUnit.Framework.Interfaces;

// The repro fixture in SharePageOnSocialMedias.cs is never asserted on by
// itself - a test can only report its own name, not judge whether it is
// correct. Instead we load the assembly through the same in-process
// runner NUnit's own test suite uses (see NUnitTestAssemblyRunner /
// DefaultTestAssemblyBuilder in nunit/src/NUnitFramework/tests/Api),
// which builds the full test tree - including every fixture instance and
// parameterized test case - without executing any test bodies. That lets
// us inspect the FullName NUnit actually assigned, which is what
// https://github.com/nunit/nunit/issues/2363 is about.
public class FullNameTests
{
    private ITest _loadedTest = null!;

    [OneTimeSetUp]
    public void LoadRepro()
    {
        var runner = new NUnitTestAssemblyRunner(new DefaultTestAssemblyBuilder());
        _loadedTest = runner.Load(typeof(SharePageOnSocialMedias).Assembly, new Dictionary<string, object>());
    }

    private static IEnumerable<ITest> AllTests(ITest test)
    {
        yield return test;
        foreach (var child in test.Tests)
        {
            foreach (var descendant in AllTests(child))
            {
                yield return descendant;
            }
        }
    }

    private void AssertFullNameExists(string expectedFullName)
    {
        var actualFullNames = AllTests(_loadedTest).Select(t => t.FullName).ToList();
        Assert.That(actualFullNames, Has.Member(expectedFullName),
            $"Expected a test with FullName '{expectedFullName}'.{System.Environment.NewLine}" +
            $"Actual FullNames:{System.Environment.NewLine}{string.Join(System.Environment.NewLine, actualFullNames)}");
    }

    [TestCase("FirefoxWin")]
    [TestCase("ChromeWin")]
    public void FixtureSuite_HasExpectedFullName(string environment)
    {
        AssertFullNameExists($"Issue2363.SharePageOnSocialMedias({environment})");
    }

    [TestCase("FirefoxWin")]
    [TestCase("ChromeWin")]
    public void ParameterizedMethodSuite_HasExpectedFullName(string environment)
    {
        AssertFullNameExists($"Issue2363.SharePageOnSocialMedias({environment}).it_can_share_by_email");
    }

    [TestCase("FirefoxWin")]
    [TestCase("ChromeWin")]
    public void IndividualTestCase_HasExpectedFullName(string environment)
    {
        AssertFullNameExists($"Issue2363.SharePageOnSocialMedias({environment}).it_can_share_by_email(\"gac-intranet\")");
    }

    [TestCase("FirefoxWin")]
    [TestCase("ChromeWin")]
    public void PlainMethod_HasExpectedFullName(string environment)
    {
        AssertFullNameExists($"Issue2363.SharePageOnSocialMedias({environment}).it_can_share_on_tumblr");
    }
}
