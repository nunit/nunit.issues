namespace TestProject1;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(Data))]
    public void Test1(object data)
    {
        if (data is byte[])
            Assert.Pass();
        else 
            Assert.Fail();
    }

    private static readonly List<object> Data =
    [
        new byte[] { 1, 2, 3 },
    ];
}