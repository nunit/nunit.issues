global using NUnit.Framework;

namespace Issue4635;

public class Tests
{
    [Test]
    public void ListInsertionTest()
    {
        var aList = new ListExt();
        aList.Add(3.0);
        aList.Add(4.0);


        var bList = new ListExt();
        bList.Add(4.0);
        bList.Add(3.0);

        CollectionAssert.AreEquivalent(aList, bList);
    }
}