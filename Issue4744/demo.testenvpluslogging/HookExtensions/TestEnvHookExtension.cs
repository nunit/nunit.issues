using demo.protocol.helper;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace demo.testenvpluslogging.HookExtensions;

internal class TestEnvHookExtension
{
    public void BeforeAnySetUps(TestExecutionContext context, IMethodInfo setUpMethod)
    {
        using (new LoggingContextGuard(setUpMethod.Name))
        {
            TestLog.Log(context.CurrentTest.IsSuite
                ? $"[Test Environment] Initiate Database (on demand), load schemas (on demand), login as admin"
                : $"[Test Environment] Create Customer User and login");
        }
    }

    public void AfterAnySetUps(TestExecutionContext context, IMethodInfo setUpMethod)
    {
    }

    public void BeforeTest(TestExecutionContext context, TestMethod testMethod)
    {
        using (new LoggingContextGuard(testMethod.Name))
        {
            TestLog.Log($"[Test Environment] Open shopping application as a common starting point for all tests");
        }
    }

    public void AfterTest(TestExecutionContext context, TestMethod testMethod)
    {
        using (new LoggingContextGuard(testMethod.Name))
        {
            TestLog.Log($"[Test Environment] Close shopping application");
        }
    }

    public void BeforeAnyTearDowns(TestExecutionContext context, IMethodInfo tearDownMethod)
    {
        using (new LoggingContextGuard(tearDownMethod.Name))
        {
            if(context.CurrentTest.IsSuite)
                TestLog.Log($"[Test Environment] login as admin");
        }
    }

    public void AfterAnyTearDowns(TestExecutionContext context, IMethodInfo tearDownMethod)
    {
        using (new LoggingContextGuard(tearDownMethod.Name))
        {
            TestLog.Log(context.CurrentTest.IsSuite
                ? $"[Test Environment] log off as admin"
                : $"[Test Environment] Log off and remove Customer User");
        }
    }
}

internal class LoggingContextGuard : IDisposable
{
    public LoggingContextGuard(string name)
    {
        TestLog.SetContext(name);
    }

    public void Dispose()
    {
        TestLog.ResetContext();
    }
}