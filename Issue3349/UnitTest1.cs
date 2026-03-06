namespace Issue3349
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1([Values] object value) // Expected failed or ignored but PASSED reported!
        {
            Assert.Fail();
        }

        [Test]
        [Combinatorial]
        public void Test2(ConsoleColor color) // Expected failed or ignored but PASSED reported!
        {
            Assert.Fail();
        }

        [Test]
        [Sequential]
        public void Test3(ConsoleColor color) // Expected failed or ignored but PASSED reported!
        {
            Assert.Fail();
        }

        [Test]
        public void Test4() // Expected failed or ignored but PASSED reported!
        {
            Assert.Pass();
        }
    }
}
