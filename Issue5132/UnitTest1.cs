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
}


