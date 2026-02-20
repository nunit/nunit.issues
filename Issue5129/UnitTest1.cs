namespace Issue5129;

public sealed class ArrayAsParameterTests
{
    private static IEnumerable<Array> Testcases
    {
        get
        {
            yield return new[] { 1, 2, 3 };
        }
    }

    [TestCaseSource(nameof(Testcases))]
    public void Foo(Array values)
    {
        Assert.That(values.Length, Is.EqualTo(3));
    }
}

public sealed class IntArrayAsParameterTests
{
    private static IEnumerable<int[]> Testcases
    {
        get
        {
            yield return new[] { 1, 2, 3 };
        }
    }

    [TestCaseSource(nameof(Testcases))]
    public void Foo(int[] values)
    {
        Assert.That(values.Length, Is.EqualTo(3));
    }
}


public sealed class ObjectAsParameterTests
{
    private static IEnumerable<object> Testcases
    {
        get
        {
            yield return new[] { 1, 2, 3 };
        }
    }

    [TestCaseSource(nameof(Testcases))]
    public void Foo(object values)
    {

        Assert.That(((Array)values).Length, Is.EqualTo(3));
    }
}



public sealed class ListAsParameterTests
{
    private static IEnumerable<IList<int>> Testcases
    {
        get
        {
            yield return new List<int> { 1, 2, 3 };
        }
    }

    [TestCaseSource(nameof(Testcases))]
    public void Foo(IList<int> values)
    {
        Assert.That(values.Count, Is.EqualTo(3));
    }
}

