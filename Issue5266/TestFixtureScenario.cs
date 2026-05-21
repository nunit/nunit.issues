namespace Issue5266.TestFixtureScenario;

/// <summary>
/// Scenario 2: TestFixture with failing OneTimeSetUp.
/// The fixture's OneTimeSetUp fails, affecting all tests in that fixture.
/// The full error is repeated for each test (Test6-Test8).
/// </summary>
[TestFixture]
public class FixtureWithFailingOneTimeSetUp
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        throw new ArgumentException(
            "Invalid configuration: The 'ConnectionString' setting is missing from appsettings.json. " +
            "Please ensure the configuration file exists and contains the required settings.");
    }

    [Test]
    public void Test6() => Assert.Pass();

    [Test]
    public void Test7() => Assert.Pass();

    [Test]
    public void Test8() => Assert.Pass();
}

/// <summary>
/// A working fixture for comparison - this test passes.
/// </summary>
[TestFixture]
public class WorkingFixture
{
    [Test]
    public void PassingTest() => Assert.Pass();
}
