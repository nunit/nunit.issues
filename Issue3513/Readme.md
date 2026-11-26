# Issue 3513: Support for CancelAfter in OneTimeSetUp and OneTimeTearDown

https://github.com/nunit/nunit/issues/3513

The original repro used `[assembly: Timeout(1000)]`. `Timeout` is .NET Framework only in NUnit 4 and later, so this repro uses
`[CancelAfter(1000)]` on the fixture instead. Each one-time method waits 10 s on `TestContext.CurrentContext.CancellationToken`.

PR #5463 (merged) added a global cancellation token. It is passed to methods that declare a `CancellationToken` parameter and is
available as `TestContext.CurrentContext.CancellationToken`. It is cancelled when the run is stopped (`StopRun`).

```csharp
[CancelAfter(1000)]
public class OneTimeSetUpHangs
{
    [OneTimeSetUp]
    public async Task BeforeAll()
    {
        CancellationToken token = TestContext.CurrentContext.CancellationToken;
        await Task.Delay(TimeSpan.FromSeconds(10), token);
    }

    [Test]
    public void Test1() => Assert.That(1, Is.EqualTo(1));
}
```

`OneTimeTearDownHangs` does the same in `[OneTimeTearDown]`.

## Run

```sh
dotnet test --logger "console;verbosity=detailed"
```

NUnit 5.1.0-alpha.0.81 (MyGet, main at 467eab2c7), NUnit3TestAdapter 6.3.1, Microsoft.NET.Test.Sdk 18.11.0, net10.0, Windows 11.

## Actual (main, 5.1.0-alpha.0.81)

The token can be cancelled, but `CancelAfter` does not cancel it in one-time methods. Both run the full 10 s and the tests pass:

```
OneTimeSetUp: CanBeCanceled=True
OneTimeSetUp: ran the full 10 s, not cancelled
OneTimeTearDown: CanBeCanceled=True
  Passed Test1 [15 ms]
  Passed Test1 [< 1 ms]
OneTimeTearDown: ran the full 10 s, not cancelled
Test Run Successful.
 Total time: 20.8658 Seconds
```

Declaring the token as a parameter, `BeforeAll(CancellationToken token)`, is not supported for one-time methods:

```
OneTimeSetUp: SetUp and TearDown methods must not have parameters: BeforeAll
```

## Expected

The token is cancelled after 1 s. `OneTimeSetUp` fails with a cancellation, and its child tests are reported as failed with
`OneTimeSetUp:` as the site. `OneTimeTearDown` is reported as a teardown failure. The run takes about 2 s.
