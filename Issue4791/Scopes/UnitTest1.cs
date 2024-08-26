
namespace Some
{

    [SetUpFixture, Parallelizable(ParallelScope.Fixtures)]
    internal class Setup
    { }

    // [Parallelizable(ParallelScope.Fixtures)]
    // [TestFixture]
    public class Dummy
    {
        // [Test]
        public void TestDummy()
        { }
    }



    // [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture("Hey")]
    [TestFixture("Hello")]
    public class SomeFixture(string s)
    {
        [Test]
        public async Task Test1()
        {
            TestContext.Out.WriteLine($"Test1: {s} {TestContext.CurrentContext.WorkerId}");
            await Task.Delay(10000);
            Assert.Pass();
        }

        [Test]
        public async Task Test2()
        {
            TestContext.Out.WriteLine($"Test1: {s} {TestContext.CurrentContext.WorkerId}");
            await Task.Delay(10000);
            Assert.Pass();
        }
    }
}


namespace Another
{

    [SetUpFixture, Parallelizable(ParallelScope.Fixtures)]  // Works
    internal class Setup
    { }

    // [Parallelizable(ParallelScope.Fixtures)]  // Does not work
    // [TestFixture]
    public class Dummy
    {
        // [Test]
        public void TestDummy()
        { }
    }



    //  [Parallelizable(ParallelScope.Fixtures)]  // Does not work
    [TestFixture("Hey")]
    [TestFixture("Hello")]
    public class AnotherFixture(string s)
    {

        [Test]
        public async Task Test1()
        {
            TestContext.Out.WriteLine($"Test1: {s} {TestContext.CurrentContext.WorkerId}");
            await Task.Delay(10000);
            Assert.Pass();
        }

        [Test]
        public async Task Test2()
        {
            TestContext.Out.WriteLine($"Test1: {s} {TestContext.CurrentContext.WorkerId}");
            await Task.Delay(10000);
            Assert.Pass();
        }
    }
}
