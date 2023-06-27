using System.Net;
using NUnit.Framework;

namespace WebAppIntegrationTesting.NestedTest;

[TestFixture]
public class NestedTests
{

    [Test]
    public async Task Test1()
    {
        var response = await TestContext.Waf.CreateClient().SendAsync(new HttpRequestMessage()
        {
            RequestUri = new Uri("http://localhost/hw")
        });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Test2()
    {
        var response = await TestContext.Waf.CreateClient().SendAsync(new HttpRequestMessage()
        {
            RequestUri = new Uri("http://localhost/hw2")
        });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}