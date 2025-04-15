using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

[SuppressMessage("ReSharper", "CheckNamespace")]
#pragma warning disable S3903

[SetUpFixture]
internal class AssemblyFixture
{
    internal static readonly int DummyMember = 0;

    static AssemblyFixture()
    {
        Task.Run(() => PrintToProgressAsync(TimeSpan.FromMilliseconds(500)));
    }

    private static async Task PrintToProgressAsync(TimeSpan printStatisticsInterval)
    {
        for (var i = 0; i < 10; i++)
        {
            await Task.Delay(printStatisticsInterval);
            await TestContext.Progress.WriteLineAsync("Timer tick!!!");
        }
    }
}

namespace TestProject1
{
    public class Tests1
    {
        [Test]
        [TestCaseSource(nameof(DummyTestCaseSource))]
        public void DummyTest(string x)
        {
        }

        private static IEnumerable<TestCaseData> DummyTestCaseSource()
        {
            // Comment to "fix":
            _ = AssemblyFixture.DummyMember;
            yield break;
        }
    }

    public class Tests
    {
        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("Test1 started");
            Thread.Sleep(5000);
            TestContext.Progress.WriteLine("Test1 finished");
        }
    }
}
