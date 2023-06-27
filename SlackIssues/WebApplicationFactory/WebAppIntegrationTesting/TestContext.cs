using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace WebAppIntegrationTesting;

public class NUnitLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new NUnitLogger();
    }

    public void Dispose()
    {
        // Cleanup logic, if any
    }
}

public class NUnitLogger : ILogger
{
    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        var message = formatter(state, exception);

        NUnit.Framework.TestContext.Progress.WriteLine(message);
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }
}

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<ILoggerProvider, NUnitLoggerProvider>();
        });
    }
}


[SetUpFixture]
public class TestContext
{
    public static WebApplicationFactory<Program> Waf { get; set; }

    [OneTimeSetUp]
    public static void Init()
    {
        Waf = new CustomWebApplicationFactory<Program>();
    }
}
