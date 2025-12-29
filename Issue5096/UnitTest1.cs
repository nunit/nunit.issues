using System.Threading;

namespace Issue5096;

// Invoke as 'dotnet bin\Debug\net10.0\Issue5096.dll --timeout 5s'
public class Tests
{
    [SetUp]
    public void Setup()
    {
        Thread.Sleep(10000);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}