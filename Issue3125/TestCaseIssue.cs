namespace Issue3125;

public class TestCaseIssue2_3125
{

    [TestCase(1.2d)]
    [TestCase(1.4d)]
    [TestCase(1.5d)]
    [TestCase(1.9d)]
    public void TestConversionInt(int actual)
    {
        Assert.That(actual, Is.EqualTo(1));
    }



    [TestCase(1.0f)]
    public void TestConversionDouble(double actual)
    {
        Assert.That(actual, Is.EqualTo(1));
    }

}
