namespace Issue3148;

using System.IO;
using System.Reflection;
using NUnit.Framework;

public class Tests
{
    [TestCaseSource(nameof(TestDirectorySource))]
    public void Test1(string testDirectory)
    {
        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Assert.That(assemblyDirectory, Is.EqualTo(testDirectory));
    }

    public static IEnumerable<string> TestDirectorySource
    {
        get { yield return TestContext.CurrentContext.TestDirectory; }
    }
}

