using System.Reflection;

namespace currentversion
{
    /// <summary>
    /// Abstract base class demonstrating all NUnit constraints from the reference documentation.
    /// Each test method validates one specific constraint to ensure it works as expected.
    /// All tests are designed to pass, providing a comprehensive example of constraint usage.
    /// </summary>
    public class NUnitConstraintsTestBaseInCurrentVersion
    {

        // Compile-time version information
        public static readonly string CompiledWithNUnitVersion =
            typeof(Assert).Assembly.GetName().Version?.ToString() ?? "Unknown";

        public static readonly string CompiledWithNUnitFullName = typeof(Assert).Assembly.FullName ?? "Unknown";

        [Test]
        public void Test_Show_Compile_Time_NUnit_Version()
        {
            TestContext.Out.WriteLine($"This class was compiled with NUnit version: {CompiledWithNUnitVersion}");
            TestContext.Out.WriteLine($"Full assembly name: {CompiledWithNUnitFullName}");
        }

        [Test]
        public void Test_Show_NUnit_Runtime_Version_Info()
        {
            // Get the NUnit Framework assembly
            var nunitAssembly = typeof(Assert).Assembly;

            // Get version information
            var assemblyName = nunitAssembly.GetName();
            var version = assemblyName.Version;
            var fullName = nunitAssembly.FullName;
            var location = nunitAssembly.Location;

            // Output to test console
            TestContext.Out.WriteLine($"NUnit Assembly Full Name: {fullName}");
            TestContext.Out.WriteLine($"NUnit Version: {version}");
            TestContext.Out.WriteLine($"NUnit Assembly Location: {location}");

            // Also get file version if available
            var fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(nunitAssembly.Location);
            TestContext.Out.WriteLine($"NUnit File Version: {fileVersionInfo.FileVersion}");
            TestContext.Out.WriteLine($"NUnit Product Version: {fileVersionInfo.ProductVersion}");

            // Get informational version from assembly attributes
            var informationalVersion = nunitAssembly
                .GetCustomAttribute<System.Reflection.AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            TestContext.Out.WriteLine($"NUnit Informational Version: {informationalVersion}");
        }


        [Test]
        public void Test_Show_Actual_Compile_Time_Version()
        {
            // This shows what version this assembly was ACTUALLY compiled against
            var thisAssembly = typeof(NUnitConstraintsTestBaseInCurrentVersion).Assembly;
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



        #region Core Assert Methods Tests

        [Test]
        public void Test_Assert_That_Boolean_Condition()
        {
            // Test: Assert.That(condition) - Asserts that a boolean condition is true
            Assert.That(2 + 2 == 4);
        }

        [Test]
        public void Test_Assert_Multiple()
        {
            // Test: Assert.Multiple(action) - Groups multiple assertions to run all and report failures together
            Assert.Multiple(() =>
            {
                Assert.That(1 + 1, Is.EqualTo(2));
                Assert.That("hello", Is.EqualTo("hello"));
                Assert.That(true, Is.True);
            });
        }

        #endregion


        #region Repro tests

        [Test]
        public void Test_Repro_Bug_With_First()
        {
            // Mimicking your scenario with simpler variables
            string? productionClientId = "prod-client-123";
            var testClientIds = new[] { "test-client-1", "test-client-2", "test-client-3" };
            var distinctClientIds = testClientIds.DistinctBy(o => o);

            // This line mimics your failing assertion
            Assert.That(productionClientId!, Is.Not.EqualTo(distinctClientIds.First()),
                "Production ClientId should not equal any test ClientId");
        }

        [Test]
        public void Test_Repro_Bug_With_Generic_Type()
        {
            // Test with generic type constraint that might expose the issue
            string? productionValue = "prod-value";
            var testValues = new[] { "test-1", "test-2", "test-3" }.AsEnumerable();

            // Force generic type resolution
            Assert.That(productionValue!, Is.Not.EqualTo(testValues.First()), "Should not be equal");
        }

        [Test]
        public void Test_Repro_Bug_With_Nullable_Reference()
        {
            // Test nullable reference type specifically
            string? nullableString = "production";
            var collection = new List<string> { "test1", "test2" };

            // This might trigger the generic type resolution issue
            Assert.That(nullableString!, Is.Not.EqualTo(collection.FirstOrDefault()), "Should not match");
        }

        [Test]
        public void Test_Repro_Bug_Complex_Generic()
        {
            // More complex scenario that might trigger the generic constraint issue
            var production = new { ClientId = "prod-123" };
            var testConfigs = new[]
            {
                new { ClientId = "test-1" },
                new { ClientId = "test-2" }
            };

            // This uses anonymous types which might cause generic resolution issues
            Assert.That(production.ClientId, Is.Not.EqualTo(testConfigs.First().ClientId),
                "ClientIds should be different");
        }

        #endregion

        #region Additional Repro tests for MissingMethodException

        [Test]
        public void Test_Repro_With_Explicit_Type_Inference()
        {
            // Force the compiler to infer generic types in a complex way
            string? productionValue = "prod";
            var testValues = new[] { "test1", "test2" }.Select(x => x);

            // This might cause generic type resolution issues
            Assert.That(productionValue!, Is.Not.EqualTo(testValues.First()));
        }

        [Test]
        public void Test_Repro_With_IEnumerable_Chain()
        {
            // Chain multiple LINQ operations to create complex generic types
            string? prodId = "production-id";
            var testIds = new[] { "test-1", "test-2", "test-3" }
                .Where(x => x.StartsWith("test"))
                .Select(x => x.ToUpper())
                .DistinctBy(x => x);

            Assert.That(prodId!, Is.Not.EqualTo(testIds.First()));
        }

        [Test]
        public void Test_Repro_With_Var_And_Complex_Linq()
        {
            // Use var to let compiler infer types
            string? prodClientId = "prod-client";
            var complexQuery = new[] { "test-1", "test-2", "test-3" }
                .Select(x => new { Id = x, Length = x.Length })
                .Where(x => x.Length > 0)
                .Select(x => x.Id)
                .DistinctBy(x => x);

            Assert.That(prodClientId!, Is.Not.EqualTo(complexQuery.First()));
        }

        [Test]
        public void Test_Repro_With_Nested_Generic_Types()
        {
            // Use nested generic types that might confuse the constraint system
            string? prodValue = "production";
            var nestedData = new List<List<string>>
            {
                new List<string> { "test-1a", "test-1b" },
                new List<string> { "test-2a", "test-2b" }
            };

            var flatData = nestedData.SelectMany(x => x).DistinctBy(x => x);
            Assert.That(prodValue!, Is.Not.EqualTo(flatData.First()));
        }

        [Test]
        public void Test_Repro_With_Dynamic_Like_Object()
        {
            // Use object that might cause type resolution issues
            string? prodId = "prod-123";
            var testData = new[]
            {
                new { ClientId = "test-1", Type = "test" },
                new { ClientId = "test-2", Type = "test" }
            };

            var clientIds = testData.Select(x => x.ClientId).DistinctBy(x => x);
            Assert.That(prodId!, Is.Not.EqualTo(clientIds.First()));
        }

        [Test]
        public void Test_Repro_With_Multiple_Generic_Constraints()
        {
            // Multiple generic constraints in same test
            string? prodValue = "production";
            var testData = new Dictionary<string, string>
            {
                { "test1", "value1" },
                { "test2", "value2" }
            };

            var keys = testData.Keys.DistinctBy(x => x);
            var values = testData.Values.DistinctBy(x => x);

            // Multiple assertions that might trigger the issue
            Assert.That(prodValue!, Is.Not.EqualTo(keys.First()));
            Assert.That(prodValue!, Is.Not.EqualTo(values.First()));
        }

        [Test]
        public void Test_Repro_With_Func_And_Generics()
        {
            // Use Func with generics that might cause issues
            string? prodId = "prod";
            Func<string[], IEnumerable<string>> processor = arr => arr.DistinctBy(x => x);
            var testIds = new[] { "test1", "test2", "test3" };

            var processedIds = processor(testIds);
            Assert.That(prodId!, Is.Not.EqualTo(processedIds.First()));
        }

        [Test]
        public void Test_Repro_Assembly_Load_Context()
        {
            // Test that might expose assembly loading issues
            string? prodValue = "production";
            var assembly = typeof(string).Assembly;
            var typeName = assembly.GetType("System.String");

            var testValues = new[] { "test1", "test2" }.AsQueryable().DistinctBy(x => x);
            Assert.That(prodValue!, Is.Not.EqualTo(testValues.First()));
        }

        #endregion

        #region Equality Constraints Tests

        [Test]
        public void Test_Is_EqualTo()
        {
            // Test: Is.EqualTo(expected) - Tests for equality
            Assert.That(42, Is.EqualTo(42));
            Assert.That("hello", Is.EqualTo("hello"));
            Assert.That(3.14, Is.EqualTo(3.14));
        }

        [Test]
        public void Test_Is_Not_EqualTo()
        {
            // Test: Is.Not.EqualTo(expected) - Tests for inequality
            Assert.That(42, Is.Not.EqualTo(43));
            Assert.That("hello", Is.Not.EqualTo("world"));
        }

        [Test]
        public void Test_Is_SameAs()
        {
            // Test: Is.SameAs(expected) - Tests for reference equality
            var obj = new object();
            Assert.That(obj, Is.SameAs(obj));
        }

        [Test]
        public void Test_Is_Not_SameAs()
        {
            // Test: Is.Not.SameAs(expected) - Tests for reference inequality
            var obj1 = new object();
            var obj2 = new object();
            Assert.That(obj1, Is.Not.SameAs(obj2));
        }

        #endregion

        #region Comparison Constraints Tests

        [Test]
        public void Test_Is_GreaterThan()
        {
            // Test: Is.GreaterThan(expected) - Tests for greater than
            Assert.That(10, Is.GreaterThan(5));
            Assert.That(3.14, Is.GreaterThan(2.0));
        }

        [Test]
        public void Test_Is_GreaterThanOrEqualTo()
        {
            // Test: Is.GreaterThanOrEqualTo(expected) - Tests for greater than or equal
            Assert.That(10, Is.GreaterThanOrEqualTo(10));
            Assert.That(15, Is.GreaterThanOrEqualTo(10));
        }

        [Test]
        public void Test_Is_AtLeast()
        {
            // Test: Is.AtLeast(expected) - Alias for GreaterThanOrEqualTo
            Assert.That(10, Is.AtLeast(10));
            Assert.That(15, Is.AtLeast(10));
        }

        [Test]
        public void Test_Is_LessThan()
        {
            // Test: Is.LessThan(expected) - Tests for less than
            Assert.That(5, Is.LessThan(10));
            Assert.That(2.0, Is.LessThan(3.14));
        }

        [Test]
        public void Test_Is_LessThanOrEqualTo()
        {
            // Test: Is.LessThanOrEqualTo(expected) - Tests for less than or equal
            Assert.That(10, Is.LessThanOrEqualTo(10));
            Assert.That(5, Is.LessThanOrEqualTo(10));
        }

        [Test]
        public void Test_Is_AtMost()
        {
            // Test: Is.AtMost(expected) - Alias for LessThanOrEqualTo
            Assert.That(10, Is.AtMost(10));
            Assert.That(5, Is.AtMost(10));
        }

        #endregion

        #region Boolean Constraints Tests

        [Test]
        public void Test_Is_True()
        {
            // Test: Is.True - Tests for true value
            Assert.That(true, Is.True);
            Assert.That(2 > 1, Is.True);
        }

        [Test]
        public void Test_Is_False()
        {
            // Test: Is.False - Tests for false value
            Assert.That(false, Is.False);
            Assert.That(1 > 2, Is.False);
        }

        #endregion

        #region Null Constraints Tests

        [Test]
        public void Test_Is_Null()
        {
            // Test: Is.Null - Tests for null value
            string nullString = null;
            Assert.That(nullString, Is.Null);
        }

        [Test]
        public void Test_Is_Not_Null()
        {
            // Test: Is.Not.Null - Tests for non-null value
            string notNullString = "hello";
            Assert.That(notNullString, Is.Not.Null);
        }

        #endregion

        #region Type Constraints Tests

        [Test]
        public void Test_Is_TypeOf_Generic()
        {
            // Test: Is.TypeOf<T>() - Tests for exact type
            Assert.That("hello", Is.TypeOf<string>());
            Assert.That(42, Is.TypeOf<int>());
        }

        [Test]
        public void Test_Is_TypeOf_Type()
        {
            // Test: Is.TypeOf(type) - Tests for exact type
            Assert.That("hello", Is.TypeOf(typeof(string)));
            Assert.That(42, Is.TypeOf(typeof(int)));
        }

        [Test]
        public void Test_Is_InstanceOf_Generic()
        {
            // Test: Is.InstanceOf<T>() - Tests for type or derived type
            Assert.That("hello", Is.InstanceOf<string>());
            Assert.That("hello", Is.InstanceOf<object>());
        }

        [Test]
        public void Test_Is_InstanceOf_Type()
        {
            // Test: Is.InstanceOf(type) - Tests for type or derived type
            Assert.That("hello", Is.InstanceOf(typeof(string)));
            Assert.That("hello", Is.InstanceOf(typeof(object)));
        }

        [Test]
        public void Test_Is_AssignableFrom_Generic()
        {
            Assert.That("Hello", Is.AssignableFrom(typeof(string)));
            Assert.That(5, Is.Not.AssignableFrom(typeof(string)));
        }

        [Test]
        public void Test_Is_AssignableTo_Generic()
        {
            // Test: Is.AssignableTo<T>() - Tests if type is assignable to
            Assert.That(typeof(string), Is.AssignableTo<object>());
        }

        [Test]
        public void Test_Is_AssignableTo_Type()
        {
            // Test: Is.AssignableTo(type) - Tests if type is assignable to
            Assert.That(typeof(string), Is.AssignableTo(typeof(object)));
        }

        #endregion

        #region Range and Pattern Constraints Tests

        [Test]
        public void Test_Is_InRange()
        {
            // Test: Is.InRange(from, to) - Tests if value is in range
            Assert.That(5, Is.InRange(1, 10));
            Assert.That(3.5, Is.InRange(1.0, 10.0));
        }

        [Test]
        public void Test_Is_Zero()
        {
            // Test: Is.Zero - Tests for zero value
            Assert.That(0, Is.Zero);
            Assert.That(0.0, Is.Zero);
        }

        [Test]
        public void Test_Is_Positive()
        {
            // Test: Is.Positive - Tests for positive value
            Assert.That(5, Is.Positive);
            Assert.That(3.14, Is.Positive);
        }

        [Test]
        public void Test_Is_Negative()
        {
            // Test: Is.Negative - Tests for negative value
            Assert.That(-5, Is.Negative);
            Assert.That(-3.14, Is.Negative);
        }

        [Test]
        public void Test_Is_NaN()
        {
            // Test: Is.NaN - Tests for NaN (Not a Number)
            Assert.That(double.NaN, Is.NaN);
            Assert.That(float.NaN, Is.NaN);
        }





        #endregion

        #region Collection Constraints Tests

        [Test]
        public void Test_Is_Empty_Collection()
        {
            // Test: Is.Empty - Tests for empty collection
            var emptyList = new List<int>();
            Assert.That(emptyList, Is.Empty);
        }

        [Test]
        public void Test_Is_Not_Empty_Collection()
        {
            // Test: Is.Not.Empty - Tests for non-empty collection
            var list = new List<int> { 1, 2, 3 };
            Assert.That(list, Is.Not.Empty);
        }

        [Test]
        public void Test_Is_Unique()
        {
            // Test: Is.Unique - Tests that all items in collection are unique
            var uniqueList = new List<int> { 1, 2, 3, 4, 5 };
            Assert.That(uniqueList, Is.Unique);
        }

        [Test]
        public void Test_Is_Ordered()
        {
            // Test: Is.Ordered - Tests that collection is ordered
            var orderedList = new List<int> { 1, 2, 3, 4, 5 };
            Assert.That(orderedList, Is.Ordered);
        }

        [Test]
        public void Test_Is_EquivalentTo()
        {
            // Test: Is.EquivalentTo(expected) - Tests collections have same items (any order)
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 3, 1, 2 };
            Assert.That(list1, Is.EquivalentTo(list2));
        }

        [Test]
        public void Test_Is_SubsetOf()
        {
            // Test: Is.SubsetOf(expected) - Tests collection is subset of expected
            var subset = new List<int> { 1, 2 };
            var superset = new List<int> { 1, 2, 3, 4, 5 };
            Assert.That(subset, Is.SubsetOf(superset));
        }

        [Test]
        public void Test_Is_SupersetOf()
        {
            // Test: Is.SupersetOf(expected) - Tests collection is superset of expected
            var superset = new List<int> { 1, 2, 3, 4, 5 };
            var subset = new List<int> { 1, 2 };
            Assert.That(superset, Is.SupersetOf(subset));
        }

        [Test]
        public void Test_Has_Count_EqualTo()
        {
            // Test: Has.Count.EqualTo(expected) - Tests collection count
            var list = new List<int> { 1, 2, 3 };
            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void Test_Has_Length_EqualTo()
        {
            // Test: Has.Length.EqualTo(expected) - Tests collection/string length
            var array = new int[] { 1, 2, 3, 4, 5 };
            var text = "hello";
            Assert.That(array, Has.Length.EqualTo(5));
            Assert.That(text, Has.Length.EqualTo(5));
        }

        [Test]
        public void Test_Has_Some_EqualTo()
        {
            // Test: Has.Some.EqualTo(expected) - Tests collection contains item
            var list = new List<int> { 1, 2, 3, 4, 5 };
            Assert.That(list, Has.Some.EqualTo(3));
        }

        [Test]
        public void Test_Has_All_GreaterThan()
        {
            // Test: Has.All.EqualTo(expected) - Tests all items equal expected (using GreaterThan for variety)
            var list = new List<int> { 5, 6, 7, 8, 9 };
            Assert.That(list, Has.All.GreaterThan(4));
        }

        [Test]
        public void Test_Has_None_EqualTo()
        {
            // Test: Has.None.EqualTo(expected) - Tests no items equal expected
            var list = new List<int> { 1, 2, 3, 4, 5 };
            Assert.That(list, Has.None.EqualTo(10));
        }

        [Test]
        public void Test_Has_Member()
        {
            // Test: Has.Member(expected) - Tests collection contains member
            var list = new List<string> { "apple", "banana", "cherry" };
            Assert.That(list, Has.Member("banana"));
        }

        [Test]
        public void Test_Has_No_Member()
        {
            // Test: Has.No.Member(expected) - Tests collection doesn't contain member
            var list = new List<string> { "apple", "banana", "cherry" };
            Assert.That(list, Has.No.Member("orange"));
        }

        [Test]
        public void Test_Contains_Item()
        {
            // Test: Contains.Item(expected) - Tests collection contains item
            var list = new List<string> { "apple", "banana", "cherry" };
            Assert.That(list, Contains.Item("banana"));
        }

        [Test]
        public void Test_Does_Contain_Collection()
        {
            // Test: Does.Contain(expected) - Tests collection contains item
            var list = new List<string> { "apple", "banana", "cherry" };
            Assert.That(list, Does.Contain("banana"));
        }

        [Test]
        public void Test_Does_Not_Contain_Collection()
        {
            // Test: Does.Not.Contain(expected) - Tests collection doesn't contain item
            var list = new List<string> { "apple", "banana", "cherry" };
            Assert.That(list, Does.Not.Contain("orange"));
        }

        #endregion

        #region String Constraints Tests

        [Test]
        public void Test_Does_StartWith()
        {
            // Test: Does.StartWith(expected) - Tests string starts with
            Assert.That("Hello World", Does.StartWith("Hello"));
        }

        [Test]
        public void Test_Does_Not_StartWith()
        {
            // Test: Does.Not.StartWith(expected) - Tests string doesn't start with
            Assert.That("Hello World", Does.Not.StartWith("World"));
        }

        [Test]
        public void Test_Does_EndWith()
        {
            // Test: Does.EndWith(expected) - Tests string ends with
            Assert.That("Hello World", Does.EndWith("World"));
        }

        [Test]
        public void Test_Does_Not_EndWith()
        {
            // Test: Does.Not.EndWith(expected) - Tests string doesn't end with
            Assert.That("Hello World", Does.Not.EndWith("Hello"));
        }

        [Test]
        public void Test_Does_Contain_String()
        {
            // Test: Does.Contain(expected) - Tests string contains substring
            Assert.That("Hello World", Does.Contain("lo Wo"));
        }

        [Test]
        public void Test_Does_Not_Contain_String()
        {
            // Test: Does.Not.Contain(expected) - Tests string doesn't contain substring
            Assert.That("Hello World", Does.Not.Contain("xyz"));
        }

        [Test]
        public void Test_Does_Match()
        {
            // Test: Does.Match(pattern) - Tests string matches regex pattern
            Assert.That("123-45-6789", Does.Match(@"\d{3}-\d{2}-\d{4}"));
        }

        [Test]
        public void Test_Does_Not_Match()
        {
            // Test: Does.Not.Match(pattern) - Tests string doesn't match regex pattern
            Assert.That("Hello World", Does.Not.Match(@"\d{3}-\d{2}-\d{4}"));
        }

        #endregion

        #region Property Constraints Tests

        [Test]
        public void Test_Has_Property_EqualTo()
        {
            // Test: Has.Property(name).EqualTo(value) - Tests property value
            var person = new { Name = "John", Age = 30 };
            Assert.That(person, Has.Property("Name").EqualTo("John"));
            Assert.That(person, Has.Property("Age").EqualTo(30));
        }

        [Test]
        public void Test_Has_Property_Null()
        {
            // Test: Has.Property(name).Null - Tests property is null
            var obj = new { Name = (string)null };
            Assert.That(obj, Has.Property("Name").Null);
        }

        [Test]
        public void Test_Has_Property_Not_Null()
        {
            // Test: Has.Property(name).Not.Null - Tests property is not null
            var obj = new { Name = "John" };
            Assert.That(obj, Has.Property("Name").Not.Null);
        }

        #endregion

        #region Exception Constraints Tests

        [Test]
        public void Test_Throws_Exception()
        {
            // Test: Throws.Exception - Tests that exception is thrown
            Assert.That(() => throw new InvalidOperationException(), Throws.Exception);
        }

        [Test]
        public void Test_Throws_TypeOf_Generic()
        {
            // Test: Throws.TypeOf<T>() - Tests specific exception type thrown
            Assert.That(() => throw new ArgumentException(), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Test_Throws_InstanceOf_Generic()
        {
            // Test: Throws.InstanceOf<T>() - Tests exception type or derived thrown
            Assert.That(() => throw new ArgumentNullException(), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public void Test_Throws_ArgumentException()
        {
            // Test: Throws.ArgumentException - Tests ArgumentException thrown
            Assert.That(() => throw new ArgumentException(), Throws.ArgumentException);
        }

        [Test]
        public void Test_Throws_ArgumentNullException()
        {
            // Test: Throws.ArgumentNullException - Tests ArgumentNullException thrown
            Assert.That(() => throw new ArgumentNullException(), Throws.ArgumentNullException);
        }

        [Test]
        public void Test_Throws_InvalidOperationException()
        {
            // Test: Throws.InvalidOperationException - Tests InvalidOperationException thrown
            Assert.That(() => throw new InvalidOperationException(), Throws.InvalidOperationException);
        }

        [Test]
        public void Test_Throws_Nothing()
        {
            // Test: Throws.Nothing - Tests that no exception is thrown
            Assert.That(() =>
            {
                var x = 1 + 1;
            }, Throws.Nothing);
        }

        #endregion

        #region File/Directory Constraints Tests

        [Test]
        public void Test_Does_Exist()
        {
            // Test: Does.Exist - Tests file or directory exists
            // Create a temporary file for testing
            var tempFile = Path.GetTempFileName();
            try
            {
                Assert.That(tempFile, Does.Exist);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Test]
        public void Test_Does_Not_Exist()
        {
            // Test: Does.Not.Exist - Tests file or directory doesn't exist
            var nonExistentFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Assert.That(nonExistentFile, Does.Not.Exist);
        }

        #endregion

        #region Constraint Combination Tests

        [Test]
        public void Test_Constraint_And_Operator()
        {
            // Test: And operator - Combining constraints
            Assert.That(5, Is.GreaterThan(0).And.LessThan(10));
        }

        [Test]
        public void Test_Constraint_Or_Operator()
        {
            // Test: Or operator - Alternative constraints
            Assert.That("hello", Is.EqualTo("hello").Or.EqualTo("world"));
        }

        [Test]
        public void Test_Constraint_Modifiers()
        {
            // Test: Constraint modifiers - Case sensitivity, tolerance, etc.
            Assert.That("HELLO", Is.EqualTo("hello").IgnoreCase);
            Assert.That(3.14159, Is.EqualTo(3.14).Within(0.01));
            Assert.That("Hello World", Does.Contain("HELLO").IgnoreCase);
        }

        #endregion

        #region Async Exception Tests

        [Test]
        public async Task Test_Async_Throws_Exception()
        {
            // Test: Async exception throwing
            Assert.That(async () => await ThrowAsync(), Throws.InvalidOperationException);
        }

        [Test]
        public async Task Test_Async_Throws_Nothing()
        {
            // Test: Async no exception
            Assert.That(async () => await DoNothingAsync(), Throws.Nothing);
        }

        private async Task ThrowAsync()
        {
            await Task.Delay(1);
            throw new InvalidOperationException("Test exception");
        }

        private async Task DoNothingAsync()
        {
            await Task.Delay(1);
        }

        #endregion


        #region Using classes

        [Test]
        public void TestGuid()
        {
            Guid g = Guid.NewGuid();
            Guid gg = g;
            Assert.That(gg, Is.EqualTo(g));
        }

        [Test]
        public void TestList()
        {
            List<int> l = [1, 2, 3];
            IEnumerable<int> e = l;
            Assert.That(l, Is.EqualTo(e));



        }

        #endregion
    }


   

}
