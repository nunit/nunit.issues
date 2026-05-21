namespace Issue5266.SetUpFixtureScenario;

/// <summary>
/// Scenario 1: SetUpFixture (namespace-level) failure.
/// The SetUpFixture fails, affecting all tests in this namespace.
/// The full error is repeated for each test (Test1-Test5).
/// </summary>
[SetUpFixture]
public class FailingSetupFixture
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
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
