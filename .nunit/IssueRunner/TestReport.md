# Test Report

## Summary

- Regression tests: total 1, success 0, fail 1
- Open issues: total 0, success 0, fail 0

## What we are testing

- Repository: https://github.com/nunit/nunit

Package versions under test:

- NUnit: 4.0.0-beta.1
- NUnit.Analyzers: 3.8.0
- NUnit3TestAdapter: 4.5.0

## Regression tests (closed issues)

- Total: 1, Success: 0, Fail: 1

| Issue | Title | Test | Conclusion |
| --- | --- | --- | --- |
| ❗ [#4281](https://github.com/nunit/nunit/issues/4281) | Throws and Delayed (.After) Constraints do not cooperate, resulting in incorrectly failing test | fail | Failure: Regression failure. |

### Closed failures (details)

#### Issue #4281: Throws and Delayed (.After) Constraints do not cooperate, resulting in incorrectly failing test

**Link**: [#4281](https://github.com/nunit/nunit/issues/4281)

**Labels**: is:bug, closed:done, pri:normal

**Conclusion**: Failure: Regression failure.

**Details**:

```
Determining projects to restore...
  Restored C:\repos\nunit\nunit.issues\Issue4281\Issue4281.csproj (in 515 ms).
C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs(30,9): warning NUnit1033: The Write methods are wrappers on TestContext.Out (https://github.com/nunit/nunit.analyzers/tree/master/documentation/NUnit1033.md) [C:\repos\nunit\nunit.issues\Issue4281\Issue4281.csproj]
C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs(34,13): warning NUnit1033: The Write methods are wrappers on TestContext.Out (https://github.com/nunit/nunit.analyzers/tree/master/documentation/NUnit1033.md) [C:\repos\nunit\nunit.issues\Issue4281\Issue4281.csproj]
C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs(35,13): warning NUnit1033: The Write methods are wrappers on TestContext.Out (https://github.com/nunit/nunit.analyzers/tree/master/documentation/NUnit1033.md) [C:\repos\nunit\nunit.issues\Issue4281\Issue4281.csproj]
C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs(68,9): warning NUnit1033: The Write methods are wrappers on TestContext.Out (https://github.com/nunit/nunit.analyzers/tree/master/documentation/NUnit1033.md) [C:\repos\nunit\nunit.issues\Issue4281\Issue4281.csproj]
C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs(69,9): warning NUnit1033: The Write methods are wrappers on TestContext.Out (https://github.com/nunit/nunit.analyzers/tree/master/documentation/NUnit1033.md) [C:\repos\nunit\nunit.issues\Issue4281\Issue4281.csproj]
C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs(70,9): warning NUnit1033: The Write methods are wrappers on TestContext.Out (https://github.com/nunit/nunit.analyzers/tree/master/documentation/NUnit1033.md) [C:\repos\nunit\nunit.issues\Issue4281\Issue4281.csproj]
  Issue4281 -> C:\repos\nunit\nunit.issues\Issue4281\bin\Debug\net10.0\Issue4281.dll
Test run for C:\repos\nunit\nunit.issues\Issue4281\bin\Debug\net10.0\Issue4281.dll (.NETCoreApp,Version=v10.0)
VSTest version 18.0.1 (x64)

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
  Failed TestDelayed [2 s]
  Error Message:
     Assert.That(()=>Count(), Is.GreaterThan(10).After(2).Seconds.PollEvery(500))
  After 2 seconds delay
  Expected: greater than 10
  But was:  5

  Stack Trace:
     at Issue4281.Tests.TestDelayed() in C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs:line 55

1)    at Issue4281.Tests.TestDelayed() in C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs:line 55


  Standard Output Messages:
 RunTime 00:00:00.51
 TestDelayed: 0
 ------------------
 RunTime 00:00:01.02
 TestDelayed: 1
 ------------------
 RunTime 00:00:01.53
 TestDelayed: 2
 ------------------
 RunTime 00:00:02.01
 TestDelayed: 3
 ------------------
 RunTime 00:00:02.01
 TestDelayed: 4
 ------------------


  Failed TestDelayedWithoutPollEvery [2 s]
  Error Message:
     Assert.That(() => Count(), Is.GreaterThan(10).After(2000,500))
  After 2000 milliseconds delay
  Expected: greater than 10
  But was:  5

  Stack Trace:
     at Issue4281.Tests.TestDelayedWithoutPollEvery() in C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs:line 62

1)    at Issue4281.Tests.TestDelayedWithoutPollEvery() in C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs:line 62


  Standard Output Messages:
 RunTime 00:00:00.50
 TestDelayed: 0
 ------------------
 RunTime 00:00:01.01
 TestDelayed: 1
 ------------------
 RunTime 00:00:01.52
 TestDelayed: 2
 ------------------
 RunTime 00:00:02.00
 TestDelayed: 3
 ------------------
 RunTime 00:00:02.00
 TestDelayed: 4
 ------------------


  Failed TestThrows [17 ms]
  Error Message:
     Assert.That(()=>ThrowingMethod(), Throws.TypeOf<ArgumentException>())
  Expected: <System.ArgumentException>
  But was:  no exception thrown

  Stack Trace:
     at Issue4281.Tests.TestThrows() in C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs:line 47

1)    at Issue4281.Tests.TestThrows() in C:\repos\nunit\nunit.issues\Issue4281\UnitTest1.cs:line 47


  Standard Output Messages:
 RunTime 00:00:00.00
 ThrowingMethod: 0
 ------------------



Failed!  - Failed:     3, Passed:     1, Skipped:     0, Total:     4, Duration: 7 s - Issue4281.dll (net10.0)
```

