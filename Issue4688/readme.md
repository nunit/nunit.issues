# NUnit Reproductions for  Issues in the NUnit Framework

https://github.com/nunit/nunit/issues/4688

Build the solution with VS22, and run both specflow tests.
Test fails within VS22 for NET 5.0, while the same test is successful for NET 4.7.2. 

While executed for NET472 shows: 
```python
    ScenarioLogPath was set to: Feature\AddTwoNumbers
    Given something
    -> done: ExampleStepsUsuallyFromOtherDLLs.GivenSomeThing() (0,0s)
    Then the path exists
    ScenarioLogPath is still: Feature\AddTwoNumbers
    -> done: ExampleStepsUsuallyFromOtherDLLs.ThenThePathExists() (0,0s)
    RESULT ONLY WITH NET472 successful:Feature\AddTwoNumbers\SomeFile.log
```

You'll end up with the following output for NET5.0: 

    Message: 
    TearDown : System.ArgumentNullException : Value cannot be null. (Parameter 'path1')

    Stack Trace: 
    --TearDown
    Path.Combine(String path1, String path2)
    FailedHooks.AfterScenarioTestEnd(ScenarioContext scenarioContext) line 20
    TestExecutionEngine.FireEvents(HookType hookType)
    TestExecutionEngine.FireScenarioEvents(HookType bindingEvent)
    TestExecutionEngine.OnScenarioEnd()
    TestRunner.OnScenarioEnd()
    SpecflowTestFeature.TestTearDown()

    Standard Output: 
    ScenarioLogPath was set to: Feature\AddTwoNumbers
    Given something
    -> done: ExampleStepsUsuallyFromOtherDLLs.GivenSomeThing() (0,0s)
    Then the path exists
    ScenarioLogPath is still: Feature\AddTwoNumbers
    -> done: ExampleStepsUsuallyFromOtherDLLs.ThenThePathExists() (0,0s)
