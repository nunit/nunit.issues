namespace Issue4298;

[TestFixture]
public abstract class Base
{
    //[Test]
    public void Base_Should_do_something()
    {
        Assert.Pass();
    }
}

internal class OuterFixture : Base
{
    public class SomeInnerScenario : OuterFixture
    {
        [Test] public void Should_something() { }
    }

    public class AnotherInnerScenario : OuterFixture
    {
        [Test] public void Should_something_else() { }
    }

    /* helper methods */
}