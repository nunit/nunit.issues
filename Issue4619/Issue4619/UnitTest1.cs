using NUnit.Framework;

namespace Issue4619
{
    public class Tests
    {
        [Test, Timeout(240_000)]
        public void Test1()
        {
        }

        [TearDown]
        public void TearDown()
        {
            TestContext.WriteLine($"Status: {TestContext.CurrentContext.Result.Outcome.Status}");
        }
    }
}