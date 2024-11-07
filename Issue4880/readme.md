To get the error, run:

```cmd
dotnet test -- NUnit.Where="test==Issue4880.Tests.Test1\(42,\"abc\"\)"
```

It is the same with or without the escaping of the special characters.
See docs https://docs.nunit.org/articles/nunit/running-tests/Test-Selection-Language.html#simple-expressions  

The last part of sentence "test - The fully qualified test name as assigned by NUnit, e.g. My.Name.Space.TestFixture.TestMethod(5)" shows that it should be possible to use parantheses.
