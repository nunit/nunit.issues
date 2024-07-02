
using NUnit.Framework;

namespace Issue4696.TestMain
{
    [TestFixture]
    public class MainTests
    {
        [Test]
        public void Run()
        {
            TestProject1.TestUtility.Run(nameof(MainTests)); // Ensure that both test projects
            TestProject2.TestUtility.Run(nameof(MainTests)); // are referred to by this main.
        }
    }



    public class T1 : Issue4696.TestProject1.Tests
    {

    }

    public class T2 : Issue4696.TestProject2.Tests
    {

    }



}
