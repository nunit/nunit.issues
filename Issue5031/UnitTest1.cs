using NUnit.Framework;

namespace Issue5031;

public class Tests
{
    private int i = 1;
    [Repeat(5)]
    [Test]
    public void Test1()
    {
        Log($"{i++} hi");
    }

    private void Log(string msg)
    {
        File.AppendAllText(@"..\..\..\log.log", msg + "\n");
        Console.WriteLine(msg);
    }

    private int count = 0;
    [Test]
    [Repeat(5, StopOnFailure = false)]
    public void Test2()
    {

        TestContext.Out.WriteLine(count);
        count++;
        Assert.That(count, Is.Not.EqualTo(3).And.Not.EqualTo(4));
    }
}
