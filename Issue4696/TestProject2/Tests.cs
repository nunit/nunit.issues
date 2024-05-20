
using NUnit.Framework;

namespace Issue4696.TestProject2
{
    public static class TestUtility
    {
        public static void Run(string message)
        {
            Assert.Pass(message);
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Run()
        {
            TestUtility.Run(nameof(Tests));
        }
    }
}
