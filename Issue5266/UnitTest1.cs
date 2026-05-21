namespace Issue5266;

/// <summary>
/// Repro for Issue 5266: SetupFixture failure repeats full exception for every affected test.
///
/// Run with: dotnet test --logger "console;verbosity=detailed"
///
/// Expected: The full exception details should appear once for the SetupFixture.
/// Actual: The full exception and stack trace is repeated for every test case.
/// </summary>
[SetUpFixture]
public class FailingSetupFixture
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Simulate a setup failure with a detailed exception
        throw new InvalidOperationException(
            "Database connection failed: Unable to connect to server 'localhost:5432'. " +
            "The server was not found or was not accessible.");
    }
}

public class TestClass1
{
    [Test]
    public void Test1() => Assert.Pass();

    [Test]
    public void Test2() => Assert.Pass();

    [Test]
    public void Test3() => Assert.Pass();
}

public class TestClass2
{
    [Test]
    public void Test4() => Assert.Pass();

    [Test]
    public void Test5() => Assert.Pass();
}
