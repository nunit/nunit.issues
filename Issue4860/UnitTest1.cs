using System.Diagnostics;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NUnit.Framework.Internal;
using Microsoft.Extensions.Logging;
using System.IO;

namespace dotnet_nunit_asp_console_writeline;

public class Tests
{
    /// <summary>
    /// This original test shows that we don't capture Console.WriteLine etc. in the test output.
    /// </summary>
    [Test]
    public async Task TestLoggingInRequestHandler()
    {
        // Needed for Debug/Trace calls.
        Trace.Listeners.Add(new ConsoleTraceListener());
        var builder = new WebHostBuilder()
            .Configure(app =>
            {
                var currentExecutionContext = TestExecutionContext.CurrentContext;
                app.Run(async context =>
                {
                    // {
                    //     // This hack allows us to have Console.WriteLine etc. appear in the test output.
                    //     var currentContext = typeof(TestExecutionContext).GetField("AsyncLocalCurrentContext", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as AsyncLocal<TestExecutionContext>;
                    //     currentContext.Value = currentExecutionContext;
                    // }
                    Debug.WriteLine("1. This is Debug.WriteLine");
                    Trace.WriteLine("2. This is Trace.WriteLine");
                    Console.WriteLine("3. This is Console.Writeline");
                    Console.Error.WriteLine("4. This is Console.Error.Writeline");
                    TestContext.WriteLine("5. This is TestContext.WriteLine");
                    TestContext.Out.WriteLine("6. This is TestContext.Out.WriteLine");
                    TestContext.Progress.WriteLine("7. This is TestContext.Progress.WriteLine");
                    TestContext.Error.WriteLine("8. This is TestContext.Error.WriteLine");
                    await context.Response.WriteAsync("Hello, world!");
                });
            })
            .UseKestrel(options =>
            {
                options.Listen(IPAddress.Loopback, 1234);
            })
            .Build();
        await builder.StartAsync();

        var httpclient = new HttpClient();
        var response = await httpclient.GetAsync("http://localhost:1234");
        Assert.That(await response.Content.ReadAsStringAsync(), Is.EqualTo("Hello, world!"));
        await builder.StopAsync();
    }



    /// <summary>
    /// This test works so using Tasks is not related to the issue
    /// </summary>
    [Test]
    public async Task SimpleAsyncTest()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
        // Start an asynchronous task
        await Task.Run(() =>
        {
            Debug.WriteLine("11. This is Debug.WriteLine");
            Trace.WriteLine("12. This is Trace.WriteLine");
            Console.WriteLine("13. This is Console.Writeline");
            Console.Error.WriteLine("14. This is Console.Error.Writeline");
            TestContext.WriteLine("15. This is TestContext.WriteLine");
            TestContext.Out.WriteLine("16. This is TestContext.Out.WriteLine");
            TestContext.Progress.WriteLine("17. This is TestContext.Progress.WriteLine");
            TestContext.Error.WriteLine("18. This is TestContext.Error.WriteLine");
        });

        // This line will execute after the task has completed
        TestContext.WriteLine("Test SimpleAsyncTest completed.");
    }

    /// <summary>
    /// This test works and show that they runs in different threads
    /// </summary>
    [Test]
    public async Task AsyncThreadTest()
    {
        TestContext.WriteLine($"Test starting on Thread ID: {Thread.CurrentThread.ManagedThreadId}");

        await Task.Run(() =>
        {
            TestContext.WriteLine($"Task running on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        });

        TestContext.WriteLine($"Test resumed on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
    }

    /// <summary>
    /// This test works too and show that they runs in different threads
    /// </summary>
    [Test]
    public async Task UsingThreads()
    {
        Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name ?? "Unnamed"}");
        var thread = new Thread(() =>
        {
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name ?? "Unnamed"}");
            TestContext.WriteLine("This is TestContext.WriteLine from a new thread.");

        });

        // Optionally, give the thread a name
        thread.Name = "MyWorkerThread";

        thread.Start();
        thread.Join();
    }

    /// <summary>
    /// This test works too and show that they run when creating threads from a threadpool
    /// </summary>
    [Test]
    public void ThreadPoolThreadTest()
    {
        // Capture the main thread ID for reference
        TestContext.WriteLine($"Main Thread ID: {Thread.CurrentThread.ManagedThreadId}");

        // Queue work to the ThreadPool
        ThreadPool.QueueUserWorkItem(_ =>
        {
            // This will run on a ThreadPool thread
            Console.WriteLine($"ThreadPool Thread ID: {Thread.CurrentThread.ManagedThreadId} - Console.WriteLine");

            // This might not appear in the test output since it's running on a ThreadPool thread
            TestContext.WriteLine($"ThreadPool Thread ID: {Thread.CurrentThread.ManagedThreadId} - TestContext.WriteLine");
        });

        // Give the ThreadPool thread time to execute before the test ends
        Thread.Sleep(1000); // Ensure that the ThreadPool thread completes its work before the test finishes
    }

    /// <summary>
    /// This test checks if the issue is related to the HttpContext, and it is not - this test works too
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task SimulatedHttpContextTest()
    {
        // Arrange: Create a mock HttpContext
        var context = new DefaultHttpContext();

        // Set up the response stream to capture output
        context.Response.Body = new MemoryStream();

        // Act: Simulate a request handling
        await HandleRequest(context);

        // Read the response body (just for demonstration)
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using (var reader = new StreamReader(context.Response.Body))
        {
            var responseBody = await reader.ReadToEndAsync();
            TestContext.WriteLine($"Response Body: {responseBody}");
        }

        // Assert: Verify output from the test context
        TestContext.WriteLine("Test completed.");
    }

    private async Task HandleRequest(HttpContext context)
    {
        // Simulate some request handling work

        // Write to the response body
        await context.Response.WriteAsync("Hello, World!");

        // Write to Console and TestContext to observe output
        Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} - Console.WriteLine inside HttpContext");
        TestContext.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} - TestContext.WriteLine inside HttpContext");
    }

    /// <summary>
    /// This test captures the Console output in a WebHost and checks if that will work, and it does.
    /// </summary>
    [Test]
    public async Task CaptureConsoleOutputInWebHostTest()
    {
        // Step 1: Redirect the console output
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        var builder = new WebHostBuilder()
            .Configure(app =>
            {
                app.Run(async context =>
                {
                    // Write to the console
                    Console.WriteLine("Hello from Kestrel");
                    await context.Response.WriteAsync("Response complete.");
                });
            })
            .UseKestrel()
            .UseUrls("http://localhost:5000")
            .Build();

        await builder.StartAsync();

        // Make a test HTTP request
        using (var client = new System.Net.Http.HttpClient())
        {
            var response = await client.GetStringAsync("http://localhost:5000");
            TestContext.WriteLine($"Response: {response}");
        }

        // Stop the WebHost
        await builder.StopAsync();

        // Step 2: Capture and verify the console output
        string consoleOutput = stringWriter.ToString();
        TestContext.WriteLine($"Captured Console Output: {consoleOutput}");

        Assert.That(consoleOutput, Does.Contain("Hello from Kestrel"));
    }


    /// <summary>
    /// This test checks if the TestExecutionContext is available in the WebHost, and it is.
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task CaptureTestExecutionContextInWebHostTest()
    {
        // Capture the initial test execution context
        var initialContext = TestExecutionContext.CurrentContext;
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        TestContext.WriteLine($"Initial TestExecutionContext: {initialContext}");

        var builder = new WebHostBuilder()
            .Configure(app =>
            {
                app.Run(async context =>
                {
                    // Try to access the TestExecutionContext in the request handler
                    var currentContext = TestExecutionContext.CurrentContext;

                    if (currentContext == null)
                    {
                        Console.WriteLine("TestExecutionContext is null inside Kestrel.");
                    }
                    else
                    {
                        TestContext.WriteLine("TestExecutionContext is available inside Kestrel.");
                        Console.WriteLine("TestExecutionContext is available inside Kestrel.");
                    }

                    await context.Response.WriteAsync("Response complete.");
                });
            })
            .UseKestrel()
            .UseUrls("http://localhost:5000")
            .Build();

        await builder.StartAsync();

        // Make a test HTTP request
        using (var client = new System.Net.Http.HttpClient())
        {
            var response = await client.GetStringAsync("http://localhost:5000");
            TestContext.WriteLine($"Response: {response}");
        }

        // Stop the WebHost
        await builder.StopAsync();

        // Step 2: Capture and verify the console output
        string consoleOutput = stringWriter.ToString();
        TestContext.WriteLine($"Captured Console Output: {consoleOutput}");
    }

    /// <summary>
    /// THis test shows that TestCOntext.Out is not null, but the output is not captured in the test output.
    /// Uncommenting the console.WriteLine will show that the output is written to the console.
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task DirectWriteToTestContextOutInWebHostTest()
    {
        var initialContext = TestExecutionContext.CurrentContext;
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        TestContext.WriteLine($"Initial TestExecutionContext: {initialContext}");
        var builder = new WebHostBuilder()
            .Configure(app =>
            {
                app.Run(async context =>
                {
                    // Directly write to TestContext.Out
                    if (TestContext.Out != null)
                    {
                        TestContext.Out.WriteLine("Direct write to TestContext.Out.");
                        // Console.WriteLine("Direct write to Console.Writeline");
                    }
                    else
                    {
                        Console.WriteLine("TestContext.Out is null inside Kestrel.");
                    }

                    await context.Response.WriteAsync("Response complete.");
                });
            })
            .UseKestrel()
            .UseUrls("http://localhost:5000")
            .Build();

        await builder.StartAsync();

        using (var client = new System.Net.Http.HttpClient())
        {
            var response = await client.GetStringAsync("http://localhost:5000");
            TestContext.WriteLine($"Response: {response}");
        }

        await builder.StopAsync();
        // Step 2: Capture and verify the console output
        string consoleOutput = stringWriter.ToString();
        TestContext.WriteLine($"Captured Console Output: {consoleOutput}");
    }


}


