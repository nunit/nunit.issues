using demo.protocol.helper;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace demo.testenv
{
    internal class HookExtensionAttribute : NUnitAttribute, IApplyToContext
    {
        public void ApplyToContext(TestExecutionContext context)
        {
            context.HookExtension = new HookExtension();
        }
    }

    internal class HookExtension : IHookExtension
    {
        public void BeforeAnySetUps(TestExecutionContext context, IMethodInfo setUpMethod)
        {
            TestLog.Log(context.CurrentTest.IsSuite
                ? $"[Test Environment] Initiate Database (on demand), load schemas (on demand), login as admin"
                : $"[Test Environment] Create Customer User and login");
        }

        public void AfterAnySetUps(TestExecutionContext context, IMethodInfo setUpMethod)
        {
        }

        public void BeforeTest(TestExecutionContext context, TestMethod testMethod)
        {
            TestLog.Log($"[Test Environment] Open shopping application as a common starting point for all tests");
        }

        public void AfterTest(TestExecutionContext context, TestMethod testMethod)
        {
            TestLog.Log($"[Test Environment] Close shopping application");
        }

        public void BeforeAnyTearDowns(TestExecutionContext context, IMethodInfo tearDownMethod)
        {
            if(context.CurrentTest.IsSuite)
                TestLog.Log($"[Test Environment] login as admin");
        }

        public void AfterAnyTearDowns(TestExecutionContext context, IMethodInfo tearDownMethod)
        {
            TestLog.Log(context.CurrentTest.IsSuite
                ? $"[Test Environment] log off as admin"
                : $"[Test Environment] Log off and remove Customer User");
        }
    }
}
