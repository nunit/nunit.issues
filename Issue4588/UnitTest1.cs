namespace ValueTask;

public class Tests
{
    private static ValueTask<string> Method() => new("");

    private static Task<string> MethodT() => new(()=>"");


    /// <summary>
    /// Doesnt compile
    /// </summary>
    /// <returns></returns>
    //[Test]
    //public async Task Test0()
    //{
    //    await Assert.ThatAsync( () => Method(), Is.EqualTo(""));
    //}



    /// <summary>   
    /// Workaround with adding async/await
    /// </summary>
    [Test]
    public async Task Test1()
    {
        var result = await Method();
        await Assert.ThatAsync(async () => await Method(), Is.EqualTo(""));
    }

    [Test]
    public async Task Test2()
    {
        var result = await MethodT().ConfigureAwait(true);
        Assert.That(result, Is.EqualTo(""));
        //await Assert.ThatAsync(MethodT, Is.EqualTo(""));
    }
}