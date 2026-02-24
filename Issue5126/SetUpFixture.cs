
using System.Diagnostics;
using System.Linq;

using NUnit.Framework;
using NUnit.Framework.Diagnostics;
using NUnit.Framework.Internal;

namespace Issue5126
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            if (!Trace.Listeners.OfType<ProgressTraceListener>().Any())
                Trace.Listeners.Add(new ProgressTraceListener());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Trace.WriteLine("TotalCount        = " + TestExecutionContext.CurrentContext.CurrentResult.TotalCount);
            Trace.WriteLine("SkipCount         = " + TestExecutionContext.CurrentContext.CurrentResult.SkipCount);
            Trace.WriteLine("InconclusiveCount = " + TestExecutionContext.CurrentContext.CurrentResult.InconclusiveCount);
        }
    }
}
