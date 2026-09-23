using NUnit.Framework;

namespace IssueXXXX;

public class Repeat
{
    [Test, Repeat(20)]
    public void TestMethod()
    {
        if (Random.Shared.NextDouble() > 0.5)
        {
            Assert.Fail("Random failure");
        }
    }
}
