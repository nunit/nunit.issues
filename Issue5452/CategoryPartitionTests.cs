namespace Issue5452;

[Category("Slow")]
public class CategoryPartitionTests
{
    [Test] public void Slow01() => Assert.Pass();
    [Test] public void Slow02() => Assert.Pass();
    [Test] public void Slow03() => Assert.Pass();
    [Test] public void Slow04() => Assert.Pass();
    [Test] public void Slow05() => Assert.Pass();
    [Test] public void Slow06() => Assert.Pass();

    [Test, Explicit] public void SlowExplicit01() => Assert.Fail("Explicit test must not run in a partition");
    [Test, Explicit] public void SlowExplicit02() => Assert.Fail("Explicit test must not run in a partition");
    [Test, Explicit] public void SlowExplicit03() => Assert.Fail("Explicit test must not run in a partition");
    [Test, Explicit] public void SlowExplicit04() => Assert.Fail("Explicit test must not run in a partition");
}
