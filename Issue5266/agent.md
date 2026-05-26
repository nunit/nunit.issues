# Issue 5266 - Repro Project

## Package Versions

When setting up a repro test project, use these latest package versions:

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.5.1" />
<PackageReference Include="NUnit" Version="4.6.1" />
<PackageReference Include="NUnit3TestAdapter" Version="6.2.0" />
<PackageReference Include="NUnit.Analyzers" Version="4.13.0">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
```

## Project Setup

- Target Framework: net10.0
- Use standard test project format (not NUnitLite)
- Run tests with `dotnet test`

## Issue Description

When a SetupFixture or TestFixture's `OneTimeSetUp` fails, NUnit repeats the full exception message and stack trace for every affected test case, causing noisy output in large test suites.

## Fix Location

The fix is implemented in:
- `src/NUnitFramework/framework/Internal/Execution/CompositeWorkItem.cs` - `SkipChildren()` method

Child tests now show `"OneTimeSetUp failed"` instead of repeating the full stack trace.
