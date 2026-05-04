using System;
using NUnit.Framework;

[TestFixture]
public class RetryTest
{
    [Retry(10, RetryExceptions = [typeof(Exception)])]
    [Test]
    public void RunsAll10ThenFails()
    {
        Console.WriteLine("try " + TestContext.CurrentContext.CurrentRepeatCount);
        Assert.Fail();
    }

    [Retry(10, RetryExceptions = [typeof(Exception)])]
    [Test]
    public void InconclusiveAfterFirst()
    {
        Console.WriteLine("try " + TestContext.CurrentContext.CurrentRepeatCount);
        throw new Exception();
    }
}
