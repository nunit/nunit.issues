using NUnit.Framework.Internal.ExecutionHooks;

namespace Issue5237;
public class Tests
{
    [Test]
    [ExceptionLogging]
    public void AssertPassExample() => Assert.Pass();
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class ExceptionLoggingAttribute : ExecutionHookAttribute
{
    public override void AfterTestHook(HookData hookData)
    {
        if (hookData.Exception is not null)
        {
            TestContext.Out.WriteLine($"Exception thrown in test ({hookData.Context.Test.Name}). Details:");
            TestContext.Out.WriteLine(hookData.Exception);
        }
    }
}