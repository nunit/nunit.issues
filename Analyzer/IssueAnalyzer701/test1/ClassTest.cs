namespace RJCP.NUnitAnalyzerTestSample
{
    using NUnit.Framework;

    [TestFixture]
    public class ClassTest
    {
        [Test]
        public void TestZeroA()
        {
            TestClass c = new() {
                A = 0,
                B = 1
            };
            Assert.That(c.Multiply(), Is.EqualTo(0));
        }
    }
}