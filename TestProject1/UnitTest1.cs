namespace TestProject1
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            //using var streamX = new MemoryStream(new byte[5]);
            //using var streamY = new MemoryStream(new byte[5]);

            Assert.That(Array.Empty<int>(), Is.EqualTo(Array.Empty<int>()));
        }
    }
}