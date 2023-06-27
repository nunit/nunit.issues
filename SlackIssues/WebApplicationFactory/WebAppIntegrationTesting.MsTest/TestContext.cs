using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAppIntegrationTesting.MsTest;

[TestClass]
public class TestContext
{
    public static WebApplicationFactory<Program> Waf { get; set; }

    [AssemblyInitialize]
    public static void Init(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext context)
    {
        Waf = new WebApplicationFactory<Program>();
    }
}