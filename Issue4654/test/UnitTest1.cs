using Microsoft.Extensions.Logging;
using mvc.Controllers;
using NSubstitute;

namespace test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var logger = Substitute.For<ILogger<HomeController>>();
        var sut =  new HomeController(logger);
        Assert.That(sut, Is.Not.Null);
    }
}