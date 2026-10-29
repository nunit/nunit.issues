# `Assert.Catch(Action, string, params object[])` fails for derived exceptions

`Assert.Catch` is meant to pass for any exception. In NUnit 5.0.0 the overload that takes a failure message only passes if the code throws exactly `System.Exception`. Almost every real exception is a subclass, such as `ArgumentException`, so adding a message to a working `Assert.Catch` can make it fail. Without the message, or on NUnit 3 and 4, the same code passes. The [docs](https://docs.nunit.org/articles/nunit/writing-tests/assertions/classic-assertions/Assert.Catch.html) say the untyped `Catch` overloads catch any exception. The cause is a one-word slip: this overload calls `Throws<Exception>` (exact type) instead of `Catch<Exception>` (type or subclass).

```csharp
// The string is the assert's failure message, printed only on failure; it is not compared with the exception's message.
[Test] public void NoMessage() => Assert.Catch(() => throw new ArgumentException());                                  // passes
[Test] public void WithMessage() => Assert.Catch(() => throw new ArgumentException(), "should throw");                // fails, but should pass
[Test] public void WithMessage_ExactException() => Assert.Catch(() => throw new Exception(), "should throw");         // passes (control)
```

## Run

```sh
dotnet test --logger "console;verbosity=normal"
```

NUnit 5.0.0, NUnit3TestAdapter 6.2.0, Microsoft.NET.Test.Sdk 18.0.1, .NET SDK 10.0.103, macOS arm64.

## Actual (NUnit 5.0.0)

```
  Passed NoMessage [3 ms]
  Failed WithMessage [13 ms]
  Error Message:
     should throw
Assert.That(caughtException, expression)
  Expected: <System.Exception>
  But was:  <System.ArgumentException: Value does not fall within the expected range.
   at Repro.CatchTests.<>c.<WithMessage>b__1_0() in .../01-assert-catch-message/CatchTests.cs:line 16
   at NUnit.Framework.Assert.Throws[TExpected](IResolveConstraint expression, Action code, String message, Object[] args)>

  Passed WithMessage_ExactException [< 1 ms]

Failed!  - Failed:     1, Passed:     2, Skipped:     0, Total:     3
```

`should throw` is only the failure text; the assert fails on the exception type.

## Expected

All three tests pass: `WithMessage` catches the `ArgumentException` like `NoMessage` does.

## Where it regressed

NUnit 3.14 and 4.x implemented this overload as `Throws(new InstanceOfTypeConstraint(typeof(Exception)), code, message, args)`, which accepts subclasses. It was still that way after #5196 (`dba634d`, delegates replaced with `Action`) and became `Throws<Exception>` in #5270 ("Release/5.x", `82fd5c4`).

`CatchAsync(Func<Task>, string, ...)` on `main` correctly calls `CatchAsync<Exception>(asyncCode, message, args)`, so only the synchronous overload is affected.

Workaround until it is fixed: pass the type, `Assert.Catch<Exception>(code, "should throw")` or `Assert.Catch(typeof(Exception), code, "should throw")`.

Checked by running, not only by reading source:

| Call | NUnit 4.5.1 | NUnit 5.0.0 |
|---|---|---|
| `Assert.Catch(() => throw new ArgumentException())` | Passed | Passed |
| `Assert.Catch(() => throw new ArgumentException(), "should throw")` | Passed | **Failed** |
| `Assert.Catch(() => throw new ArgumentException("same"), "same")` | Passed | **Failed** |
| `Assert.Catch<Exception>(() => throw new ArgumentException(), "should throw")` | Passed | Passed |
| `Assert.Catch(typeof(Exception), () => throw new ArgumentException(), "should throw")` | Passed | Passed |

The third row shows the failure has nothing to do with the message text: it fails even when the failure message equals the exception message.

## Suggested fix

`src/NUnitFramework/framework/Assert.Exceptions.System.cs`, line 124 (still on `main` at 398eae7):

```csharp
public static Exception? Catch(Action code, string message, params object?[]? args)
{
    return Throws<Exception>(code, message, args);   // before
    return Catch<Exception>(code, message, args);    // after
}
```
