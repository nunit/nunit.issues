using LastVersion.Tests;
using LastVersion.Tests.Issue5011;

namespace currentversion
{

    [TestFixture]
    public class Tests : NUnitConstraintsTestBase
    {

    }

    public class ReproTests : Issue5011ReproductionTest
    {

    }

}


namespace CurrentVersion.Tests
{
    /// <summary>
    /// Concrete test class that inherits from the abstract base class.
    /// This simulates the consuming project scenario.
    /// </summary>
    [TestFixture]
    public class ConcreteClientCredentialTest : ClientCredentialTestBase
    {
        // This inherits the ThatClientIdForProductionIsDifferentThanTest() method
        // which should fail with MissingMethodException when called from this context

        [Test]
        public void Test_Current_Version_Info()
        {
            TestContext.Out.WriteLine($"Concrete class running with NUnit: {typeof(Assert).Assembly.GetName().Version}");
        }
    }

    [TestFixture]
    public class ConcreteClientCredentialTest2 : ClientCredentialTestBase2
    {
        // This inherits the ThatClientIdForProductionIsDifferentThanTest() method
        // which should fail with MissingMethodException when called from this context
        [Test]
        public void Test_Current_Version_Info()
        {
            TestContext.Out.WriteLine($"Concrete class running with NUnit: {typeof(Assert).Assembly.GetName().Version}");
        }
    }
}
