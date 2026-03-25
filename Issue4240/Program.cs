#:package NUnit@4.5.1
#:package NUnitlite@4.5.1

using NUnit.Framework;
using NUnitLite;

[assembly: Apartment(ApartmentState.STA)]

new AutoRun().Execute(args);

public static class ThreadTracker
{
    public static readonly int MainThreadId = Thread.CurrentThread.ManagedThreadId;
}

public class MyTest
{
    [Test]
    public void SampleTest()
    {
        var testThreadId = Thread.CurrentThread.ManagedThreadId;
        Assert.That(testThreadId, Is.EqualTo(ThreadTracker.MainThreadId));
    }
}
