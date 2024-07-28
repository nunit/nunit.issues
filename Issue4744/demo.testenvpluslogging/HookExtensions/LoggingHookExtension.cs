using demo.protocol.helper;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace demo.testenvpluslogging.HookExtensions
{
    internal class LoggingHookExtension
    {
        public void BeforeAnySetUps(TestExecutionContext context, IMethodInfo setUpMethod)
        {
            TestLog.SetContext(setUpMethod.Name);
        }

        public void AfterAnySetUps(TestExecutionContext context, IMethodInfo setUpMethod)
        {
            TestLog.ResetContext();
        }

        public void BeforeTest(TestExecutionContext context, TestMethod testMethod)
        {
            TestLog.SetContext(testMethod.Name);
        }

        public void AfterTest(TestExecutionContext context, TestMethod testMethod)
        {
            TestLog.ResetContext();
        }

        public void BeforeAnyTearDowns(TestExecutionContext context, IMethodInfo tearDownMethod)
        {
            TestLog.SetContext(tearDownMethod.Name);
        }

        public void AfterAnyTearDowns(TestExecutionContext context, IMethodInfo tearDownMethod)
        {
            TestLog.ResetContext();
        }
    }

}
