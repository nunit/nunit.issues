using System.Collections;

namespace Issue4549;

[TestFixture]
public class MyTests
{
    [TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.TestCases))]
    public int DivideTest(int n, int d)
    {
        return n / d;
    }
}

public class MyDataClass
{
    public static IEnumerable TestCases
    {
        get
        {
            yield return new TestCaseData(12, 3).SetCategory("So what do you mean, -this works ? Right.").Returns(4);
            yield return new TestCaseData(12, 2).SetName("Whatever-1").Returns(6);
            yield return new TestCaseData(12, 4).Returns(3);
        }
    }
}

internal class Dummy
{
    [Category("So what do you mean - this works?")]
    [Test]
    public void DummyTest()
    {
        Assert.Pass();
    }
}

public class MyOwnCategoryAttribute : CategoryAttribute
{
    public MyOwnCategoryAttribute() : base(nameof(MyOwnCategoryAttribute))
    {
    }
}
