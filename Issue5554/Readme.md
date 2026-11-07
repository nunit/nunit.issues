# Issue 5554: async tests are Invalid under NativeAOT

https://github.com/nunit/nunit/issues/5554, fix in https://github.com/nunit/nunit/pull/5555.

Under NativeAOT, every `async Task` test is reported as Invalid, and `TestContext.CurrentContext` on a thread
without a test context throws. Both pass under JIT. NUnit looks members up by name in places the trimmer
can't see, so they are trimmed away:

- `AwaitShapeInfo.TryCreate` calls `GetInterface("System.Runtime.CompilerServices.INotifyCompletion")`, which returns null.
- `AdhocContext` calls `new MethodWrapper(GetType(), nameof(AdhocTestMethod))`, and `AdhocTestMethod` is gone.

| Folder      | What it is                                                                                          |
|-------------|-----------------------------------------------------------------------------------------------------|
| `Tests`     | Ordinary test project, run with `dotnet test`. The three tests pass under JIT.                      |
| `NativeAot` | The same test file in an NUnitLite app published with `PublishAot=true`. Two of the three fail.     |
| `NoNUnit`   | The `GetInterface` lookup NUnit does, without NUnit. Returns the interface under JIT, null under AOT. |

NUnit/NUnitLite 5.0.0, .NET SDK 10.0.403, macOS arm64. Use your own RID (`linux-x64`, `win-x64`, ...).

## Tests

```
cd Tests
dotnet test
```

```
Passed!  - Failed:     0, Passed:     3, Skipped:     0, Total:     3
```

## NativeAot: run

```
cd NativeAot
dotnet publish -c Release -r osx-arm64 -o out
./out/Issue5554.NativeAot --noresult
```

The test assembly is rooted with `<TrimmerRootAssembly>`, the usual workaround from [nunit/nunit#5358](https://github.com/nunit/nunit/issues/5358). Without it no tests are found.

### Actual (NUnit 5.0.0), exit code 2

```
1) Invalid : Issue5554.AotAsyncTests.AsyncTest
Failure building TestMethod
System.InvalidOperationException : Cannot determine result type for: System.Threading.Tasks.Task
   at NUnit.Framework.Internal.AwaitAdapter.GetResultType(Type) + 0xb4

2) Failed : Issue5554.AotAsyncTests.ContextOnAnotherThread
  Method NUnit.Framework.Internal.TestExecutionContext+AdhocContext.AdhocTestMethod not found

Test Count: 3, Passed: 1, Failed: 2, Warnings: 0, Inconclusive: 0, Skipped: 0
  Failed Tests - Failures: 1, Errors: 0, Invalid: 1
```

`ContextOnAnotherThread` catches the exception on its thread. Uncaught, it crashes the whole run.

### Expected (with PR 5555)

```
dotnet publish -c Release -r osx-arm64 -o out-fix -p:NUnitSrc=<nunit checkout>
./out-fix/Issue5554.NativeAot --noresult
```

```
Overall result: Passed
Test Count: 3, Passed: 3, Failed: 0, Warnings: 0, Inconclusive: 0, Skipped: 0
```

## NoNUnit: run

```
cd NoNUnit
dotnet run
dotnet publish -c Release -r osx-arm64 -o out && ./out/Issue5554.NoNUnit
```

JIT:

```
awaiter:     System.Runtime.CompilerServices.TaskAwaiter
interface:   System.Runtime.CompilerServices.INotifyCompletion
OnCompleted: Void OnCompleted(System.Action)
```

NativeAOT (the publish also warns IL2075 on the `GetInterface` call):

```
awaiter:     System.Runtime.CompilerServices.TaskAwaiter
interface:   null
OnCompleted: null
```
