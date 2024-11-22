
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Issue4775;

public class Tests
{
    [Test]
    public void Test()
    {
        var a = new[]
        {
            new[,] { { 0, 1, 2 }, { 0, 0, 0 }, { 1, 1, 0 } },
            new[,] { { 0, 1, 2 }, { 0, 0, 0 }, { 1, 1, 0 } },
            new[,] { { 0, 1, 2 }, { 0, 0, 0 }, { 1, 1, 0 } }
        };
        var b = ColumnIterator();

        // OK
        Assert.That(a, Is.EqualTo(b));
        ClassicAssert.AreEqual(a, b);

        // Throws
        Assert.That(a, Is.EqualTo(b).AsCollection);
        CollectionAssert.AreEqual(a, b);
    }


    [Test]
    public void TestB()
    {
        var a = new[,] { { 0, 1, 2 }, { 0, 0, 0 }, { 1, 1, 0 } };
        var b = new[,] { { 0, 1, 2 }, { 0, 0, 0 }, { 1, 1, 0 } };

        // OK
        Assert.That(a, Is.EqualTo(b));
        ClassicAssert.AreEqual(a, b);

        // Throws
        Assert.That(a, Is.EqualTo(b).AsCollection);
        CollectionAssert.AreEqual(a, b);
    }

    private static IEnumerable<int[,]> ColumnIterator()
    {
        yield return new[,] { { 0, 1, 2 }, { 0, 0, 0 }, { 1, 1, 0 } };
        yield return new[,] { { 0, 1, 2 }, { 0, 0, 0 }, { 1, 1, 0 } };
        yield return new[,] { { 0, 1, 2 }, { 0, 0, 0 }, { 1, 1, 0 } };
    }
}