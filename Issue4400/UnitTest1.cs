namespace Issue4400;

public class Tests
{
    

    [TestCase("2023-01-01 13:00:10", "2023-01-01 13:00:00", 10)] // succeeds
    [TestCase(null, "2023-01-01 13:00:00", 10)] // throws
    [TestCase( "2023-01-01 13:00:00",null, 10)] // throws
    [TestCase(null, null, 10)] // succeeds
    public void DatesShouldBeTheSame(DateTime? value, DateTime? expectedDateTime, int toleranceInSeconds)
    {
        Assert.That(value, Is.EqualTo(expectedDateTime).Within(toleranceInSeconds).Seconds);
    }

    [TestCase("2023-01-01 13:00:00", "2023-01-01 13:00:00")] // succeeds
    [TestCase(null, "2023-01-01 13:00:00")] // throws
    [TestCase("2023-01-01 13:00:00", null)] // throws
    [TestCase(null, null)] // succeeds
    public void StringsShouldBeTheSame(string? value, string? expectedValue)
    {
        Assert.That(value, Is.EqualTo(expectedValue));
    }


    [TestCase(42.0, 42.05,0.1)] // succeeds
    [TestCase(null, 42.05,0.1)] // throws
    [TestCase(42.05, null,0.1)] // throws
    [TestCase(null, null,0.1)] // succeeds
    public void DoublesShouldBeTheSame(double? value, double? expectedValue,double tolerance)
    {
        Assert.That(value, Is.EqualTo(expectedValue).Within(tolerance));
    }


}