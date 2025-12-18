# Test Report

## Summary

- Regression tests: total 1, success 0, fail 1
- Open issues: total 0, success 0, fail 0

## What we are testing

Package versions under test:

- NUnit: 4.4.0
- NUnit.Analyzers: 4.11.2
- NUnit3TestAdapter: 6.0.0

## Regression tests (closed issues)

- Total: 1, Success: 0, Fail: 1

| Issue | Test | Conclusion |
| --- | --- | --- |
| ❗ #1056 | fail | Failure: Regression failure. |

### Closed failures (details)

#### Issue #1056: ITestCaseData does not show test with Visual Studio Test Adapter 3.0.7

**Link**: [#1056](https://github.com/nunit/nunit/issues/1056)

**Labels**: is:bug, closed:duplicate, pri:normal

**Conclusion**: Failure: Regression failure.

**Details**:

```
Determining projects to restore...
  Restored C:\repos\nunit\nunit.issues\Issue1056\Issue1056.csproj (in 829 ms).
C:\repos\nunit\nunit.issues\Issue1056\UnitTest1.cs(81,19): warning CS8618: Non-nullable property 'Name' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the property as nullable. [C:\repos\nunit\nunit.issues\Issue1056\Issue1056.csproj]
C:\repos\nunit\nunit.issues\Issue1056\UnitTest1.cs(99,12): warning CS8618: Non-nullable property 'Properties' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the property as nullable. [C:\repos\nunit\nunit.issues\Issue1056\Issue1056.csproj]
  Issue1056 -> C:\repos\nunit\nunit.issues\Issue1056\bin\Debug\net10.0\Issue1056.dll
Test run for C:\repos\nunit\nunit.issues\Issue1056\bin\Debug\net10.0\Issue1056.dll (.NETCoreApp,Version=v10.0)
VSTest version 18.0.1 (x64)

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
  Failed TestTheTruth [3 ms]
  Error Message:
   Failure building Test
System.InvalidCastException : Unable to cast object of type 'Issue1056.ItemTestCaseData' to type 'NUnit.Framework.Internal.TestCaseParameters'.
  Stack Trace:
     at NUnit.Framework.TestCaseSourceAttribute.BuildFrom(IMethodInfo method, Test suite)+MoveNext()
   at NUnit.Framework.Internal.Builders.DefaultTestCaseBuilder.BuildFrom(IMethodInfo method, Test parentSuite)


Failed!  - Failed:     1, Passed:     0, Skipped:     0, Total:     1, Duration: 3 ms - Issue1056.dll (net10.0)
```

