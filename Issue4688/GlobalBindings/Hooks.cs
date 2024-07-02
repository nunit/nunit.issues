using NUnit.Framework;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace GlobalBindings
{
    [Binding]
    public class Hooks
    {
        /// <summary>
        /// A before test scenario hook. Prepare scenario test environment.
        /// </summary>
        [BeforeScenario(Order = 0)]
        public void AtScenario(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            string scenarioTitle = TestContext.CurrentContext.Test.Name;
            var runningTest = SpecFlowTestConfig.Instance.RunningTest;
            runningTest.ScenarioLogPath = Path.Combine("Feature", scenarioTitle);
            Console.WriteLine("ScenarioLogPath was set to: "+runningTest.ScenarioLogPath);
        }
    }
}
