using checkredisnull.Microsoft.Extensions.Primitives;


using StackExchange.Redis;

namespace NUnitIssue4898;

/// <summary>
/// Code from @moshekar
/// </summary>
public class Tests
{
    [Test]
    public void TestStruct()
    {
        var redisResult = RedisValue.Null;

        Assert.That(redisResult, Is.EqualTo(RedisValue.Null));
    }

    [Test]
    public void TestStringValues()
    {
        StringValues headerInfo = new StringValues("test");  
        Assert.That(headerInfo, Is.EqualTo("test"));

    }
}
