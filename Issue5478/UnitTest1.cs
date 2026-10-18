// A child namespace with a fixture, declared first, so its namespace suite is the first child of Repro5478.
namespace Repro5478.Child
{
    public class Other { [Test] public void T() { } }
}

namespace Repro5478
{
    public static class Db { public static List<string> Items { get; } = []; }

    // Declared before Seed, so without a working dependency it runs first.
    [DependsOnFixture(typeof(Seed))]
    public class Report { [Test] public void Read() => Assert.That(Db.Items, Is.Not.Empty); }

    public class Seed { [Test] public void Add() => Db.Items.Add("x"); }
}

/*
This is a reproduction of NUnit issue 5478.
Run with: dotnet test -- NUnit.NumberOfTestWorkers=0   (avoids issue 5479)
Expected (bug): Read fails, because the dependency on Seed is ignored and Report runs first.
Remove Repro5478.Child and Read passes.

TestSuite Repro5478
  TestSuite Child        <- Tests[0], a namespace suite, not a TestFixture
    TestFixture Other
  TestFixture Report
  TestFixture Seed


*/
