namespace IssueAnalyzer665
{
    public class Tests
    {
        public Task<bool> Something() => Task.FromResult(true);

        public bool SomethingSimple() => true;

        [Test]
        public async Task MyTest()
        {
            //  Assert.That(Something(),Is.EqualTo(true));  // Function call, but with warning and fails
            Assert.That(() => Something(), Is.EqualTo(true));  // Delegate
            Assert.That(Something,Is.EqualTo(true));  // Delegate with method group (suggested by VS/Roslyn, green line under Something above)

            Assert.That(SomethingSimple(), Is.EqualTo(true));  // Function call
            Assert.That(SomethingSimple(), Is.Not.EqualTo(false)); // Function call

            await Assert.ThatAsync(() => Something(), Is.EqualTo(true));  // Delegate
            await Assert.ThatAsync(Something, Is.EqualTo(true));  // Delegate with method group
            await Assert.ThatAsync(Something, Is.Not.EqualTo(false)); //Delegate with method group
            TestContext.CurrentContext.CurrentRepeatCount

        }
    }
}