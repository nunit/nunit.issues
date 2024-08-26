namespace Some
{

    [SetUpFixture, Parallelizable(ParallelScope.Fixtures)]
    internal class Setup
    { }


    [TestFixture("Hey")]
    [TestFixture("Hello")]
    public class SomeFixture(string s)
    {
        [Test]
        public async Task Test1()
        {
            TestContext.Out.WriteLine($"Test1: {s} {TestContext.CurrentContext.WorkerId}");
            await Task.Delay(5000);
            Assert.Pass();
        }

        [Test]
        public async Task Test2()
        {
            TestContext.Out.WriteLine($"Test1: {s} {TestContext.CurrentContext.WorkerId}");
            await Task.Delay(5000);
            Assert.Pass();
        }
    }
}


namespace Another
{

    [SetUpFixture, Parallelizable(ParallelScope.Fixtures)]
    internal class Setup
    { }

    [TestFixture("Hey")]
    [TestFixture("Hello")]
    public class AnotherFixture(string s)
    {
        
        [Test]
        public async Task Test1()
        {
            TestContext.Out.WriteLine($"Test1: {s} {TestContext.CurrentContext.WorkerId}");
            await Task.Delay(5000);
            Assert.Pass();
        }

        [Test]
        public async Task Test2()
        {
            TestContext.Out.WriteLine($"Test1: {s} {TestContext.CurrentContext.WorkerId}");
            await Task.Delay(5000);
            Assert.Pass();
        }
    }
}
