# Issue 5453 - Breaking nullability change in `Matches<T>` (5.0.0)

https://github.com/nunit/nunit/issues/5453

Cause: `ConstraintExpression.Matches<TActual>(Predicate<TActual>)` became
`Matches<TActual>(Predicate<TActual?>)` in 5.0.0 (commit ba0a1f5c3, PR #5218 "Allow custom comparer null self").
`PredicateConstraint<T>` changed the same way.

Run:

    dotnet test -p:NUnitVersion=4.6.1   # builds, passes
    dotnet test -p:NUnitVersion=5.0.0   # CS8602 as error

Note: `HasOneMatches` is the issue's code verbatim. On 4.6.1 it compiles but fails at runtime:
both "hello" and "world" have length 5, and `Has.One` means exactly one.

## Manfred's comment (verified)

`ManfredNullItemTests` shows why `Predicate<T?>` is correct. `TActual` is not tied to the element type,
so `Matches<string>` on a `List<string?>` gets `null` at runtime.
On 4.6.1, `x => x.Length == 5` compiles without warning and throws `NullReferenceException`.
On 5.0.0 the compiler reports CS8602.

User fix on 5.0.0: `word => word!.Length == 5` or `word => word?.Length == 5`.
