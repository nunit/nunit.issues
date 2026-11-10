using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace Issue5577;

[CancelAfter(50)]
public sealed class CancelAfterRaceRepro
{
    [TestCaseSource(nameof(Runs))]
    public void EndsAsTheTimerFires(int run) => Thread.Sleep(45 + (run % 11));

    private static IEnumerable<int> Runs() => Enumerable.Range(0, 2000);
}
