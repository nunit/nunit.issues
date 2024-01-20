namespace ValueTask;

public class Tests
{
    public static ValueTask<string> Method() => new("");

    [Test]
    public async Task Test1()
    {
        await Assert.ThatAsync(()=>Method().AsTask(),Is.EqualTo(""));
    }
}