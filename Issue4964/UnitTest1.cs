using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Issue4964;

public class Tests
{
    [Test]
    public void UsesProvidedIComparerWithNot()
    {
        var comparer = new ObjectComparer();
        Assert.Multiple(() =>
        {
            Assert.That(2 + 3, Is.Not.EqualTo(4).Using(comparer));
            Assert.That(comparer.WasCalled, "Comparer was not called");
        });
    }
}

internal class ObjectComparer : IEqualityComparer<int>
{
    public bool WasCalled { get; private set; }

    public bool Equals(int x, int y)
    {
        WasCalled = true;
        return x == y;
    }

    public int GetHashCode([DisallowNull] int obj)
    {
        return obj.GetHashCode();
    }
}