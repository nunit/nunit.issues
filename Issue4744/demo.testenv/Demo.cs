using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnitLite;
using System.Text;


/* ----------------------------------------------------------------
 * Create a HookExtenstionAttribute to setup the test environment
 * and enable the registration by a IApplyToContext attribute
 * ----------------------------------------------------------------
*/

internal class TestEnvHookExtensionAttribute : NUnitAttribute, IApplyToContext
{

    public void ApplyToContext(TestExecutionContext context)
    {

        context.HookExtension.BeforeAnySetUps += (context1, setUpMethod)
            => Execute.PrepareTestDatabase();

        context.HookExtension.BeforeTest += (context1, testMethod) 
            => Execute.PrepareApplicationForTesting();

        context.HookExtension.AfterTest += (context1, testMethod)
            => Execute.CleanupUserSpecificApplicationData();

        context.HookExtension.AfterAnyTearDowns += (context1, tearDownMethod)
            => Execute.CleanupTestDatabase();
    }
}

/* -------------------------------------------------
 * Test with HookExtension working in the background
 * -------------------------------------------------
 */

[TestEnvHookExtension]
public class WebShopDemoTest
{
    [OneTimeSetUp]
    public void PrepareProductCatalog()
    {
        Execute.MethodOfTestFixutre("[OneTimeSetUp] add some products to the selling catalog");
    }

    [Test]
    public void SomeTest()
    {
        Execute.MethodOfTestFixutre("[Test] some testing in the created environment");
    }

    [OneTimeTearDown]
    public void CleanProductCatalog()
    {
        Execute.MethodOfTestFixutre("[OneTimeTearDown] clean up product catalog");
    }
}

/* ----------------------------------------
 * Test Execution by NUnitLite
 * ----------------------------------------
 */

static class Program
{
    static int Main(string[] args)
    {
        try
        {
            return new AutoRun().Execute(args);
        }
        finally
        {
            Console.WriteLine("\n================================   Test Log   =============================");
            Console.WriteLine(Execute.ActionLog.ToString());
            Console.WriteLine("===========================================================================");
        }
    }
}

/*
Execution flow of the test:

================================   Test Log   =============================
[TEST_ENVIRONMENT] Initiate Database (on demand), load schemas (on demand), create users, login as admin
  ├--> [OneTimeSetUp] add some products to the selling catalog
[TEST_ENVIRONMENT] Login as demo user and open shopping application
  ├--> [Test] some testing in the created environment
[TEST_ENVIRONMENT] Close shopping application and clean up test user specific data
  ├--> [OneTimeTearDown] clean up product catalog
[TEST_ENVIRONMENT] Clean up database environment
=========================================================================== */


/* -------------------------------------------------------------------------
 * Test Helper: Simulation of test actions and environment preparation steps
 * -------------------------------------------------------------------------
 */
internal static class Execute
{
    public static StringBuilder ActionLog { get; } = new StringBuilder();

    public static void MethodOfTestFixutre(string message)
    {
        ActionLog.AppendLine($"   ├--> {message}");
    }

    public static void PrepareTestDatabase()
    {
        ActionLog.AppendLine($"[TEST_ENVIRONMENT] Initiate Database (on demand), load schemas (on demand), create users, login as admin");
    }

    public static void CreateCustomer()
    {
        ActionLog.AppendLine($"[TEST_ENVIRONMENT] Create Customer User and login");
    }

    public static void PrepareApplicationForTesting()
    {
        ActionLog.AppendLine($"[TEST_ENVIRONMENT] Login as demo user and open shopping application");
    }

    public static void CleanupUserSpecificApplicationData()
    {
        ActionLog.AppendLine($"[TEST_ENVIRONMENT] Close shopping application and clean up test user specific data");
    }

    public static void CleanupTestDatabase()
    {
        ActionLog.AppendLine($"[TEST_ENVIRONMENT] Clean up database environment");
    }
}