using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Runtime.Versioning;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

namespace testing;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureServices(services =>
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNUnitLogger().SetMinimumLevel(LogLevel.Information);
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
        });
    }

}

#if ISTHISNEEDED
public class NunitLogger<T> : ILogger<T>, IDisposable where T : notnull
{
    public void Dispose()
    {
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception, string> formatter)
    {
        TestContext.Out.WriteLine(state!.ToString());   // Seems duplicate of IlgNunitLogger l 65, and never called
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }
}
#endif

public class IlgNunitLogger : ILogger, IDisposable
{
    public void Dispose()
    {
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception, string> formatter)
    {
        TestContext.Out.WriteLine(state!.ToString());
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }
}



[UnsupportedOSPlatform("browser")]
[ProviderAlias("NUnitLogger")]
public sealed class IlgNUnitLoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
        Logger?.Dispose();
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (Logger != null) return Logger;

        Logger = new();
        return Logger;
    }

    public static IlgNunitLogger? Logger { get; private set; }
}

public static class IlgNunitLoggerExtensions
{
    public static ILoggingBuilder AddNUnitLogger(this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, IlgNUnitLoggerProvider>());

        //LoggerProviderOptions.RegisterProviderOptions
        //    <ColorConsoleLoggerConfiguration, ColorConsoleLoggerProvider>(builder.Services);

        return builder;
    }
}






public class Tests
{
    private CustomWebApplicationFactory<Program> factory;
    private HttpClient? client;
    private ILogger logger;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        factory = new CustomWebApplicationFactory<Program>();
    }

    [SetUp]
    public void Setup()
    {
        client = factory.CreateClient();
        using var scope = factory.Services.CreateScope();
        logger = scope.ServiceProvider.GetRequiredService<ILogger<Tests>>();
    }

    [TearDown]
    public void Cleanup()
    {
        client?.Dispose();
        client = null;
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        factory.Dispose();
    }

    [Test]
    public async Task Test1()
    {
        logger.LogInformation("This is a test log message from the test.");
        var result = await client.GetAsync("https://localhost:7010/weatherforecast");
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
