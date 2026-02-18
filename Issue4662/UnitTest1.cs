using NUnit.Framework.Legacy;

namespace Issue4662;

public class Tests
{
    

    [TestCase("dhdhdh")]
    public void Test1(string s)
    {
        StringAssert.IsNotNullNorEmpty(s);
    }
    
    [TestCase("   ")]
    [TestCase(null)]
    public void Test2(string? s)
    {
        StringAssert.IsNullOrWhiteSpace(s);
    }
}