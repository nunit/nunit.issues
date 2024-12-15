using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace NUnitIssue4898;

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

		Assert.That(response.Headers["X-Test"], Is.EqualTo("test"));

	}
}
