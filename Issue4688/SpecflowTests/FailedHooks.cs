using GlobalSpecflowUtility;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace GlobalBindings
{
    [Binding]
    public class FailedHooks
    {

        [AfterScenario(Order = 1000)]
        public void AfterScenarioTestEnd(ScenarioContext scenarioContext)
        {
            //HERE OCCURS THE ISSUE:
            //  The TestConfig.Instance value does not exist with the same value for NET5 as it does for NET472
            
            //TODO: ADD DEBUG POINT HERE to see the error. (4)
            var instance = TestConfig.Instance;
            Console.WriteLine("RESULT ONLY WITH NET472 successful:" + Path.Combine(TestConfig.Instance.RunningTest.ScenarioLogPath, "SomeFile.log"));
        }
    }
}
