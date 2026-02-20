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

public sealed class ParamsAsLastAfterArrayParameterTests
{
    private static IEnumerable<Array> Testcases
    {
        get
        {
            yield return new [] { 1, 2, 3 };
            yield return new[] { 4,5,6,7 };
        }
    }

    [TestCaseSource(nameof(Testcases))]
    public void Foo(Array values/*,params int[] extra*/)
    {
        Assert.That(values.Length, Is.GreaterThanOrEqualTo(3));
        //Assert.That(extra.Length, Is.EqualTo(0));
    }
}

public sealed class ParamsAsLastAfterIntParameterTests
{
    private static IEnumerable<Array> Testcases
    {
        get
        {
            yield return new[] { 1, 2, 3 };

        }
    }

    [TestCaseSource(nameof(Testcases))]
    public void Foo(int value, params int[] extra)
    {
        Assert.That(value, Is.Not.Zero);
        Assert.That(extra.Length, Is.GreaterThanOrEqualTo(2));
    }
}

public sealed class ArrayAsLastAfterIntParameterTests
{
    private static IEnumerable<Array> Testcases
    {
        get
        {
            yield return new[] { 1, 2, 3 };

        }
    }

    [TestCaseSource(nameof(Testcases))]
    public void Foo(int value, Array extra)
    {
        Assert.That(value, Is.Not.Zero);
        Assert.That(extra.Length, Is.GreaterThanOrEqualTo(2));
    }
}
