
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace RetrieveParallelScope
{
    [TestFixture, NonParallelizable]
    public class TestFixtureNonParallelizable
    {
        [Test, Parallelizable(ParallelScope.Self)]
        public void TestParallelizable()
        {
            if (NUnitEx.CurrentTestIsExecutingParallel)
            {
                // Use dynamic ephemeral port to run local socket test.
            }
            else
            {
                // Use fixed default port to run local socket test.
            }
        }

        [Test]
        public void TestNoneParallelizable()
        {
            Assert.That(NUnitEx.CurrentTestIsExecutingParallel, Is.False);
        }
    }
}
