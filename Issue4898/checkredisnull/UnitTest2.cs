using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
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
        var context = new DefaultHttpContext();
        var response = context.Response;
        response.Headers["X-Test"] = "test";
        StringValues headerInfo = response.Headers["X-Test"];
        Assert.That(headerInfo, Is.EqualTo("test"));

    }
}
