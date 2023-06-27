using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAppIntegrationTesting.MsTest.NestedTest;

[TestClass]
public class NestedTests
{

    [TestMethod]
    public async Task Test1()
    {
        var response = await TestContext.Waf.CreateClient().SendAsync(new HttpRequestMessage()
        {
            RequestUri = new Uri("http://localhost/hw")
        });

        Assert.Equals(response.StatusCode, HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task Test2()
    {
        var response = await TestContext.Waf.CreateClient().SendAsync(new HttpRequestMessage()
        {
            RequestUri = new Uri("http://localhost/hw2")
        });

        Assert.Equals(response.StatusCode, HttpStatusCode.OK);
    }
}