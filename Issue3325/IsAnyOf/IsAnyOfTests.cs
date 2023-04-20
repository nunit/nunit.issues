namespace VerifyNUnit4.IsAnyOf
{
    public class IsAnyOfTests
    {
        [Test]
        public void TestNotAnyOf()
        {
            var ls = new List<string> { "a", "b", "c" };
            string x = "e";
            Assert.That(x, Is.Not.AnyOf(ls));
        }

        [Test]
        public void TestDoesNotContain()
        {
            var ls = new List<string> { "a", "b", "c" };
            string x = "e";
            Assert.That(ls, Does.Not.Contain(x));
        }

        [Test]
        public void TestDoesContainFails()
        {
            var ls = new List<string> { "a", "b", "c" };
            string x = "e";
            Assert.That(() => Assert.That(ls, Does.Contain(x)), Throws.TypeOf<AssertionException>().With.Message.Contains("Expected: some item equal to \"e\""));
        }

        [Test]
        public void TestIsAnyOfFails()
        {
            var ls = new List<string> { "a", "b", "c" };
            string x = "e";
            Assert.That(() => Assert.That(x, Is.AnyOf(ls)), Throws.TypeOf<AssertionException>());
        }
    }
}
