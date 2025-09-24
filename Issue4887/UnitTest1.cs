global using NUnit.Framework;

using System.IO;

namespace Issue4887
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            TestContext.Out.WriteLine("Out");
            Assert.That(1, Is.EqualTo(2));
        }
    }
}