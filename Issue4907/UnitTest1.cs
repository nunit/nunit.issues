using System.Reflection;

namespace Issue4907;

public class Tests
{
    [Test]
    public async Task Test1()
    {
        await Assert.ThatAsync(async () => throw new Exception("Ugh"), Throws.Exception);
    }

    [Test]
    public async Task Test2()
    {
        await Assert.ThatAsync(() => Task.FromException(new Exception("Ugh")), Throws.Exception);
    }

    [Test]
    public void Test3()
    {
        Assert.That(() => throw new Exception("Ugh"), Throws.Exception);
    }

    // Workaround

    [Test]
    public async Task TestW1()
    {
        await Assert.ThatAsync(async () => throw new Exception("Ugh"), Throws.TypeOf<Exception>());
    }


    // Added these from the documentation

    [Test]
    public async Task ThrowsArgumentExceptionTest()
    {
        await Assert.ThatAsync(async () => { throw new ArgumentException(); }, Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public async Task ThrowsExceptionTypeOfArgumentExceptionTest()
    {
        await Assert.ThatAsync(async () => { throw new ArgumentException(); }, Throws.Exception.TypeOf<ArgumentException>());
    }

    [Test]
    public async Task ThrowsArgumentExceptionWithSpecificPropertyTest()
    {
        await Assert.ThatAsync(async () =>
        {
            var ex = new MyArgumentException("whatever");
            throw ex;
        }, Throws.TypeOf<MyArgumentException>().With.Property("MyProperty").EqualTo("42"));
    }

    [Test]
    public async Task ThrowsArgumentExceptionDirectlyTest()
    {
        await Assert.ThatAsync(async () => { throw new ArgumentException(); }, Throws.ArgumentException);
    }

    [Test]
    public async Task ThrowsTargetInvocationExceptionWithInnerArgumentExceptionTest()
    {
        await Assert.ThatAsync(async () =>
        {
            throw new TargetInvocationException(new ArgumentException());
        }, Throws.TargetInvocationException.With.InnerException.TypeOf<ArgumentException>());
    }

}

public class MyArgumentException : ArgumentException
{
    public MyArgumentException(string message) : base(message)
    {
    }

    public string MyProperty { get; set; } = "42";
}