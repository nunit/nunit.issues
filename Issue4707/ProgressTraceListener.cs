
using System.Diagnostics;
using System.Security.Permissions;

using NUnit.Framework;

namespace RetrieveParallelScope
{
    /// <summary>
    /// Directs tracing or debugging output to <see cref="TestContext.Progress"/>.
    /// </summary>
    /// <remarks>
    /// Implemented following the <see cref="ConsoleTraceListener"/> implementation.
    /// </remarks>
    [HostProtection(Synchronization = true)]
    public class ProgressTraceListener : TextWriterTraceListener
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressTraceListener"/> class.
        /// </summary>
        public ProgressTraceListener()
            : base(TestContext.Progress)
        {
        }
    }
}
