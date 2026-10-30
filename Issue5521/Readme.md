# `Reflect.InvokeMethod` does not unwrap `TargetInvocationException` when stack traces lack method names

On runtimes whose stack traces don't include method names, such as .NET NativeAOT with `StackTraceSupport=false` and possibly other AOT or WebAssembly runtimes that leave out stack trace data, a test that calls `Assert.Pass()` is reported as an Error, and a failing test gets an extra Error on top of its failure. The same tests pass everywhere else.

NUnit runs test methods through reflection, and reflection wraps anything the method throws in a `TargetInvocationException`. NUnit is supposed to remove that wrapper, but since nunit/nunit#4885 (the fix for #4718) it only does so when the wrapper's stack trace mentions `NUnit.Framework`. Without method names that check never matches, so the wrapper stays, and NUnit sees an unexpected exception instead of the `SuccessException` or `AssertionException` inside it.

This folder has two parts:

1. `Repro.csproj` (`dotnet test`): calls `ExceptionHelper.Unwrap` directly on a `TargetInvocationException` built with no stack trace, plus a control showing `Reflect.InvokeMethod` unwraps correctly when stack traces are available.
2. `aot/AotRepro.csproj`: an NUnitLite runner published with NativeAOT and `StackTraceSupport=false`, running real `Assert.Pass()` and failing tests end to end. A control publish with `StackTraceSupport=true` shows the same binary passing.

NUnit 5.0.0, NUnitLite 5.0.0, NUnit3TestAdapter 6.2.0, Microsoft.NET.Test.Sdk 18.0.1, .NET SDK 10.0.103 (ILCompiler 10.0.3), macOS arm64.

## Part 1: `Unwrap` on a wrapper without a stack trace

```sh
dotnet test --logger "console;verbosity=normal"
```

### Actual (NUnit 5.0.0)

```
  Passed ReflectInvokeMethod_WithStackTraces_RethrowsInnerException [6 ms]
  Failed Unwrap_TargetInvocationExceptionWithoutStackTrace_ReturnsInnerException [11 ms]
  Error Message:
     Assert.That(noTrace.Unwrap(), Is.TypeOf<SuccessException>())
  Expected: <NUnit.Framework.SuccessException>
  But was:  <System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation.
 ---> NUnit.Framework.SuccessException: passed
   --- End of inner exception stack trace --->

Test Run Failed.
Total tests: 2
     Passed: 1
     Failed: 1
```

### Expected

Both pass: `Unwrap` (or at least `Reflect.InvokeMethod`) returns the `SuccessException`.

## Part 2: NativeAOT with `StackTraceSupport=false`

```sh
cd aot
dotnet publish -c Release -r osx-arm64 -o out
./out/AotRepro --noresult
```

(Use your own RID, e.g. `linux-x64`/`win-x64`. NativeAOT needs the platform toolchain: Xcode CLT on macOS.)

The tests in `aot/Program.cs`: `PassingTest` (`Assert.Pass()`), `FailingTest` (`Assert.That(1, Is.EqualTo(2))`), `PlainTest` (passes without throwing).

### Actual (NUnit 5.0.0), exit code 2

`at AotRepro!<BaseAddress>+0x...` frame lines removed for brevity; that is all the stack trace contains on this runtime.

```
Errors, Failures and Warnings

1-1) Failed : AotTests.FailingTest
  Assert.That(1, Is.EqualTo(2))
  Expected: 2
  But was:  1

1-2) Error : AotTests.FailingTest
System.Reflection.TargetInvocationException : Exception has been thrown by the target of an invocation.
  ----> NUnit.Framework.AssertionException :   Assert.That(1, Is.EqualTo(2))
  Expected: 2
  But was:  1

  ResultState: Failed
--AssertionException

2) Error : AotTests.PassingTest
System.Reflection.TargetInvocationException : Exception has been thrown by the target of an invocation.
  ----> NUnit.Framework.SuccessException : No message provided
  ResultState: Passed
--SuccessException

Test Run Summary
  Overall result: Failed
  Test Count: 3, Passed: 1, Failed: 2, Warnings: 0, Inconclusive: 0, Skipped: 0
    Failed Tests - Failures: 0, Errors: 2, Invalid: 0
```

### Control: same code, `StackTraceSupport=true`

```sh
dotnet publish -c Release -r osx-arm64 -p:StackTraceSupport=true -o out-trace
./out-trace/AotRepro --noresult
```

```
1) Failed : AotTests.FailingTest
  Assert.That(1, Is.EqualTo(2))
  Expected: 2
  But was:  1
   at AotRepro!<BaseAddress>+0x64c254

Test Run Summary
  Overall result: Failed
  Test Count: 3, Passed: 2, Failed: 1, Warnings: 0, Inconclusive: 0, Skipped: 0
    Failed Tests - Failures: 1, Errors: 0, Invalid: 0
```

### Expected (with `StackTraceSupport=false`)

The same as the control: `PassingTest` passes and `FailingTest` is a plain Failure with no wrapper Error.

## Where it came from

The stack-trace condition in `Unwrap` was added by #4885 (merged 2024-11-20, fixing #4718), first released in NUnit 4.3.0. Part 1 of this repro passes on NUnit 4.1.0 and fails on 4.5.1 and 5.0.0. #4515 (2023) only introduced the unwrapping.

The [Assert.Pass docs](https://docs.nunit.org/articles/nunit/writing-tests/assertions/special-assertions/Assert.Pass.html) say it ends the test "recording it as successful", which is what fails here.

## Suggested fix

`src/NUnitFramework/framework/Internal/Reflect.cs`, line 418 (still on `main` at 398eae7; the stack-trace check is in `Internal/ExceptionHelper.cs`, line 317). The caught `TargetInvocationException` always comes from NUnit's own `Invoke` call, so unwrap exactly one level without checking the stack trace:

```csharp
catch (TargetInvocationException e)
{
    throw new NUnitException("Rethrown", e.InnerException ?? e);
}
```

A `TargetInvocationException` thrown by user code is still kept, as #4718 wanted. Other `Unwrap` callers are unchanged.

## Fix check

Done against an NUnit source checkout at `main` 5aaf36c (#5519, which sets the result before `Assert.Pass` throws, is included and does not fix this). The `aot/Program.cs` tests were built with `ProjectReference`s to `nunit.framework.csproj` and `nunitlite.csproj`, NativeAOT, `StackTraceSupport=false`. Two local build tweaks, unrelated to the fix: the `Microsoft.CodeAnalysis.CSharp.CodeStyle` analyzer reference was dropped (it needs a newer compiler than SDK 10.0.103), and `dotnet` ran from outside the checkout so its `global.json` (SDK 10.0.403) didn't apply.

Unpatched `main`: same as NUnit 5.0.0 above (`Errors: 2`, `PassingTest` is an Error).

With the one-line change in `Reflect.InvokeMethod`:

```
1) Failed : AotTests.FailingTest
  Assert.That(1, Is.EqualTo(2))
  Expected: 2
  But was:  1

Test Count: 3, Passed: 2, Failed: 1, Warnings: 0, Inconclusive: 0, Skipped: 0
  Failed Tests - Failures: 1, Errors: 0, Invalid: 0
```

NUnit's own tests with the change, net10.0: `Passed! - Failed: 0, Passed: 6645, Skipped: 4, Total: 6649`. This includes `UnexpectedExceptionTests.TargetInvocationExceptionInUserCodeIsPassedThrough`, the #4718 case.
