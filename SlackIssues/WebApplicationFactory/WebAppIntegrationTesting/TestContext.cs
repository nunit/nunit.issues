using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace WebAppIntegrationTesting;

[SetUpFixture]
public class TestContext
{
    public static WebApplicationFactory<Program> Waf { get; set; }

    [OneTimeSetUp]
    public static void Init()
    {
        Waf = new WebApplicationFactory<Program>();
    }
}
