# Issue 5577: `CancelAfter` can crash the test host with `ObjectDisposedException`

https://github.com/nunit/nunit/issues/5577

Each test case sleeps for about as long as the `[CancelAfter(50)]` timeout, so many of them end as the timer fires.
The timer callback in `CancelAfterCommand.Execute` then calls `Cancel()` on a `CancellationTokenSource` that `Execute`
has already disposed. The exception is thrown on a thread pool thread, so it crashes the test host.

```csharp
[CancelAfter(50)]
public sealed class CancelAfterRaceRepro
{
    [TestCaseSource(nameof(Runs))]
    public void EndsAsTheTimerFires(int run) => Thread.Sleep(45 + (run % 11));

    private static IEnumerable<int> Runs() => Enumerable.Range(0, 2000);
}
```

## Run

```sh
dotnet test
```

NUnit 5.0.0, NUnit3TestAdapter 6.3.1, Microsoft.NET.Test.Sdk 18.11.0, net10.0, Windows 11.

## Actual (NUnit 5.0.0)

The test host crashed in 4 of 4 runs, after 27 or fewer of the 2000 test cases:

```
The active test run was aborted. Reason: Test host process crashed : Process path: ...\Issue5577\bin\Debug\net10.0\testhost.exe
Unhandled exception. System.AggregateException: One or more errors occurred. (The CancellationTokenSource has been disposed.)
 ---> System.ObjectDisposedException: The CancellationTokenSource has been disposed.
   at System.Threading.CancellationTokenSource.Cancel()
   at NUnit.Framework.Internal.Commands.CancelAfterCommand.<>c__DisplayClass3_0.<Execute>b__0()
   ...
   at System.Threading.CancellationTokenSource.ExecuteCallbackHandlers(Boolean throwOnFirstException)
   at System.Threading.TimerQueueTimer.Fire(Boolean isThreadPool)

Test Run Aborted.
```

## Expected

The run completes. Each test case passes or is reported as cancelled.
