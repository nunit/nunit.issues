
using NUnit.Framework;

namespace Issue5126
{
    [TestFixture]
    public class TestFixture
    {
        [Test, Explicit]
        public void ExplicitTestMethod()
        {
            Assert.Pass();
        }

        [Test]
        public void NonExplicitTestMethod()
        {
            Assert.Pass();
        }
    }
}
