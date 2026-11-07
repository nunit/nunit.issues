namespace Issue5554;

// These pass under JIT. NativeAot/ runs the same file as a NativeAOT NUnitLite app,
// where AsyncTest is Invalid and ContextOnAnotherThread fails with NUnit 5.0.0.
public class AotAsyncTests
{
    [Test]
    public void SyncTest() => Assert.Pass();

    [Test]
    public async Task AsyncTest() => await Task.Yield();

    [Test]
    public void ContextOnAnotherThread()
    {
        // A thread that doesn't inherit the test's context gets NUnit's AdhocContext.
        string? name = null;
        Exception? error = null;
        Thread thread;
        using (ExecutionContext.SuppressFlow())
        {
            thread = new Thread(() =>
            {
                try { name = TestContext.CurrentContext.Test.Name; }
                catch (Exception e) { error = e; } // uncaught, this kills the whole run
            });
            thread.Start();
        }
        thread.Join();
        Assert.That(error, Is.Null, () => error!.Message);
        Assert.That(name, Is.EqualTo("AdhocTestMethod"));
    }
}
