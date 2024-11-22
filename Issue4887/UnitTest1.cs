global using NUnit.Framework;

using System.IO;

namespace Issue4887
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.That(1, Is.EqualTo(1));
        }
    }
}