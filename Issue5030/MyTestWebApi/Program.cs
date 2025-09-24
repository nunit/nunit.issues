using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Match the reporter's logging setup
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.MapGet("/ping", (ILoggerFactory lf) =>
{
    var log = lf.CreateLogger("Ping");
    log.LogInformation("Ping endpoint invoked at {Time}", DateTimeOffset.Now);

    // Emit several messages so any character-by-character behavior is obvious
    log.LogInformation("Starting work...");
    for (int i = 1; i <= 5; i++)
    {
        log.LogInformation("Step {Step}/5 complete", i);
    }
    log.LogInformation("All done.");

    return Results.Ok("pong");
});

app.Run();

// Needed so WebApplicationFactory<Program> can find Program
public partial class Program { }

