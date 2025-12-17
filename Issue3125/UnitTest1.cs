namespace Issue3125;

class Test
{
    private static readonly TestCaseData[] Cases =
    {
         new TestCaseData("", 0),
         new TestCaseData("", 1)
      };

    [TestCaseSource(nameof(Cases))]
    public void TestA(string a, float b)
    {
        int x = 42;
        float y = 42;
        float z = x;
        double w = z;
        short s = 42;
        short s2 = 42000;
        short s3 = x;
    }

    [TestCase("30.3.2025")]
    [TestCase("30/3/2025")]
    [TestCase("3/30/2025")]
    [TestCase("2025-10-10")]
    public void TestDate(DateTime d)
    {
        TestContext.Out.WriteLine(d);
        Assert.Pass();
        
    }

    [TestCase(42.6f)]
    [TestCase(42.4f)]
    public void TestInt(int x)
    {
        Assert.That(x,Is.EqualTo(42));
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    [TestCaseSource(nameof(Cases))]
    public void TestB(string a, int b)
    {
    }
}