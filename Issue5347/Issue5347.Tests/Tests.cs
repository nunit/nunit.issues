using NUnit.Framework;
using NUnit.Framework.Interfaces;

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


public class UnitTest1
{
    public class DataProvider<T> where T : TestData, new()
    {
        public static IEnumerable<TestCaseData> GetData()
        {
            string[] names = new string[] { "Matthew", "Kelly", "Shawn" };
            char[] genders = new char[] { 'M', 'F', 'M' };
            int[] ages = new int[] { 35, 34, 30 };

            for (int i = 0; i < names.Length; i++)
            {
                TestData testData = new TestData()
                {
                    Name = names[i],
                    Gender = genders[i],
                    Age = ages[i]
                };

                TestCaseData tcd = new TestCaseData(testData);
                tcd.SetName("{m}("+ testData.Name + "-" + testData.Gender + "-" + testData.Age +")");

                yield return tcd;
            }
        }
    }


    [Test]
    [TestCaseSource(typeof(DataProvider<TestData>), "GetData")]
    public void Persons(TestData test)
    {
        Assert.IsTrue(test.Name == "Matthew");
    }
}

public class TestData
{
    public string Name { get; init; }
    public char Gender { get; init; }
    public int Age { get; init; }
}