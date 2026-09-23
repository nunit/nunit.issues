using NUnit.Framework;
namespace IssueXXXX;

public class Repeat
{
    [Test, Repeat(20)]
    public void TestMethod()
    {
        if (Random.Shared.NextDouble() > 0.5)
        {
            // Assert.Fail("Random failure"); // works (preserves message+stacktrace)

            // fails: (loses message+stacktrace)
            throw new AssertionException("Random failure");

            // also fails with third-party libraries like FluentAssertions:
            // 1.Should().Be(2, "Random failure");
        }
    }
}
