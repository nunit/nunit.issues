using System.Xml.Linq;
using NUnit.Framework;
using NUnitLite;

// Runs the fixture with NUnitLite, then prints the fixture result from TestResult.xml.
// The fixture result is only visible in the NUnit XML, not in dotnet test or Test Explorer.
new AutoRun(typeof(OneTimeTearDownWarningAfterChildFailure).Assembly).Execute(["--noheader", "--result=TestResult.xml"]);

XElement fixture = XDocument.Load("TestResult.xml").Descendants("test-suite")
    .Single(e => (string?)e.Attribute("type") == "TestFixture");

string result = (string?)fixture.Attribute("result") ?? "";
string site = (string?)fixture.Attribute("site") ?? "(none)";
Console.WriteLine();
Console.WriteLine($"Fixture result: result=\"{result}\" site=\"{site}\"");
Console.WriteLine($"Fixture message: {fixture.Element("reason")?.Element("message")?.Value ?? fixture.Element("failure")?.Element("message")?.Value}");
Console.WriteLine(result == "Failed" && site == "Child"
    ? "OK: the failure of the test method is kept."
    : "BUG (issue 5523): the warning from OneTimeTearDown replaced the failure of the test method.");

public class OneTimeTearDownWarningAfterChildFailure
{
    [Test]
    public void Fails() => Assert.Fail("child");

    [OneTimeTearDown]
    public void OneTimeTearDown() => Assert.Warn("ottd warn");
}
