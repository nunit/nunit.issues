//#define opt1
//#define opt2
#define opt3




namespace Some
{
#if opt1
    [SetUpFixture, Parallelizable(ParallelScope.Fixtures)]   // Works
#endif
    internal class Setup
    { }
#if opt2
    // [Parallelizable(ParallelScope.Fixtures)]  // Does not work
    // [TestFixture]
#endif
    public class Dummy
    {
#if opt2
        // [Test]
#endif
        public void TestDummy()
        { }
    }

    public class Dummy2
    {

        [Test]

        public void TestDummy()
        {
            TestContext.Out.WriteLine($"TestDummy:  {TestContext.CurrentContext.WorkerId}");
        }
    }


#if opt3
    [Parallelizable(ParallelScope.Fixtures)]  // Does not work
#endif
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
#if opt1
    [SetUpFixture, Parallelizable(ParallelScope.Fixtures)]  // Works
#endif
    internal class Setup
    { }

#if opt2
     [Parallelizable(ParallelScope.Fixtures)]  // Does not work
     [TestFixture]
#endif
    public class Dummy
    {
#if opt2
        [Test]
#endif
        public void TestDummy()
        { }
    }


#if opt3
    [Parallelizable(ParallelScope.Fixtures)]  // Does not work
#endif
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
