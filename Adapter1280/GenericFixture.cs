namespace Adapter1280;

[TestFixture(TypeArgs = [typeof(object)])]
[TestFixture(TypeArgs = [typeof(string)])]
internal sealed class GenericFixtureWithProperArgsProvided<T>
{
    [Test]
    public void SomeTest()
    {
        Assert.That(typeof(T).IsClass, Is.True);
    }
}
