namespace TestProject1
{
    [TestFixture]
    public class TestsWorkingWithStaticDatapointSource
    {
        public TestsWorkingWithStaticDatapointSource()
        {
            throw new Exception("Thrown in constructor");
        }

        [DatapointSource]
        private static int[] intSource = new int[] { 0, 1, 6, 8, 50 };

        [Test, Theory]
        public async Task MethodWithIntParam(int intVal)
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