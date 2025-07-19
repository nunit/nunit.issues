
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LastVersion.Tests
{
    /// <summary>
    /// Abstract base class that provides client credentials configuration tests.
    /// This simulates the nuget package scenario.
    /// </summary>
    public abstract class ClientCredentialTestBase
    {
        protected List<ClientCredentialsConfiguration> ClientCredentialsConfigurationForTests { get; } = new();
        protected ClientCredentialsConfiguration? clientCredentialsConfigurationForProduction;

        [Test]
        public void ThatClientIdForProductionIsDifferentThanTest()
        {
            // This is the EXACT test that fails in production
            var clientIds = ClientCredentialsConfigurationForTests.DistinctBy(o => o.ClientId);

            // This line should throw MissingMethodException due to version mismatch
            Assert.That(clientCredentialsConfigurationForProduction!.ClientId,
                Is.Not.EqualTo(clientIds.First().ClientId),
                "ClientId for production is equal to clientId used for tests");
        }

        [Test]
        public void Test_Show_Version_Info()
        {
            var thisAssembly = typeof(ClientCredentialTestBase).Assembly;
            var nunitRef = thisAssembly.GetReferencedAssemblies()
                .FirstOrDefault(a => a.Name?.Equals("nunit.framework", StringComparison.OrdinalIgnoreCase) == true);

            TestContext.Out.WriteLine($"🔍 Base class compiled against NUnit: {nunitRef?.Version}");
            TestContext.Out.WriteLine($"🚀 Currently running NUnit: {typeof(Assert).Assembly.GetName().Version}");

            if (nunitRef != null && !nunitRef.Version.Equals(typeof(Assert).Assembly.GetName().Version))
            {
                TestContext.Out.WriteLine("🚨 VERSION MISMATCH DETECTED!");
            }
        }

        protected virtual void SetupTestData()
        {
            // Setup test data
            ClientCredentialsConfigurationForTests.Clear();
            ClientCredentialsConfigurationForTests.AddRange(new[]
            {
                new ClientCredentialsConfiguration { clientId = "test-client-1" },
                new ClientCredentialsConfiguration { clientId = "test-client-2" },
                new ClientCredentialsConfiguration { clientId = "test-client-3" }
            });

            clientCredentialsConfigurationForProduction = new ClientCredentialsConfiguration
            {
                clientId = "production-client-id"
            };
        }

        [SetUp]
        public void Setup()
        {
            SetupTestData();
        }
    }
}





namespace LastVersion.Tests
{
    // This simulates your internal types in the nuget package
    public partial class ClientCredentialsConfiguration
    {
        public string Authority => authority;
        public string ClientId => clientId;
        public string Scopes => scopes == null ? "" : string.Join(" ", scopes);
        public string PrivateKey => privateJwk;
        public int RefreshTokenAfterMinutes { get; set; } = 8;
        public bool CanFallbackToBearerToken { get; set; } = false;
    }

    public partial class ClientCredentialsConfiguration
    {
        public string clientName { get; set; } = "";
        public string authority { get; set; } = "";
        public string clientId { get; set; } = "";
        public string[] grantTypes { get; set; } = System.Array.Empty<string>();
        public string[] scopes { get; set; } = System.Array.Empty<string>();
        public string secretType { get; set; } = "";
        public string rsaPrivateKey { get; set; } = "";
        public int rsaKeySizeBits { get; set; }
        public string privateJwk { get; set; } = "";
    }
}





namespace LastVersion.Tests
{
    /// <summary>
    /// Abstract base class that provides client credentials configuration tests.
    /// This simulates the nuget package scenario with INTERNAL properties.
    /// </summary>
    public abstract class ClientCredentialTestBase2
    {
        // This is INTERNAL to the package - consuming project can't access it directly
        internal List<ClientCredentialsConfiguration> ClientCredentialsConfigurationForTests { get; } = new();

        // This is also internal
        internal ClientCredentialsConfiguration? clientCredentialsConfigurationForProduction;

        [Test]
        public void ThatClientIdForProductionIsDifferentThanTest()
        {
            // This is the EXACT test that fails in production
            // The key is that this method is compiled in the nuget package (NUnit 4.3.2)
            // but executed in the consuming project (NUnit 4.4.0-beta.1)
            // AND it uses internal types that the consuming project can't see directly

            var clientIds = ClientCredentialsConfigurationForTests.DistinctBy(o => o.ClientId);

            // This line should throw MissingMethodException due to:
            // 1. Version mismatch between compile/runtime
            // 2. Complex generic type resolution with internal types
            // 3. LINQ chain with property access on internal objects
            Assert.That(clientCredentialsConfigurationForProduction!.ClientId,
                Is.Not.EqualTo(clientIds.First().ClientId),
                "ClientId for production is equal to clientId used for tests");
        }

        [Test]
        public void Test_Show_Version_And_Context_Info()
        {
            var thisAssembly = typeof(ClientCredentialTestBase).Assembly;
            var nunitRef = thisAssembly.GetReferencedAssemblies()
                .FirstOrDefault(a => a.Name?.Equals("nunit.framework", StringComparison.OrdinalIgnoreCase) == true);

            TestContext.Out.WriteLine($"🔍 Base class compiled against NUnit: {nunitRef?.Version}");
            TestContext.Out.WriteLine($"🚀 Currently running NUnit: {typeof(Assert).Assembly.GetName().Version}");
            TestContext.Out.WriteLine($"📦 Base class assembly: {thisAssembly.FullName}");
            TestContext.Out.WriteLine($"🏠 Executing assembly: {System.Reflection.Assembly.GetExecutingAssembly().FullName}");

            if (nunitRef != null && !nunitRef.Version.Equals(typeof(Assert).Assembly.GetName().Version))
            {
                TestContext.Out.WriteLine("🚨 VERSION MISMATCH DETECTED!");
                TestContext.Out.WriteLine("   This method was compiled with different NUnit version than runtime!");
            }
        }

        // This setup method is called from the consuming project but sets up internal data
        protected virtual void SetupTestData()
        {
            // Setup internal test data that consuming project can't access directly
            ClientCredentialsConfigurationForTests.Clear();
            ClientCredentialsConfigurationForTests.AddRange(new[]
            {
                new ClientCredentialsConfiguration { clientId = "test-client-1" },
                new ClientCredentialsConfiguration { clientId = "test-client-2" },
                new ClientCredentialsConfiguration { clientId = "test-client-3" }
            });

            clientCredentialsConfigurationForProduction = new ClientCredentialsConfiguration
            {
                clientId = "production-client-id"
            };
        }

        [SetUp]
        public void Setup()
        {
            SetupTestData();
        }


        [Test]
        public void Test_Force_Method_Resolution_Failure()
        {
            TestContext.Out.WriteLine("=== Attempting to Force Method Resolution Issue ===");

            // Try reflection to see exactly what EqualTo methods are available
            var constraintExpressionType = typeof(NUnit.Framework.Constraints.ConstraintExpression);
            var equalToMethods = constraintExpressionType.GetMethods()
                .Where(m => m.Name == "EqualTo")
                .ToArray();

            TestContext.Out.WriteLine($"Found {equalToMethods.Length} EqualTo methods:");
            foreach (var method in equalToMethods)
            {
                var parameters = method.GetParameters();
                var paramString = string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"));
                var genericParams = method.GetGenericArguments();
                var genericString = genericParams.Length > 0 ? $"<{string.Join(", ", genericParams.Select(g => g.Name))}>" : "";
                TestContext.Out.WriteLine($"  {method.ReturnType.Name} {method.Name}{genericString}({paramString})");
            }

            // Try to reproduce the exact failing pattern with detailed type info
            string? prodId = "production-client-id";
            var testConfigs = new[]
            {
                new { ClientId = "test-client-1" },
                new { ClientId = "test-client-2" },
                new { ClientId = "test-client-3" }
            };

            var clientIds = testConfigs.DistinctBy(o => o.ClientId);
            var firstClientId = clientIds.First().ClientId;

            TestContext.Out.WriteLine($"Production ID type: {prodId.GetType().FullName}");
            TestContext.Out.WriteLine($"Test ID type: {firstClientId.GetType().FullName}");
            TestContext.Out.WriteLine($"DistinctBy result type: {clientIds.GetType().FullName}");
            TestContext.Out.WriteLine($"First() result type: {clientIds.First().GetType().FullName}");

            // Now try the assertion that should fail
            try
            {
                Assert.That(prodId!, Is.Not.EqualTo(firstClientId),
                    "ClientId for production is equal to clientId used for tests");
                TestContext.Out.WriteLine("✅ Assertion succeeded");
            }
            catch (System.MissingMethodException ex)
            {
                TestContext.Out.WriteLine($"🎯 MissingMethodException caught: {ex.Message}");
                TestContext.Out.WriteLine($"Target site: {ex.TargetSite}");
                throw;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"⚠️ Other exception: {ex.GetType().Name}: {ex.Message}");
                throw;
            }
        }


    }
}




