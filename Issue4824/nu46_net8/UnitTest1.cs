using NUnit.Framework.Constraints;

namespace nu46_net8;

public class Tests
{
    // Row 1: Inline lambda - ❌ BREAKS on .NET 8 with NUnit 4.6 (ambiguity between TestDelegate and Action)
    // [Test]
    // public void TestInlineLambda()
    // {
    //     Assert.That(() => { }, Is.Not.EqualTo(null));  // Error CS0121: ambiguous call
    // }

    // Row 2: Action/Func<T> (inline lambda) - ❌ BREAKS on .NET 8 with NUnit 4.6 (ambiguity)
    // [Test]
    // public void TestActionInlineLambda()
    // {
    //     Action action = () => { };
    //     Assert.That(() => action(), Is.Not.EqualTo(null));  // Error CS0121: ambiguous call
    // }
    //
    // [Test]
    // public void TestFuncInlineLambda()
    // {
    //     Func<int> func = () => 42;
    //     Assert.That(() => func(), Is.EqualTo(42));  // Error CS0121: ambiguous call
    // }

    // Row 3: Action/Func<T> (as variable) - ✅ Works with NUnit 4.6
    [Test]
    public void TestActionAsVariable()
    {
        Action action = () => { };
        Assert.That(action, Is.Not.EqualTo(null));
    }

    [Test]
    public void TestFuncAsVariable()
    {
        Func<int> func = () => 42;
#pragma warning disable NUnit2021 // Analyzer not updated for NUnit 4.6 Func<T> execution behavior
        Assert.That(func, Is.EqualTo(42));
#pragma warning restore NUnit2021
    }

    // Row 4: Explicit TestDelegate usage - ❌ Obsolete/breaking with NUnit 4.6
    // These types are marked obsolete and should be replaced with Action/Func<T>:
    // [Test]
    // public void TestExplicitTestDelegate()
    // {
    //     TestDelegate td = () => { };
    //     Assert.That(td, Is.Not.EqualTo(null));  // Warning CS0618: TestDelegate is obsolete
    // }
    //
    // [Test]
    // public void TestExplicitActualValueDelegate()
    // {
    //     ActualValueDelegate<int> avd = () => 42;
    //     Assert.That(avd, Is.EqualTo(42));  // Warning CS0618: ActualValueDelegate is obsolete
    // }
}
