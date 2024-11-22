

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

[assembly: Parallelizable(ParallelScope.Fixtures)]


namespace Issue4833;
public class Fixture
{
    [Test, Apartment(ApartmentState.STA)]
    public void Test1()
    {
        TestContext.WriteLine($"{nameof(Test1)}");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        TestContext.WriteLine($"Started at {DateTime.Now:HH:mm:ss.fff}");
        Thread.Sleep(10);
        TestContext.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, {TestContext.CurrentContext.WorkerId}");
        TestContext.WriteLine($"Started at {DateTime.Now:HH:mm:ss.fff}");
        stopwatch.Stop();
        

    }

    [Test, Apartment(ApartmentState.STA)]
    public void Test2()
    {
        TestContext.WriteLine($"{nameof(Test2)}");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        TestContext.WriteLine($"Started at {DateTime.Now:HH:mm:ss.fff}");
        Thread.Sleep(10);
        TestContext.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, {TestContext.CurrentContext.WorkerId}");
        TestContext.WriteLine($"Stopped at {DateTime.Now:HH:mm:ss.fff}");
        stopwatch.Stop();
    }
}

public class Fixture2
{
    [Test, Apartment(ApartmentState.STA)]
    public void Test3()
    {
        TestContext.WriteLine($"{nameof(Test3)}");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        TestContext.WriteLine($"Started at {DateTime.Now:HH:mm:ss.fff}");
        Thread.Sleep(10);
        TestContext.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, {TestContext.CurrentContext.WorkerId}");
        TestContext.WriteLine($"Stopped at {DateTime.Now:HH:mm:ss.fff}");
        stopwatch.Stop();
    }

    [Test, Apartment(ApartmentState.STA)]
    public void Test4()
    {
        TestContext.WriteLine($"{nameof(Test4)}");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        TestContext.WriteLine($"Started at {DateTime.Now:HH:mm:ss.fff}");
        Thread.Sleep(10);
        TestContext.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, {TestContext.CurrentContext.WorkerId}");
        TestContext.WriteLine($"Stopped at {DateTime.Now:HH:mm:ss.fff}");
        stopwatch.Stop();
    }
}
