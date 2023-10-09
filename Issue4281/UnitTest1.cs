using System.Diagnostics;

namespace Issue4281;

public class Tests
{
    private Stopwatch stopWatch;
    
    [SetUp]
    public void Setup()
    {
        count = 0;
        stopWatch = new Stopwatch();
    }

    [Test]
    public void TestThrowsAfter()
    {
        stopWatch.Start();
        Assert.That(()=>ThrowingMethod(), Throws.TypeOf<ArgumentException>().After(5).Seconds.PollEvery(500).MilliSeconds);
    }

    private int count;
    private int ThrowingMethod()
    {
        // Thread.Sleep(50);
        
        var ts = stopWatch.Elapsed;

        TestContext.WriteLine("RunTime " + $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");
        // Uncommenting this code has no effect on the outcome:
        if (count < 5)
        {
            TestContext.WriteLine($"ThrowingMethod: {count}");
            TestContext.WriteLine($"------------------");
            return ++count;
        }
        throw new ArgumentException("test");
    }

    /// <summary>
    /// This test fails correctly
    /// </summary>
    [Test]
    public void TestThrows()
    {
        Assert.That(()=>ThrowingMethod(), Throws.TypeOf<ArgumentException>());
    }
}