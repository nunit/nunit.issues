using NUnit.Framework;
using NUnitLite;

return new AutoRun(typeof(AotTests).Assembly).Execute(args);

public class AotTests
{
    // Expected: Passed. On NativeAOT with StackTraceSupport=false: Error, TargetInvocationException wrapping SuccessException.
    [Test]
    public void PassingTest() => Assert.Pass();

    // Expected: Failed with the assertion message. Here the recorded exception is the wrapper.
    [Test]
    public void FailingTest() => Assert.That(1, Is.EqualTo(2));

    // Control: an ordinary passing test that does not throw.
    [Test]
    public void PlainTest() => Assert.That(1, Is.EqualTo(1));
}
