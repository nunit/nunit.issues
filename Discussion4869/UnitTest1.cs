namespace Discussion4869;

public class MyIntegrationTest
{
    [Test,Order(1)]
    public void Step1()
    {
        TestContext.Out.WriteLine(nameof(MyIntegrationTest)+"."+nameof(Step1));
        Assert.Pass();
    }

    [Test, Order(2)]
    public void Step2()
    {
        TestContext.Out.WriteLine(nameof(MyIntegrationTest) + "." + nameof(Step2));
        Assert.Pass();
    }

    [Test, Order(3)]
    public void Step3()
    {
        TestContext.Out.WriteLine(nameof(MyIntegrationTest) + "." + nameof(Step3));
        Assert.Pass();
    }
}
