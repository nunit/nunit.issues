
using System.Diagnostics;

using NUnit.Framework;

namespace RetrieveParallelScope
{
    /// <remarks><para>
    /// <see cref="ProgressTraceListener"/> for background on having to add this setup-fixture.
    /// </para><para>
    /// Note that a setup-fixture only applies to the assembly it is contained in.
    /// </para></remarks>
    [SetUpFixture]
    public class SetUpFixture
    {
        /// <summary>
        /// One-time setup for all the test fixtures in this assembly.
        /// </summary>
        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            Trace.Listeners.Add(new ProgressTraceListener()); // Note that this listener "directs tracing or debugging output", i.e. always both, even being named "Trace".
        }
    }
}
