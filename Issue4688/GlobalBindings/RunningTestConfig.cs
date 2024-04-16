using System.Configuration;

namespace GlobalBindings
{
    /// <summary>
    /// The running test config.
    /// </summary>
    public class RunningTestConfig : ConfigurationElement
    {
        // TODO: Add a breakpoint here (1)
        public RunningTestConfig()
        {

        }

        /// <summary>
        /// Gets the scenario test directory, it is set at test start
        /// </summary>
        public string ScenarioLogPath { get; internal set; }
    }
}
