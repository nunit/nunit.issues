
using System.Diagnostics;

using NUnit.Framework;
using NUnit.Framework.Internal;

namespace RetrieveParallelScope
{
    public class NUnitEx
    {
        public static bool CurrentTestIsExecutingParallel
        {
            get
            {
                Trace.WriteLine("This is " + TestContext.CurrentContext.Test.FullName + ":");

                var contextProperties = TestContext.CurrentContext.Test.Properties[PropertyNames.ParallelScope];
                Trace.WriteLine(" > ParallelScope @ TestContextEx: " + IEnumerableEx.ItemsToString(contextProperties));

                var executionContextProperties = TestExecutionContext.CurrentContext.CurrentTest.Properties[PropertyNames.ParallelScope];
                Trace.WriteLine(" > ParallelScope @ TestExecutionContextEx: " + IEnumerableEx.ItemsToString(executionContextProperties));

            ////if (some logic on properties)
            ////    return (true);
            ////else
                    return (false);
            }
        }
    }
}
