namespace Issue2363;

using System.Collections;

// Minimal repro distilled from the original bug report, which used a
// browser-automation DSL (Go.To<T>().Should.Exist() etc). None of that
// DSL affects test naming, so it has been stripped entirely - only the
// shape that matters for the FullName bug is kept:
//   an abstract [TestFixture] base class
//   + a sealed derived fixture parameterized via [TestFixtureSource]
//   + a test method that is itself parameterized via [TestCase]
//   + a plain (non-parameterized) test method

public enum E
{
    FirefoxWin,
    ChromeWin
}

public class FirefoxSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return E.FirefoxWin;
    }
}

public class ChromeSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return E.ChromeWin;
    }
}

[TestFixture]
public abstract class ShareBaseSpec
{
    protected readonly E Environment;

    protected ShareBaseSpec(E environment)
    {
        Environment = environment;
    }
}

[TestFixtureSource(typeof(FirefoxSource))]
[TestFixtureSource(typeof(ChromeSource))]
public sealed class SharePageOnSocialMedias : ShareBaseSpec
{
    private const string AppSlug = "gac-intranet";

    public SharePageOnSocialMedias(E environment) : base(environment)
    {
    }

    [Test]
    [TestCase(AppSlug)]
    public void it_can_share_by_email(string testCaseAppSlug)
    {
        Assert.Pass();
    }

    [Test]
    public void it_can_share_on_tumblr()
    {
        Assert.Pass();
    }
}
