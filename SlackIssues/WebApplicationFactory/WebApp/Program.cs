var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/hw", () =>
{
    var logger = app.Services.GetService<ILogger<Program>>();
    logger.LogCritical("hw called");
    Console.WriteLine("test -w");

    throw new Exception();

    return "Hello World!";
});
app.MapGet("/hw2", () =>
{
    var logger = app.Services.GetService<ILogger<Program>>();
    logger.LogCritical("hw 2 called");
    Console.WriteLine("test");

    throw new InvalidCastException();

    return "Hello World! 2";
});

app.Run();

public partial class Program {}