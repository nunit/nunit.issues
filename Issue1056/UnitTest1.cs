using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System.Collections;

namespace Issue1056;

[TestFixture]
public class TestAssemblyTests
{
    [Test, TestCaseSource(typeof(ItemTestCaseProvider))]
    public int TestTheTruth(Item item)
    {
        var program = new Program();
        return item.Quality;
    }
}

class ItemTestCaseProvider : IEnumerable<ItemTestCaseData>
{
    public IEnumerator<ItemTestCaseData> GetEnumerator()
    {
        yield return new ItemTestCaseData(ItemBuilder.AnyItem.WithInitialQuality(1).ToBeSoldIn(1).ShouldByTomorrowHaveQualityOf(0));
        yield return new ItemTestCaseData(ItemBuilder.AnyItem.WithInitialQuality(2).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(0));
        yield return new ItemTestCaseData(ItemBuilder.AnyItem.WithInitialQuality(0).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(0));
        yield return new ItemTestCaseData(ItemBuilder.AgedBrie.WithInitialQuality(0).ToBeSoldIn(1).ShouldByTomorrowHaveQualityOf(1));
        yield return new ItemTestCaseData(ItemBuilder.AgedBrie.WithInitialQuality(0).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(2));
        yield return new ItemTestCaseData(ItemBuilder.AgedBrie.WithInitialQuality(50).ToBeSoldIn(1).ShouldByTomorrowHaveQualityOf(50));
        yield return new ItemTestCaseData(ItemBuilder.AgedBrie.WithInitialQuality(50).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(50));
        yield return new ItemTestCaseData(ItemBuilder.Sulfuras.ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(80));
        yield return new ItemTestCaseData(ItemBuilder.BackstagePass.ToBeSoldIn(11).ShouldByTomorrowHaveQualityOf(1));
        yield return new ItemTestCaseData(ItemBuilder.BackstagePass.ToBeSoldIn(10).ShouldByTomorrowHaveQualityOf(2));
        yield return new ItemTestCaseData(ItemBuilder.BackstagePass.ToBeSoldIn(5).ShouldByTomorrowHaveQualityOf(3));
        yield return new ItemTestCaseData(ItemBuilder.BackstagePass.WithInitialQuality(50).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(0));
        yield return new ItemTestCaseData(ItemBuilder.ConjuredItem.WithInitialQuality(2).ToBeSoldIn(1).ShouldByTomorrowHaveQualityOf(0));
        yield return new ItemTestCaseData(ItemBuilder.ConjuredItem.WithInitialQuality(4).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(0));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
class ItemBuilder
{
    public static ItemBuilder AnyItem { get { return new ItemBuilder("Any Item"); } }
    public static ItemBuilder AgedBrie { get { return new ItemBuilder("Aged Brie"); } }
    public static ItemBuilder Sulfuras { get { return new ItemBuilder("Sulfuras, Hand of Ragnaros").WithInitialQuality(80); } }
    public static ItemBuilder BackstagePass { get { return new ItemBuilder("Backstage passes to a TAFKAL80ETC concert"); } }
    public static ItemBuilder ConjuredItem { get { return new ItemBuilder("Conjured Mana Cake"); } }

    public Item Item { get; private set; }
    public int ExpectedQuality { get; private set; }

    private ItemBuilder(string name)
    {
        Item = new Item { Name = name };
    }

    public ItemBuilder ToBeSoldIn(int days)
    {
        Item.SellIn = days;
        return this;
    }
    public ItemBuilder WithInitialQuality(int quality)
    {
        Item.Quality = quality;
        return this;
    }

    internal ItemBuilder ShouldByTomorrowHaveQualityOf(int quality)
    {
        ExpectedQuality = quality;
        return this;
    }
}

public class Item
{
    public string Name { get; internal set; }
    public int SellIn { get; internal set; }
    public int Quality { get; internal set; }
}

// Works
//internal class ItemTestCaseData : TestCaseParameters
//{
//    public ItemTestCaseData(ItemBuilder itemBuilder)
//        : base(new object[] { itemBuilder.Item })
//    {
//        ExpectedResult = itemBuilder.ExpectedQuality;
//        TestName = $"{itemBuilder.Item.Name} with quality {itemBuilder.Item.Quality} to be sold in {itemBuilder.Item.SellIn} days should have quality of {itemBuilder.ExpectedQuality} tomorrow";
//    }
//}

internal class ItemTestCaseData : ITestCaseData
{
    public ItemTestCaseData(ItemBuilder itemBuilder)
    {
        Arguments = [itemBuilder.Item];
        ExpectedResult = itemBuilder.ExpectedQuality;
        TestName = $"{itemBuilder.Item.Name} with quality {itemBuilder.Item.Quality} to be sold in {itemBuilder.Item.SellIn} days should have quality of {itemBuilder.ExpectedQuality} tomorrow";
    }

    public object[] Arguments { get; private set; }

    public object ExpectedResult { get; private set; }

    public bool HasExpectedResult { get { return true; } }

    public IPropertyBag Properties { get; private set; }

    public RunState RunState { get { return RunState.Runnable; } }

    public string TestName { get; private set; }
}