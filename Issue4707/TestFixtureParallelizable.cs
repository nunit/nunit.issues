
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace RetrieveParallelScope
{
    [TestFixture, Parallelizable(ParallelScope.Children)]
    public class TestFixtureParallelizable
    {
        [Test]
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

        [Test, NonParallelizable]
        public void TestNoneParallelizable()
        {
            Assert.That(NUnitEx.CurrentTestIsExecutingParallel, Is.False);
        }
    }
}
