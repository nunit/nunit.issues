using System.Diagnostics;

namespace Issue4962;

public class Tests
{
    [Test, CancelAfter(1000)]
    public async Task SampleTest(CancellationToken cancellationToken)
    {
        var sw = new Stopwatch();
        cancellationToken.Register(() =>
        {
            Console.Write($"Cancelled ");
        });
        
        sw.Start();
        try
        {
            await Task.Delay(15000, cancellationToken);
        }
        catch (Exception)
        {}
        finally
        {}
        Console.WriteLine($"after {sw.Elapsed}");
        Assert.That(cancellationToken.IsCancellationRequested, Is.True);
    }
}
