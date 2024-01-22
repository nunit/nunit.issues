namespace Issue4589;

[TestFixture]
public sealed class Foo
{
    [Test]
    public void Test1() { }

    [Test]
    [Explicit]
    public void Test2() { }
}

[Explicit]
public sealed class Bar
{
    [Test]
    public void Test1() { }
}