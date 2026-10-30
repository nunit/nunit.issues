# Issue 5523: a warning in OneTimeTearDown replaces the failure of a test method

https://github.com/nunit/nunit/issues/5523, fixed by https://github.com/nunit/nunit/pull/5527.

The fixture class has one test method that fails, and a `OneTimeTearDown` that calls `Assert.Warn`.
The fixture result should stay `Failed` (site `Child`), because a test method failed. With NUnit 5.0.0 it becomes `Warning`.

The fixture result is only visible in the NUnit XML (`<test-suite type="TestFixture">`), and in the overall result of the run.

| Folder       | What it is                                                                                     |
|--------------|------------------------------------------------------------------------------------------------|
| `Tests`      | Ordinary test project, run with `dotnet test`. Shows the failing test method only.             |
| `NUnitLite`  | Same fixture class run with NUnitLite. Reads `TestResult.xml` and prints the fixture result.   |

## NUnitLite: run

```
cd NUnitLite
dotnet run
```

### Actual (NUnit 5.0.0)

```
Overall result: Warning
Fixture result: result="Warning" site="(none)"
Fixture message: One or more child tests had errors
ottd warn
BUG (issue 5523): the warning from OneTimeTearDown replaced the failure of the test method.
```

### Expected (with PR 5527)

```
Overall result: Failed
Fixture result: result="Failed" site="Child"
Fixture message: One or more child tests had errors
ottd warn
OK: the failure of the test method is kept.
```

The message is the same in both. Only the result state changes.
