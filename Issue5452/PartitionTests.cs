namespace Issue5452;

public class PartitionTests
{
    [Test] public void Test01() => Assert.Pass();
    [Test] public void Test02() => Assert.Pass();
    [Test] public void Test03() => Assert.Pass();
    [Test] public void Test04() => Assert.Pass();
    [Test] public void Test05() => Assert.Pass();
    [Test] public void Test06() => Assert.Pass();
    [Test] public void Test07() => Assert.Pass();
    [Test] public void Test08() => Assert.Pass();
    [Test] public void Test09() => Assert.Pass();
    [Test] public void Test10() => Assert.Pass();

    [Test, Explicit] public void Explicit01() => Assert.Fail("Explicit test must not run in a partition");
    [Test, Explicit] public void Explicit02() => Assert.Fail("Explicit test must not run in a partition");
}
