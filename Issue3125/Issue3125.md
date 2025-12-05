# Issue #3125 - TestCaseData Type Conversion Bug

## Problem Description

When the same `TestCaseData` is used by multiple test methods with different parameter types, NUnit's type conversion logic was mutating the shared argument array. This caused subsequent test methods to receive already-converted values instead of the original values.

### Example

```csharp
class Test
{
    private static readonly TestCaseData[] Cases =
    {
        new TestCaseData("", 0),
        new TestCaseData("", 1)
    };
    
    [TestCaseSource(nameof(Cases))]
    public void TestA(string a, float b)  // Expects float
    {
    }

    [TestCaseSource(nameof(Cases))]
    public void TestB(string a, int b)    // Expects int
    {
    }
}
```

### Expected Behavior
- `TestA` should receive `(string, float)` - the `int` value converted to `float`
- `TestB` should receive `(string, int)` - the original `int` value unchanged

### Actual Behavior
- `TestA` correctly received `(string, float)` 
- `TestB` failed with: `System.ArgumentException: Object of type 'System.Single' cannot be converted to type 'System.Int32'`

The error occurred because:
1. `TestA` converted the shared argument `0` (int) to `0.0f` (float) and stored it back
2. `TestB` then tried to use the already-converted `0.0f` (float) but expected `0` (int)

## Root Cause

The issue was in `NUnitTestCaseBuilder.cs` where the same `TestCaseParameters` object was shared across multiple test methods. When `TypeHelper.ConvertArgumentList()` was called, it modified the shared `Arguments` array in place.

## Solution

The fix involves two key changes:

### 1. Clone TestCaseParameters for Each Test Method

In `NUnitTestCaseBuilder.CheckTestMethodSignature()`, create a fresh clone of the `TestCaseParameters` for each test method:

```csharp
if (parms is not null)
{
    // Create a clone of the parameters to avoid sharing Arguments across multiple test methods
    // This is necessary because the same TestCaseData may be used with multiple test methods that expect different types
    parms = new TestCaseParameters(parms.Arguments)
    {
        ExpectedResult = parms.ExpectedResult,
        HasExpectedResult = parms.HasExpectedResult,
        RunState = parms.RunState,
        Properties = parms.Properties,
        TestName = parms.TestName,
        TypeArgs = parms.TypeArgs,
        OriginalArguments = parms.OriginalArguments,
        ArgDisplayNames = parms.ArgDisplayNames
    };
    testMethod.Parms = parms;
    // ... rest of the code
}
```

### 2. Ensure TypeHelper.ConvertArgumentList Returns a Clone

In `TypeHelper.ConvertArgumentList()`, always work on a cloned array:

```csharp
public static object?[] ConvertArgumentList(object?[] arglist, IParameterInfo[] parameters)
{
    System.Diagnostics.Debug.Assert(arglist.Length <= parameters.Length);

    // Clone the array to avoid modifying the original when the same TestCaseData is used by multiple test methods
    object?[] convertedArgs = new object?[arglist.Length];
    Array.Copy(arglist, convertedArgs, arglist.Length);

    for (int i = 0; i < convertedArgs.Length; i++)
    {
        object? arg = convertedArgs[i];

        if (arg is IConvertible)
        {
            Type argType = arg.GetType();
            Type targetType = parameters[i].ParameterType;
            bool convert = false;

            if (argType != targetType && IsNumeric(argType) && IsNumeric(targetType))
            {
                // ... conversion logic ...
            }

            if (convert)
            {
                convertedArgs[i] = Convert.ChangeType(arg, targetType,
                    System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }

    return convertedArgs;
}
```

## Files Modified

1. `src/NUnitFramework/framework/Internal/Builders/NUnitTestCaseBuilder.cs`
   - Clone `TestCaseParameters` for each test method
   - Use the returned converted array from `ConvertArgumentList`

2. `src/NUnitFramework/framework/Internal/TypeHelper.cs`
   - Clone the argument array before conversion
   - Return the converted clone instead of modifying in place

3. `src/NUnitFramework/tests/Internal/TypeHelperTests.cs`
   - Added regression test `ConvertArgumentList_WithMultipleTestMethodsUsingSameTestCaseData()`

## Test Coverage

Added a test in `TypeHelperTests.cs` that reproduces the issue:

```csharp
[Test]
public void ConvertArgumentList_WithMultipleTestMethodsUsingSameTestCaseData()
{
    // Create test parameters using reflection from real methods
    var methodA = typeof(TestMethods).GetMethod(nameof(TestMethods.TestA));
    var methodB = typeof(TestMethods).GetMethod(nameof(TestMethods.TestB));

    var paramA = methodA!.GetParameters()
        .Select(p => new RuntimeParameterInfo(p))
        .ToArray();
    var paramB = methodB!.GetParameters()
        .Select(p => new RuntimeParameterInfo(p))
        .ToArray();

    // Original arguments from TestCaseData
    object?[] originalArgs = new object?[] { string.Empty, 0 };

    // Convert for TestA (should convert int to float)
    object?[] argsForTestA = (object?[])originalArgs.Clone();
    TypeHelper.ConvertArgumentList(argsForTestA, paramA);

    // Verify TestA received the float conversion
    Assert.That(argsForTestA[1], Is.TypeOf<float>());

    // Convert for TestB using fresh original args (should NOT convert, int is already int)
    object?[] argsForTestB = (object?[])originalArgs.Clone();
    TypeHelper.ConvertArgumentList(argsForTestB, paramB);

    // Verify TestB receives int (not float from previous conversion)
    Assert.That(argsForTestB[1], Is.TypeOf<int>());
}
```

## Impact

This fix ensures that:
- Each test method gets its own independent copy of the test case arguments
- Type conversions only affect the specific test method they're intended for
- The original `TestCaseData` remains immutable and can be safely reused across multiple test methods
- No breaking changes to public APIs

## Related Issues

This bug was discovered when using `TestCaseSource` with the same data for multiple test methods that expected different numeric types for their parameters.
