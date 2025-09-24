using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace MyTestWebApi.Tests;

public class ApiLoggingTests
{
    [Test]
    public async Task Ping_LogsToConsole_AndReturns200()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var resp = await client.GetAsync("/ping");

        Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var payload = await resp.Content.ReadAsStringAsync();
        Assert.That(payload, Is.EqualTo("\"pong\""));

        // Add a line to the test output just so you can see separation
        TestContext.Out.WriteLine("Completed GET /ping");
    }
}