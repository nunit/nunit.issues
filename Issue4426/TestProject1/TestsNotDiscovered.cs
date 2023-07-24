namespace TestProject1
{
    [TestFixture]
    public class TestsNotDiscovered
    {
        public TestsNotDiscovered()
        {
            throw new Exception("Thrown in constructor");
        }

        [OneTimeSetUp]
        public void Init()
        {
            throw new Exception("Thrown in OneTimeSetUp");
        }

        [DatapointSource]
        private int[] intSource = new int[] { 0, 1, 6, 8, 50 };

        [Test, Theory]
        public async Task MethodWithIntParamBeingIgnored(int intVal)
        {
            await Task.Delay(intVal);
            Assert.Pass();
        }

        [Test]
        public void AnotherMethodAlsoIgnored()
        {
            Assert.Pass();
        }
    }
}