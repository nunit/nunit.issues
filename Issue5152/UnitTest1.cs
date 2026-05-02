namespace Issue5152;

public class Tests
{
    [Test]
    public void TC_Assert_TC105_byte() 
    { 
        byte testValue = 30; 
        Assert.That(testValue, Is.EqualTo(50).Within(50).Percent);  // Fails
    }

    [Test]
    public void TC_Assert_TC105_Int()
    {
        int testValue = 30;
        Assert.That(testValue, Is.EqualTo(50).Within(50).Percent);  // Pass
    }


    [Test]
    public void TC_Assert_TC105_CastDouble() 
    { 
        byte testValue = 30; 
        Assert.That((double)testValue, Is.EqualTo(50).Within(50.0).Percent); // Pass
    }
}
