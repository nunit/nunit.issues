using demo.protocol.helper;
using NUnit.Framework;

namespace demo.devenv;

public class WebShopDemoTest
{
    [OneTimeSetUp]
    public void PrepareProductCatalog()
    {
        TestLog.Log("[OneTimeSetUp] add some products to the selling catalog");
    }

    [SetUp]
    public void PrepareShoppingCart()
    {
        TestLog.Log("[SetUp] Set delivery address and payment method for customer user");
    }


    [Test]
    public void SomeTest()
    {
        TestLog.Log("[Test] some testing in the created environment");
    }

    [TearDown]
    public void CleanUserRelatedDataInOtherApps()
    {
        TestLog.Log("[TearDown] clean up custom user related data in other sub systems like payment or delivery");
    }

    [OneTimeTearDown]
    public void CleanProductCatalog()
    {
        TestLog.Log("[OneTimeTearDown] clean up product catalog");
    }
}
