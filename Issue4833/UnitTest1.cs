

using System.Threading;

[assembly: Parallelizable(ParallelScope.Fixtures)]


namespace Issue4833;
public class Fixture
{
    [Test, Apartment(ApartmentState.STA)]
    public void Test1()
    {
        TestContext.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, {TestContext.CurrentContext.WorkerId}");
        
    }

    [Test, Apartment(ApartmentState.STA)]
    public void Test2()
    {
        TestContext.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, {TestContext.CurrentContext.WorkerId}");
    }
}

public class Fixture2
{
    [Test, Apartment(ApartmentState.STA)]
    public void Test3()
    {
        TestContext.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, {TestContext.CurrentContext.WorkerId}");
    }

    [Test, Apartment(ApartmentState.STA)]
    public void Test4()
    {
        TestContext.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, {TestContext.CurrentContext.WorkerId}");
    }
}
