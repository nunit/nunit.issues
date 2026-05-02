namespace Issue5178;

public class Tests
{
    [TestCaseSource(nameof(ArraySources))]
    public void GenericMethod<T>(IEnumerable<T> data)
    {
        Assert.Pass();
    }

    [TestCaseSource(nameof(ArraySources))]
    public void ObjectMethod(IEnumerable<object> data)
    {
        Assert.Pass();
    }

    private static IEnumerable<object> ArraySources
    {
        get
        {
            yield return new int[][]
            {
                    new[] {1, 2, 3},
                    new[] {4, 5, 6}
            };
            yield return new List<List<double>> { new List<double> { 1.0, 2.0, 3.0 } };
            yield return (new int[][]
            {
                    new[] {1, 2, 3},
                    new[] {4, 5, 6}
            }).AsEnumerable();
            yield return new Array[]
            {
                    Array.CreateInstance(typeof(decimal), 5),
                    Array.CreateInstance(typeof(decimal), 5),
            };
        }
    }
}
