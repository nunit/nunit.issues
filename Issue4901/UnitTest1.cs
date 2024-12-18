using System.Diagnostics;

namespace Issue4901;

public class Tests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Console.WriteLine("OneTimeSetUp by Console.WriteLine");
        Trace.WriteLine("OneTimeSetUp by Trace.WriteLine");
        TestContext.Out.WriteLine("OneTimeSetUp by TestContext.Out.WriteLine");
        TestContext.Progress.WriteLine("OneTimeSetUp by TestContext.Progress.WriteLine");
        TestContext.Error.WriteLine("OneTimeSetUp by TestContext.Error.WriteLine");
    }

    [SetUp]
    public void Setup()
    {
        Console.WriteLine("SetUp by Console.WriteLine");
        Trace.WriteLine("SetUp by Trace.WriteLine");
        TestContext.Out.WriteLine("SetUp by TestContext.Out.WriteLine");
        TestContext.Progress.WriteLine("SetUp by TestContext.Progress.WriteLine");
        TestContext.Error.WriteLine("SetUp by TestContext.Error.WriteLine");
    }

    [Test]
    public void Test1()
    {
        Console.WriteLine("Test by Console.WriteLine");
        Trace.WriteLine("Test by Trace.WriteLine");
        TestContext.Out.WriteLine("Test by TestContext.Out.WriteLine");
        TestContext.Progress.WriteLine("Test by TestContext.Progress.WriteLine");
        TestContext.Error.WriteLine("Test by TestContext.Error.WriteLine");
        Assert.Pass();
    }
}

[SetUpFixture]
public class ASetupFixture
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Console.WriteLine("SetUpFixture.OneTimeSetUp by Console.WriteLine");
        Trace.WriteLine("SetUpFixture.OneTimeSetUp by Trace.WriteLine");
        TestContext.Out.WriteLine("SetUpFixture.OneTimeSetUp by TestContext.Out.WriteLine");
        TestContext.Progress.WriteLine("SetUpFixture.OneTimeSetUp by TestContext.Progress.WriteLine");
        TestContext.Error.WriteLine("SetUpFixture.OneTimeSetUp by TestContext.Error.WriteLine");
    }
}
