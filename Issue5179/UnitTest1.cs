namespace Issue5179;
public class Tests
{
    [TestCaseSource(nameof(MyTestData))]
    public void PassDataFromNUnit_TestCaseSource(params object?[]? data)
    {
        // Assert
        Assert.That(data, Is.Null);
    }

    [Test]
    public void PassDataDirectly()
    {
        // Assert
        PassDataFromNUnit_TestCaseSource(null);
    }

    public static object?[] MyTestData() => [null];
}
