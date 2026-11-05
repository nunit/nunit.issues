# Issue 5561: catching the exception from `Assert.Pass` leaves the result `Inconclusive`

https://github.com/nunit/nunit/issues/5561, fixed by https://github.com/nunit/nunit/pull/5519 (issue 5517).

The test calls `Assert.Pass("pass message")`, catches the `SuccessException`, and reads `TestContext.CurrentContext.Result`.
The result should be `Passed` with the message. With NUnit 5.0.0 it is still the initial `Inconclusive` with no message,
because `Assert.Pass` only throws. The result is set later, when the runner handles the exception, and that never happens
when the test catches it.

```csharp
try
{
    Assert.Pass("pass message");
}
catch (SuccessException)
{
}

var result = TestContext.CurrentContext.Result;
Assert.That(result.Outcome.Status, Is.EqualTo(TestStatus.Passed));
```

## Run

```sh
dotnet test --logger "console;verbosity=normal"
```

NUnit 5.0.0, NUnit3TestAdapter 6.3.1, Microsoft.NET.Test.Sdk 18.11.0, net10.0, Windows 11.

## Actual (NUnit 5.0.0)

```
Outcome: Inconclusive, Message: ''
  Expected: Passed
  But was:  Inconclusive
```

## Expected (with PR 5519)

```
Outcome: Passed, Message: 'pass message'
```

The test passes. Checked by running the same test against a build of `main`.
