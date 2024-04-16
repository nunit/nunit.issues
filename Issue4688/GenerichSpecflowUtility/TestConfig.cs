using GlobalBindings;

namespace GlobalSpecflowUtility
{
    /// <summary>
    /// Configuration for the Specflow test
    /// </summary>
    public class TestConfig : SpecFlowTestConfig
    {
        private const string testSettings = nameof(testSettings);

        private TestConfig()
        {
        }

        /// <summary>
        /// Current Instance from the app.config
        /// </summary>
        public static new readonly TestConfig Instance = new TestConfig().GetSection<TestConfig>(testSettings);
    }
 }