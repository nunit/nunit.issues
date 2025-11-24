using System.Diagnostics;

namespace Issue3930;

[TestFixture]
internal class Bar
{
    [TearDown]
    public void TearDown()
    {
        // imitate long cleaning teardown
        Thread.Sleep(5000);
    }

    [Test]
    public void A()
    {
        Console.WriteLine("Bar.A");
    }

    [Test]
    public void B()
    {
        ShouldPass(
            async () =>
            {
                // imitate real async work to force context switch
                await Task.Yield();
                Thread.Sleep(3000);
                Assert.Fail();
            });
    }

    [Test]
    public void C()
    {
        Console.WriteLine("Bar.C");
    }

    private static void ShouldPass(Action action)
    {
        var budget = TimeBudget.StartNew(10000);
        while (!budget.HasExpired())
        {
            try
            {
                action();
                return;
            }
            catch (AssertionException)
            {
            }
        }
    }
}

[TestFixture]
internal class Foo
{
    [Test]
    public void A()
    {
        Console.WriteLine("Foo.A");
    }
}

public class TimeBudget
{
    private readonly Stopwatch _stopwatch;
    private readonly int _milliseconds;
    private TimeBudget(int milliseconds)
    {
        _milliseconds = milliseconds;
        _stopwatch = Stopwatch.StartNew();
    }
    public static TimeBudget StartNew(int milliseconds)
    {
        return new TimeBudget(milliseconds);
    }
    public bool HasExpired()
    {
        return _stopwatch.ElapsedMilliseconds >= _milliseconds;
    }
}