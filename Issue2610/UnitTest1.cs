namespace Issue2610;

using System.Threading;
using NUnit.Framework;

    [TestFixture, SingleThreaded]
    [Parallelizable(ParallelScope.Fixtures)] // or ParallelScope.All
    internal class TestClass
    {
        private Thread _setupThread;

        [OneTimeSetUp]
        public void OTSetup()
        {
            _setupThread = Thread.CurrentThread;
        }

        [SetUp]
        public void Setup()
        {
            Assert.That(Thread.CurrentThread, Is.EqualTo(_setupThread));
        }

        [Test]
        public void Test()
        {
            Assert.That(Thread.CurrentThread, Is.EqualTo(_setupThread));
        }

        [TearDown]
        public void Teardown()
        {
            Assert.That(Thread.CurrentThread, Is.EqualTo(_setupThread));
        }

        [OneTimeTearDown]
        public void OTTeardown()
        {
            Assert.That(Thread.CurrentThread, Is.EqualTo(_setupThread)); // failure, not always
        }
    }
