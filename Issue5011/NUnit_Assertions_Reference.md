# NUnit Assertions Reference

This document provides a comprehensive list of all available assertions in NUnit 3.x/4.x. NUnit provides two main assertion models: the modern constraint-based model using `Assert.That()` and the legacy classic model.

## Table of Contents
- [Modern Constraint-Based Assertions](#modern-constraint-based-assertions)
- [Classic Assertions (Legacy)](#classic-assertions-legacy)
- [String Assertions](#string-assertions)
- [Collection Assertions](#collection-assertions)
- [File Assertions](#file-assertions)
- [Exception Assertions](#exception-assertions)
- [Constraint Syntax](#constraint-syntax)

---

## Modern Constraint-Based Assertions

The modern NUnit assertion model uses `Assert.That()` with constraints. This is the recommended approach.

### Basic Syntax
```csharp
Assert.That(actual, constraint);
Assert.That(actual, constraint, message);
Assert.That(actual, constraint, () => message);
```

### Core Assert Methods
- `Assert.That(condition)` - Asserts that a boolean condition is true
- `Assert.That(actual, constraint)` - Asserts that actual value meets constraint
- `Assert.That(() => expression, constraint)` - Asserts that lambda expression meets constraint
- `Assert.That(delegate, constraint)` - Asserts that delegate meets constraint
- `Assert.Multiple(action)` - Groups multiple assertions to run all and report failures together
- `Assert.Pass()` - Marks test as passed and terminates execution
- `Assert.Pass(message)` - Marks test as passed with message
- `Assert.Fail()` - Marks test as failed
- `Assert.Fail(message)` - Marks test as failed with message
- `Assert.Ignore()` - Marks test as ignored
- `Assert.Ignore(message)` - Marks test as ignored with message
- `Assert.Inconclusive()` - Marks test as inconclusive
- `Assert.Inconclusive(message)` - Marks test as inconclusive with message
- `Assert.Warn(message)` - Issues a warning

### Equality Constraints
- `Is.EqualTo(expected)` - Tests for equality
- `Is.Not.EqualTo(expected)` - Tests for inequality
- `Is.SameAs(expected)` - Tests for reference equality
- `Is.Not.SameAs(expected)` - Tests for reference inequality

### Comparison Constraints
- `Is.GreaterThan(expected)` - Tests for greater than
- `Is.GreaterThanOrEqualTo(expected)` - Tests for greater than or equal
- `Is.AtLeast(expected)` - Alias for GreaterThanOrEqualTo
- `Is.LessThan(expected)` - Tests for less than
- `Is.LessThanOrEqualTo(expected)` - Tests for less than or equal
- `Is.AtMost(expected)` - Alias for LessThanOrEqualTo

### Boolean Constraints
- `Is.True` - Tests for true value
- `Is.False` - Tests for false value

### Null Constraints
- `Is.Null` - Tests for null value
- `Is.Not.Null` - Tests for non-null value

### Type Constraints
- `Is.TypeOf<T>()` - Tests for exact type
- `Is.TypeOf(type)` - Tests for exact type
- `Is.InstanceOf<T>()` - Tests for type or derived type
- `Is.InstanceOf(type)` - Tests for type or derived type
- `Is.AssignableFrom<T>()` - Tests if type is assignable from
- `Is.AssignableFrom(type)` - Tests if type is assignable from
- `Is.AssignableTo<T>()` - Tests if type is assignable to
- `Is.AssignableTo(type)` - Tests if type is assignable to

### Range and Pattern Constraints
- `Is.InRange(from, to)` - Tests if value is in range
- `Is.Zero` - Tests for zero value
- `Is.Positive` - Tests for positive value
- `Is.Negative` - Tests for negative value
- `Is.NaN` - Tests for NaN (Not a Number)
- `Is.Odd` - Tests for odd numbers
- `Is.Even` - Tests for even numbers
- `Is.MultipleOf(divisor)` - Tests if number is multiple of divisor

### Collection Constraints
- `Is.Empty` - Tests for empty collection
- `Is.Not.Empty` - Tests for non-empty collection
- `Is.Unique` - Tests that all items in collection are unique
- `Is.Ordered` - Tests that collection is ordered
- `Is.EquivalentTo(expected)` - Tests collections have same items (any order)
- `Is.SubsetOf(expected)` - Tests collection is subset of expected
- `Is.SupersetOf(expected)` - Tests collection is superset of expected
- `Has.Count.EqualTo(expected)` - Tests collection count
- `Has.Length.EqualTo(expected)` - Tests collection/string length
- `Has.Some.EqualTo(expected)` - Tests collection contains item
- `Has.All.EqualTo(expected)` - Tests all items equal expected
- `Has.None.EqualTo(expected)` - Tests no items equal expected
- `Has.Member(expected)` - Tests collection contains member
- `Has.No.Member(expected)` - Tests collection doesn't contain member
- `Contains.Item(expected)` - Tests collection contains item
- `Does.Contain(expected)` - Tests collection contains item
- `Does.Not.Contain(expected)` - Tests collection doesn't contain item

### String Constraints
- `Does.StartWith(expected)` - Tests string starts with
- `Does.Not.StartWith(expected)` - Tests string doesn't start with
- `Does.EndWith(expected)` - Tests string ends with
- `Does.Not.EndWith(expected)` - Tests string doesn't end with
- `Does.Contain(expected)` - Tests string contains substring
- `Does.Not.Contain(expected)` - Tests string doesn't contain substring
- `Does.Match(pattern)` - Tests string matches regex pattern
- `Does.Not.Match(pattern)` - Tests string doesn't match regex pattern

### Property Constraints
- `Has.Property(name).EqualTo(value)` - Tests property value
- `Has.Property(name).Null` - Tests property is null
- `Has.Property(name).Not.Null` - Tests property is not null

### Exception Constraints
- `Throws.Exception` - Tests that exception is thrown
- `Throws.TypeOf<T>()` - Tests specific exception type thrown
- `Throws.InstanceOf<T>()` - Tests exception type or derived thrown
- `Throws.ArgumentException` - Tests ArgumentException thrown
- `Throws.ArgumentNullException` - Tests ArgumentNullException thrown
- `Throws.InvalidOperationException` - Tests InvalidOperationException thrown
- `Throws.Nothing` - Tests that no exception is thrown

### File/Directory Constraints
- `Does.Exist` - Tests file or directory exists
- `Does.Not.Exist` - Tests file or directory doesn't exist

---

## Classic Assertions (Legacy)

The classic assertion model is maintained for backward compatibility. These are available in the `NUnit.Framework.Legacy` namespace.

### Equality Assertions
```csharp
ClassicAssert.AreEqual(expected, actual)
ClassicAssert.AreEqual(expected, actual, message)
ClassicAssert.AreEqual(expected, actual, delta) // For floating point
ClassicAssert.AreNotEqual(expected, actual)
ClassicAssert.AreNotEqual(expected, actual, message)
```

### Identity Assertions
```csharp
ClassicAssert.AreSame(expected, actual)
ClassicAssert.AreSame(expected, actual, message)
ClassicAssert.AreNotSame(expected, actual)
ClassicAssert.AreNotSame(expected, actual, message)
```

### Boolean Assertions
```csharp
ClassicAssert.IsTrue(condition)
ClassicAssert.IsTrue(condition, message)
ClassicAssert.IsFalse(condition)
ClassicAssert.IsFalse(condition, message)
ClassicAssert.True(condition) // Alias for IsTrue
ClassicAssert.False(condition) // Alias for IsFalse
```

### Null Assertions
```csharp
ClassicAssert.IsNull(anObject)
ClassicAssert.IsNull(anObject, message)
ClassicAssert.IsNotNull(anObject)
ClassicAssert.IsNotNull(anObject, message)
ClassicAssert.Null(anObject) // Alias for IsNull
ClassicAssert.NotNull(anObject) // Alias for IsNotNull
```

### Numeric Assertions
```csharp
ClassicAssert.IsNaN(aDouble)
ClassicAssert.IsNaN(aDouble, message)
```

### Collection Membership
```csharp
ClassicAssert.Contains(expected, collection)
ClassicAssert.Contains(expected, collection, message)
```

### Empty/Not Empty
```csharp
ClassicAssert.IsEmpty(collection)
ClassicAssert.IsEmpty(collection, message)
ClassicAssert.IsNotEmpty(collection)
ClassicAssert.IsNotEmpty(collection, message)
```

---

## String Assertions

String-specific assertions available in `StringAssert` class:

```csharp
StringAssert.Contains(expected, actual)
StringAssert.Contains(expected, actual, message)
StringAssert.DoesNotContain(expected, actual)
StringAssert.DoesNotContain(expected, actual, message)
StringAssert.StartsWith(expected, actual)
StringAssert.StartsWith(expected, actual, message)
StringAssert.DoesNotStartWith(expected, actual)
StringAssert.DoesNotStartWith(expected, actual, message)
StringAssert.EndsWith(expected, actual)
StringAssert.EndsWith(expected, actual, message)
StringAssert.DoesNotEndWith(expected, actual)
StringAssert.DoesNotEndWith(expected, actual, message)
StringAssert.AreEqualIgnoringCase(expected, actual)
StringAssert.AreEqualIgnoringCase(expected, actual, message)
StringAssert.AreNotEqualIgnoringCase(expected, actual)
StringAssert.AreNotEqualIgnoringCase(expected, actual, message)
StringAssert.IsMatch(pattern, actual)
StringAssert.IsMatch(pattern, actual, message)
StringAssert.DoesNotMatch(pattern, actual)
StringAssert.DoesNotMatch(pattern, actual, message)
```

---

## Collection Assertions

Collection-specific assertions in `CollectionAssert` class:

```csharp
CollectionAssert.AreEqual(expected, actual)
CollectionAssert.AreEqual(expected, actual, comparer)
CollectionAssert.AreEqual(expected, actual, message)
CollectionAssert.AreNotEqual(expected, actual)
CollectionAssert.AreNotEqual(expected, actual, message)
CollectionAssert.AreEquivalent(expected, actual)
CollectionAssert.AreEquivalent(expected, actual, message)
CollectionAssert.AreNotEquivalent(expected, actual)
CollectionAssert.AreNotEquivalent(expected, actual, message)
CollectionAssert.Contains(collection, actual)
CollectionAssert.Contains(collection, actual, message)
CollectionAssert.DoesNotContain(collection, actual)
CollectionAssert.DoesNotContain(collection, actual, message)
CollectionAssert.AllItemsAreInstancesOfType(collection, expectedType)
CollectionAssert.AllItemsAreInstancesOfType(collection, expectedType, message)
CollectionAssert.AllItemsAreNotNull(collection)
CollectionAssert.AllItemsAreNotNull(collection, message)
CollectionAssert.AllItemsAreUnique(collection)
CollectionAssert.AllItemsAreUnique(collection, message)
CollectionAssert.IsSubsetOf(subset, superset)
CollectionAssert.IsSubsetOf(subset, superset, message)
CollectionAssert.IsNotSubsetOf(subset, superset)
CollectionAssert.IsNotSubsetOf(subset, superset, message)
CollectionAssert.IsOrdered(collection)
CollectionAssert.IsOrdered(collection, message)
CollectionAssert.IsOrdered(collection, comparer)
CollectionAssert.IsOrdered(collection, comparer, message)
```

---

## File Assertions

File-specific assertions in `FileAssert` class:

```csharp
FileAssert.Exists(file)
FileAssert.Exists(file, message)
FileAssert.DoesNotExist(file)
FileAssert.DoesNotExist(file, message)
FileAssert.AreEqual(expected, actual)
FileAssert.AreEqual(expected, actual, message)
FileAssert.AreNotEqual(expected, actual)
FileAssert.AreNotEqual(expected, actual, message)
```

---

## Exception Assertions

### Modern Syntax (Recommended)
```csharp
// Test that exception is thrown
Assert.That(() => code, Throws.Exception);
Assert.That(() => code, Throws.TypeOf<ArgumentException>());
Assert.That(() => code, Throws.InstanceOf<ArgumentException>());

// Test specific exception types
Assert.That(() => code, Throws.ArgumentException);
Assert.That(() => code, Throws.ArgumentNullException);
Assert.That(() => code, Throws.InvalidOperationException);

// Test no exception is thrown
Assert.That(() => code, Throws.Nothing);

// Test exception properties
Assert.That(() => code, Throws.ArgumentException.With.Property("ParamName").EqualTo("param"));
Assert.That(() => code, Throws.TypeOf<ArgumentException>().With.Message.Contains("error"));
```

### Direct Exception Testing
```csharp
// Returns the exception if thrown, null if not
var ex = Assert.Throws<ArgumentException>(() => code);
var ex = Assert.Throws(typeof(ArgumentException), () => code);

// Catch any exception
var ex = Assert.Catch<Exception>(() => code);
var ex = Assert.Catch(() => code);

// Assert no exception is thrown
Assert.DoesNotThrow(() => code);
Assert.DoesNotThrow(() => code, message);
```

### Async Exception Testing
```csharp
// For async methods
Assert.ThrowsAsync<ArgumentException>(async () => await AsyncMethod());
Assert.CatchAsync<Exception>(async () => await AsyncMethod());
Assert.DoesNotThrowAsync(async () => await AsyncMethod());
```

---

## Constraint Syntax

### Combining Constraints
```csharp
// And operator
Assert.That(actual, Is.GreaterThan(0).And.LessThan(100));

// Or operator  
Assert.That(actual, Is.EqualTo("hello").Or.EqualTo("world"));

// Not operator
Assert.That(actual, Is.Not.EqualTo(expected));
Assert.That(actual, Is.Not.Null);
```

### Constraint Modifiers
```csharp
// Case sensitivity
Assert.That(actual, Is.EqualTo(expected).IgnoreCase);
Assert.That(actual, Does.Contain(substring).IgnoreCase);

// Numeric tolerance
Assert.That(actual, Is.EqualTo(expected).Within(0.01));
Assert.That(actual, Is.EqualTo(expected).Within(1).Percent);

// Collection comparison
Assert.That(actual, Is.EqualTo(expected).AsCollection);
Assert.That(actual, Is.EquivalentTo(expected).IgnoreCase);

// Delay/timeout for conditions
Assert.That(() => condition, Is.True.After(1000).MilliSeconds);
Assert.That(() => condition, Is.True.After(1).Seconds);
```

### Custom Constraints
You can create custom constraints by implementing `IResolveConstraint` or inheriting from `Constraint`.

---

## Additional Assertion Helpers

### Assume Class
For assumptions (preconditions that make test inconclusive if not met):
```csharp
Assume.That(condition);
Assume.That(actual, constraint);
```

### TestContext
For test output and metadata:
```csharp
TestContext.WriteLine(message);
TestContext.Out.WriteLine(message);
TestContext.Error.WriteLine(message);
```

---

## Best Practices

1. **Prefer constraint-based syntax** (`Assert.That`) over classic assertions
2. **Use meaningful assertion messages** for better test failure diagnostics
3. **Group related assertions** with `Assert.Multiple` to see all failures at once
4. **Use specific constraints** rather than general ones (e.g., `Is.Empty` vs `Has.Count.EqualTo(0)`)
5. **Consider tolerance** for floating-point comparisons
6. **Use appropriate exception testing patterns** for different scenarios

---

## Examples

### Basic Assertions
```csharp
// Boolean
Assert.That(result, Is.True);
Assert.That(condition, Is.False);

// Equality
Assert.That(actual, Is.EqualTo(expected));
Assert.That(actual, Is.Not.EqualTo(unexpected));

// Null checks
Assert.That(obj, Is.Not.Null);
Assert.That(obj, Is.Null);

// Numeric comparisons
Assert.That(score, Is.GreaterThan(80));
Assert.That(age, Is.InRange(18, 65));
Assert.That(price, Is.EqualTo(19.99).Within(0.01));
```

### Collection Assertions
```csharp
// Collection properties
Assert.That(list, Is.Not.Empty);
Assert.That(list, Has.Count.EqualTo(3));
Assert.That(array, Has.Length.EqualTo(5));

// Collection contents
Assert.That(list, Does.Contain("item"));
Assert.That(list, Has.Some.EqualTo("item"));
Assert.That(list, Has.All.GreaterThan(0));
Assert.That(list, Is.Unique);
Assert.That(list, Is.Ordered);

// Collection comparison
Assert.That(actual, Is.EqualTo(expected));
Assert.That(actual, Is.EquivalentTo(expected));
Assert.That(subset, Is.SubsetOf(superset));
```

### String Assertions
```csharp
// String content
Assert.That(text, Does.StartWith("Hello"));
Assert.That(text, Does.EndWith("world"));
Assert.That(text, Does.Contain("test"));
Assert.That(text, Does.Match(@"\d{3}-\d{3}-\d{4}"));

// Case insensitive
Assert.That(text, Is.EqualTo("HELLO").IgnoreCase);
Assert.That(text, Does.Contain("TEST").IgnoreCase);
```

### Exception Assertions
```csharp
// Exception testing
Assert.That(() => method(), Throws.ArgumentNullException);
Assert.That(() => method(), Throws.Exception.With.Message.Contains("error"));

// Return exception for further testing
var ex = Assert.Throws<InvalidOperationException>(() => method());
Assert.That(ex.Message, Does.Contain("specific error"));

// No exception expected
Assert.That(() => method(), Throws.Nothing);
Assert.DoesNotThrow(() => method());
```

This reference covers all the major assertion capabilities in NUnit. The modern constraint-based approach provides more flexibility and better error messages, making it the recommended choice for new tests.
