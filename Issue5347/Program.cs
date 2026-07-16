using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Reflection;

Console.WriteLine("Hello, World!");

new NUnitLite.AutoRun().Execute(args);

[TestFixture]
public class MyTests
{
    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Category("ExampleCategory")]
    [Test]
    public void Test2()
    {
        Assert.Pass();
    }

    [Property("Whatever", 42)]
    [Category("ExampleCategoryTwo")]
    [Test]
    public void Test3()
    {
        Assert.Pass();
    }
}

[TestFixture]
public class MyNonTests
{
    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Category("ExampleCategory")]
    [Test]
    public void Test2()
    {
        Assert.Pass();
    }

    [Property("Whatever", 42)]
    [Category("ExampleCategoryTwo")]
    [Test]
    public void Test3()
    {
        Assert.Pass();
    }
}

[SetUpFixture]
public class Init
{
    [OneTimeSetUp]
    public void BeforeAllTests()
    {
        var assemblyContext = TestContext.CurrentContext.Test.Parent;
        foreach (var test in assemblyContext.Tests)
        {
            PrintTestDetails(test);
        }
    }

    // Note that the tests are found on the second level here.
    private static void PrintTestDetails(ITest test)
    {
        if (test.Tests.Count > 0)
        {
            foreach (var child in test.Tests)
            {
                PrintTestDetails(child);
            }

            return;
        }

        TestContext.Progress.WriteLine($"TestName: {test.Name}");
        TestContext.Progress.WriteLine("Properties");
        foreach (var key in test.Properties.Keys)
        {
            var values = test.Properties[key];
            var joinedValues = string.Join(", ", values.Cast<object?>().Select(v => v?.ToString() ?? "<null>"));
            TestContext.Progress.WriteLine($"Key: {key}: Value: {joinedValues}");
        }

        TestContext.Progress.WriteLine("Attributes:");
        if (!string.IsNullOrWhiteSpace(test.ClassName) && !string.IsNullOrWhiteSpace(test.MethodName))
        {
            var method = FindMethod(test.ClassName, test.MethodName);
            if (method != null)
            {
                foreach (var attribute in method.GetCustomAttributes(inherit: true))
                {
                    TestContext.Progress.WriteLine($"Attribute: {FormatAttribute(attribute)}");
                }
            }
        }
    }

    private static string FormatAttribute(object attribute)
    {
        var parts = attribute.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0 && p.Name != nameof(Attribute.TypeId))
            .Select(p =>
            {
                var value = p.GetValue(attribute);
                if (value is System.Collections.IEnumerable enumerable && value is not string)
                {
                    var values = enumerable.Cast<object?>().Select(v => v?.ToString() ?? "<null>");
                    return $"{p.Name}=[{string.Join(", ", values)}]";
                }

                return $"{p.Name}={value ?? "<null>"}";
            });

        return $"{attribute.GetType().FullName} ({string.Join(", ", parts)})";
    }

    private static MethodInfo? FindMethod(string className, string methodName)
    {
        var type = Type.GetType(className);
        if (type == null)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(className);
                if (type != null)
                {
                    break;
                }
            }
        }

        return type?.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .FirstOrDefault(m => m.Name == methodName);
    }

    [OneTimeTearDown]
    public void AfterAllTests()
    {
    }
}