using NUnit.Framework.Internal.ExecutionHooks;

namespace Issue5238;
public class Tests
{
    internal List<Exception> Errors { get; } = [];

    [Test]
    [ExceptionLogging]
    [Order(1)]
    public void WrappedExceptionExample() => throw new InvalidOperationException();

    [Test]
    [Order(2)]
    public void ValidateException()
    {
        Assert.That(Errors, Has.Count.EqualTo(1));
        Assert.That(Errors[0], Is.TypeOf<InvalidOperationException>());
    }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class ExceptionLoggingAttribute : ExecutionHookAttribute
{
    public override void AfterTestHook(HookData hookData)
    {
        if (hookData.Exception is not null)
        {
            var fixture = hookData.Context.Test.Parent!.Fixture as Tests;
            fixture!.Errors.Add(hookData.Exception);
        }
    }
}