namespace Discussion4869;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class StepAttribute(int order) : TestAttribute
{
    public new void ApplyToTest(NUnit.Framework.Internal.Test test)
    {
        base.ApplyToTest(test);

        // Apply the order to the test
        test.Properties.Set(NUnit.Framework.Internal.PropertyNames.Order, order);
    }
}

public class MyIntegrationTest2
{
    int i = 0;
    [Step(1)]
    public void Step1()
    {
        TestContext.Out.WriteLine($"{nameof(MyIntegrationTest2)}.{nameof(Step1)} {++i}");
        Assert.Pass();
    }

    [Step(3)]
    public void Step3()
    {
        TestContext.Out.WriteLine($"{nameof(MyIntegrationTest2)}.{nameof(Step3)} {++i}");
        Assert.Pass();
    }

    [Step(2)]
    public void Step2()
    {
        TestContext.Out.WriteLine($"{nameof(MyIntegrationTest2)}.{nameof(Step2)} {++i}");
        Assert.Pass();
    }
}


