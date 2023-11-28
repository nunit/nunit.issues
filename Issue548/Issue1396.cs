namespace Issue548
{
    [TestFixture]
    [Category("a")]
    public class SampleTest
    {

       
        [Test]
        [Category("b")]
        public void SomeSampleTest()
        {
            var categories = TestContext.CurrentContext.Test.AllCategories().ToList();

            //categories will only contain a single element, "b"
            Assert.That(categories, Does.Contain("a"));
            Assert.That(categories, Does.Contain("b"));
        }
    }
}
