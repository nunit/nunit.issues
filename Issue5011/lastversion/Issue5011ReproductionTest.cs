namespace LastVersion.Tests.Issue5011;

/// <summary>
/// Reproduction test for NUnit Issue 5011 - MissingMethodException with Is.Not.EqualTo
/// 
/// Error: Method not found: 'NUnit.Framework.Constraints.EqualConstraint 
/// NUnit.Framework.Constraints.ConstraintExpression.EqualTo(!!0)'
/// 
/// This occurs when:
/// 1. Test assembly compiled with NUnit 4.3.2
/// 2. Runtime uses NUnit 4.4.0-beta.1 
/// 3. Generic type inference with complex LINQ expressions
/// </summary>
[TestFixture]
public class Issue5011ReproductionTest
{
    [Test]
    public void Test_MissingMethodException_With_DistinctBy_And_First()
    {
        // This reproduces the exact pattern from the client code that fails
        string? productionClientId = "production-client-id";
        var testClientIds = new[] { "test-client-1", "test-client-2", "test-client-3" };
        var distinctClientIds = testClientIds.DistinctBy(x => x);

        // This line should trigger MissingMethodException when there's a version mismatch
        // The generic type inference from DistinctBy().First() seems to cause the issue
        Assert.That(productionClientId!, Is.Not.EqualTo(distinctClientIds.First()),
            "Production ClientId should not equal any test ClientId");
    }

    [Test]
    public void Test_MissingMethodException_With_Complex_Linq_Chain()
    {
        // More complex LINQ chain that might trigger the issue
        string? productionValue = "production-value";
        var testValues = new[] { "test-1", "test-2", "test-3", "test-1" } // Intentional duplicate
            .Where(x => x.StartsWith("test"))
            .Select(x => x.ToUpperInvariant())
            .DistinctBy(x => x)
            .OrderBy(x => x);

        // This should also trigger the MissingMethodException
        Assert.That(productionValue!, Is.Not.EqualTo(testValues.First()));
    }

    [Test]
    public void Test_Working_Version_Without_Complex_Generics()
    {
        // This version should work fine (for comparison)
        string productionValue = "production-value";
        string testValue = "test-value";

        // Simple case that doesn't trigger the issue
        Assert.That(productionValue, Is.Not.EqualTo(testValue));
    }

    [Test]
    public void Test_Working_Version_With_Explicit_Type()
    {
        // This version should work fine by avoiding generic inference
        string? productionClientId = "production-client-id";
        var testClientIds = new[] { "test-client-1", "test-client-2", "test-client-3" };
        var distinctClientIds = testClientIds.DistinctBy(x => x);

        // Explicitly type the First() result to avoid generic inference issues
        string firstTestId = distinctClientIds.First();
        Assert.That(productionClientId!, Is.Not.EqualTo(firstTestId));
    }

    [Test]
    public void Test_MissingMethodException_With_Anonymous_Types()
    {
        // Test with anonymous types that might trigger the issue
        var productionConfig = new { ClientId = "prod-123", Environment = "production" };
        var testConfigs = new[]
        {
            new { ClientId = "test-1", Environment = "test" },
            new { ClientId = "test-2", Environment = "test" },
            new { ClientId = "test-1", Environment = "test" } // Duplicate
        };

        var distinctTestConfigs = testConfigs.DistinctBy(x => x.ClientId);

        // This might trigger the MissingMethodException with anonymous types
        Assert.That(productionConfig.ClientId, Is.Not.EqualTo(distinctTestConfigs.First().ClientId));
    }

    /// <summary>
    /// This test specifically targets the scenario described in the issue:
    /// - Nullable reference type (string?)
    /// - Complex LINQ with DistinctBy (newer LINQ method)
    /// - Generic type inference challenges
    /// - Is.Not.EqualTo constraint
    /// </summary>
    [Test]
    public void Test_Issue5011_Exact_Reproduction()
    {
        // Simulate the exact client scenario
        var clientCredentialsConfigurationForProduction = new ClientCredentials
        {
            ClientId = "production-client-credentials"
        };

        // Simulate ClientCredentialsConfigurationForTests.DistinctBy(o => o.ClientId)
        var clientCredentialsConfigurationForTests = new[]
        {
            new { ClientId = "test-client-1" },
            new { ClientId = "test-client-2" },
            new { ClientId = "test-client-3" },
            new { ClientId = "test-client-1" } // Duplicate to make DistinctBy meaningful
        };

        var clientIds = clientCredentialsConfigurationForTests.DistinctBy(o => o.ClientId);

        // This is the exact line that fails in the client code
        Assert.That(clientCredentialsConfigurationForProduction!.ClientId,
            Is.Not.EqualTo(clientIds.First().ClientId),
            "ClientId for production is equal to clientId used for tests");
    }

            [Test]
        public void Test_Show_Actual_Compile_Time_Version()
        {
            // This shows what version this assembly was ACTUALLY compiled against
            var thisAssembly = typeof(NUnitConstraintsTestBase).Assembly;
            var nunitRef = thisAssembly.GetReferencedAssemblies()
                .FirstOrDefault(a => a.Name?.Equals("nunit.framework", StringComparison.OrdinalIgnoreCase) == true);
            
            TestContext.Out.WriteLine($"This assembly location: {thisAssembly.Location}");
            TestContext.Out.WriteLine($"This assembly full name: {thisAssembly.FullName}");
            
            if (nunitRef != null)
            {
                TestContext.Out.WriteLine($"🔍 This assembly was compiled against NUnit: {nunitRef.Version}");
                TestContext.Out.WriteLine($"🔍 Referenced assembly full name: {nunitRef.FullName}");
            }
            else
            {
                TestContext.Out.WriteLine("❌ Could not find NUnit framework reference");
            }
            
            // Compare with currently loaded version
            var loadedVersion = typeof(Assert).Assembly.GetName().Version;
            var loadedFullName = typeof(Assert).Assembly.FullName;
            TestContext.Out.WriteLine($"🚀 Currently loaded NUnit version: {loadedVersion}");
            TestContext.Out.WriteLine($"🚀 Currently loaded assembly: {loadedFullName}");
            
            if (nunitRef != null && !nunitRef.Version.Equals(loadedVersion))
            {
                TestContext.Out.WriteLine("🚨 VERSION MISMATCH DETECTED! This could cause MissingMethodException!");
                TestContext.Out.WriteLine($"   Compiled against: {nunitRef.Version}");
                TestContext.Out.WriteLine($"   Running with: {loadedVersion}");
            }
            else
            {
                TestContext.Out.WriteLine("✅ No version mismatch detected");
            }
        }

    #region Specific MissingMethodException Tests

    [Test]
    public void Test_Force_Generic_Type_Inference_Issue()
    {
        // Try to force the exact generic method resolution that's failing
        string? prodValue = "production";

        // Create a complex generic chain that might confuse method resolution
        var testChain = new[] { "test1", "test2" }
            .AsEnumerable()
            .Cast<object>()
            .Cast<string>()
            .DistinctBy(x => x);

        // This might trigger the specific EqualTo(!!0) method that's missing
        Assert.That(prodValue!, Is.Not.EqualTo(testChain.First()));
    }

    [Test]
    public void Test_Force_Constraint_Expression_Method()
    {
        // Try to explicitly use ConstraintExpression methods
        string? prodValue = "production";
        var testValue = new[] { "test" }.First();

        // Force specific constraint expression usage
        var constraint = Is.Not.EqualTo(testValue);
        Assert.That(prodValue!, constraint);
    }

    [Test]
    public void Test_Complex_Generic_With_Nullable()
    {
        // Combine nullable types with complex generics
        string? nullableProd = "production";
        IEnumerable<string?> nullableTest = new string?[] { "test1", null, "test2" }
            .Where(x => x != null)
            .DistinctBy(x => x);

        // This might trigger the generic resolution issue
        Assert.That(nullableProd!, Is.Not.EqualTo(nullableTest.First()));
    }

    [Test]
    public void Test_Anonymous_Type_Generic_Issue()
    {
        // Use anonymous types which create complex generic scenarios
        var prod = new { Id = "prod", Value = "production" };
        var tests = new[]
        {
                new { Id = "test1", Value = "test" },
                new { Id = "test2", Value = "test" }
            }.DistinctBy(x => x.Id);

        // This might cause issues with generic constraint resolution
        Assert.That(prod.Value, Is.Not.EqualTo(tests.First().Value));
    }

    [Test]
    public void Test_Reflection_To_Find_Missing_Method()
    {
        // Let's see what EqualTo methods are actually available
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

        // Now try the actual assertion that might fail
        string? prodValue = "production";
        var testValues = new[] { "test1", "test2" }.DistinctBy(x => x);
        Assert.That(prodValue!, Is.Not.EqualTo(testValues.First()));
    }

    [Test]
    public void Test_Force_ConstraintExpression_EqualTo_Generic()
    {
        // Try to explicitly trigger the problematic method signature
        string? prodValue = "production";
        var complexType = new[] { "test" }
            .Select(x => new { Value = x })
            .DistinctBy(x => x.Value)
            .Select(x => x.Value);

        // This should call ConstraintExpression.EqualTo<T>(T expected)
        var firstTest = complexType.First();
        Assert.That(prodValue!, Is.Not.EqualTo(firstTest));
    }

    [Test]
    public void Test_Use_Var_To_Force_Inference()
    {
        // Use var everywhere to force the compiler to infer types
        string? prodValue = "production";
        var testData = new[] { "test1", "test2", "test3" };
        var distinctData = testData.DistinctBy(x => x);
        var firstItem = distinctData.First();

        // Let var handle all type inference
        var constraint = Is.Not.EqualTo(firstItem);
        Assert.That(prodValue!, constraint);
    }

    [Test]
    public void Test_Late_Bound_Generic_Resolution()
    {
        // Try to create a scenario where generic resolution happens late
        string? prodValue = "production";

        Func<IEnumerable<string>, string> getFirst = enumerable => enumerable.First();
        var testData = new[] { "test1", "test2" }.DistinctBy(x => x);
        var firstTest = getFirst(testData);

        // This might trigger late-bound generic resolution issues
        Assert.That(prodValue!, Is.Not.EqualTo(firstTest));
    }

    #endregion


}


public class ClientCredentials
{
    public string? ClientId { get; set; }
}