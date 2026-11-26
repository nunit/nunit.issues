using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Issue3513;

// Each one-time method waits 10 s on the test cancellation token.
// With [CancelAfter(1000)] it should be cancelled after about 1 s.
[CancelAfter(1000)]
public class OneTimeSetUpHangs
{
    [OneTimeSetUp]
    public async Task BeforeAll()
    {
        CancellationToken token = TestContext.CurrentContext.CancellationToken;
        TestContext.Progress.WriteLine($"OneTimeSetUp: CanBeCanceled={token.CanBeCanceled}");
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        TestContext.Progress.WriteLine("OneTimeSetUp: ran the full 10 s, not cancelled");
    }

    [Test]
    public void Test1() => Assert.That(1, Is.EqualTo(1));
}

[CancelAfter(1000)]
public class OneTimeTearDownHangs
{
    [Test]
    public void Test1() => Assert.That(1, Is.EqualTo(1));

    [OneTimeTearDown]
    public async Task AfterAll()
    {
        CancellationToken token = TestContext.CurrentContext.CancellationToken;
        TestContext.Progress.WriteLine($"OneTimeTearDown: CanBeCanceled={token.CanBeCanceled}");
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        TestContext.Progress.WriteLine("OneTimeTearDown: ran the full 10 s, not cancelled");
    }
}
