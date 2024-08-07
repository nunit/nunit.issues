# Introduce HookExtension to support high level tests

Demo test for [Issue 4744](https://github.com/nunit/nunit/issues/4744) and the related draft [pull request](https://github.com/nunit/nunit/pull/4745).

The test is using NUnitLite and therefore you get an executable for test execution. At the end, a log is written to the console in order to understand the interaction between the test and HookExtension using a small web store example.

**Hint:** due to this [bug](https://docs.myget.org/docs/how-to/package-not-found-during-package-restore) you may not be able to compile the demo after checking out the code.

Workaround:
- use ```dotnet build``` for the first compilation
- Visual Studio: before compiling add the MyGet feed (https://www.myget.org/F/nunit_issues/api/v3/index.json) to the package manager configuration

## Reading

Demo.cs contains everything and can be read from top to bottom.
