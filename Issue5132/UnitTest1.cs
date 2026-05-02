using NUnit.Framework.Internal;

namespace Issue5132;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
        SimplifyAbort();
    }
    
    [Test]
    public void Test2()
    {
      Assert.Pass();
    }

    [UnhandledExceptionHandling(UnhandledExceptionHandling.Error)]
    [Test]
    public void Test3()
    {
        for (int i = 0; i < 3; i++)
            SimplifyAbort();
    }

    
    
    
    
    [Test]
    public void Test4()
    {
        for (int i = 0; i < 3; i++)
            SimplifyAbortFix();
    }

    public static void SimplifyAbort()
        {
            var thread = new Thread(() => 
            {
                Thread.Sleep(1000);
            });
            thread.Start();
            thread.Join(500);
            thread.Abort();
        }

    public static void SimplifyAbortFix()
    {
        var thread = new Thread(() =>
        {
            try
            {
                Thread.Sleep(1000);
            }
            catch (ThreadAbortException)
            {
                TestContext.Out.WriteLine("ThreadAbortException");
                Thread.ResetAbort(); // optional: prevents rethrow at end of catch/finally
            }
        });

        thread.Start();
        thread.Join(500);

        if (thread.IsAlive)
            thread.Abort();

        thread.Join(); // ensure it’s finished before returning
    }



    [Explicit]
    [Timeout(300)]
    [Test]
    public void Test5()
    {
        var thread = new Thread(() =>
        {
            Thread.Sleep(1000);
        });
        thread.Start();
        thread.Join(1200);
    }
    

}


