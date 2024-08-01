using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace GlobalBindings
{
    [Binding]
    public sealed class ExampleStepsUsuallyFromOtherDLLs
    {
        [Given("something")]
        public void GivenSomeThing()
        {
            //here would be done anything..
        }

        [Then(@"the path exists")]
        public void ThenThePathExists()
        {
            var runningTest = SpecFlowTestConfig.Instance.RunningTest;
            Assert.That(runningTest.ScenarioLogPath,Is.Not.Null);
            //TODO: Add breakpoint here (2)
            Console.WriteLine("ScenarioLogPath is still: " + runningTest.ScenarioLogPath);
        }
    }
}