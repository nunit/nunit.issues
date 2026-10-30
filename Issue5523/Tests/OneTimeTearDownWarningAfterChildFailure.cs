namespace Issue5523;

// Expected fixture result: Failed (site Child), because Fails() fails.
// Actual with NUnit 5.0.0: Warning, from the Assert.Warn in OneTimeTearDown.
public class OneTimeTearDownWarningAfterChildFailure
{
    [Test]
    public void Fails() => Assert.Fail("child");

    [OneTimeTearDown]
    public void OneTimeTearDown() => Assert.Warn("ottd warn");
}
