using NUnit.Framework.Constraints;

namespace nu46_net8;

public class Tests_NUnit46_With_Net8
{
    // Row 1: Inline lambda - ❌ BREAKS on .NET 8 with NUnit 4.6 (ambiguity between TestDelegate and Action)
    // [Test]
    // public void Row_1_TestInlineLambda()
    // {
    //     Assert.That(() => { }, Is.Not.EqualTo(null));  // Error CS0121: ambiguous call
    // }

    // Row 2: Action/Func<T> (inline lambda) - ❌ BREAKS on .NET 8 with NUnit 4.6 (ambiguity)
    // [Test]
    // public void Row_2_TestActionInlineLambda()
    // {
    //     Action action = () => { };
    //     Assert.That(() => action(), Is.Not.EqualTo(null));  // Error CS0121: ambiguous call
    // }
    //
    // [Test]
    // public void Row_2_TestFuncInlineLambda()
    // {
    //     Func<int> func = () => 42;
    //     Assert.That(() => func(), Is.EqualTo(42));  // Error CS0121: ambiguous call
    // }

    // Row 3: Action/Func<T> (as variable) - ✅ Works with NUnit 4.6
    [Test]
    public void Row_3_TestActionAsVariable()
    {
        Action action = () => { };
        Assert.That(action, Is.Not.EqualTo(null));
    }

    [Test]
    public void Row_3_TestFuncAsVariable()
    {
        Func<int> func = () => 42;
        Assert.That(func, Is.EqualTo(42));
    }

    // Row 4: Explicit TestDelegate usage - ❌ Obsolete/breaking with NUnit 4.6
    // These types are marked obsolete and should be replaced with Action/Func<T>:
    // [Test]
    // public void Row_4_TestExplicitTestDelegate()
    // {
    //     TestDelegate td = () => { };
    //     Assert.That(td, Is.Not.EqualTo(null));  // Warning CS0618: TestDelegate is obsolete
    // }
    //
    // [Test]
    // public void Row_4_TestExplicitActualValueDelegate()
    // {
    //     ActualValueDelegate<int> avd = () => 42;
    //     Assert.That(avd, Is.EqualTo(42));  // Warning CS0618: ActualValueDelegate is obsolete
    // }

    // Row 5: Func with constraint that evaluates result - but doesn't do anything like that, this is useless
    // Test code checks that evaluating the constraint throws an assertion failure because the Func<int> itself is not equal to the expected value.
    [Test]
    public void Row_5_TestFuncWithConstraintResult()
    {
        Func<int> func = () => 42;
        Action act = () => { Assert.That(func, Is.EqualTo(43)); };
        Assert.That(act, Throws.Exception.TypeOf<AssertionException>());
    }
}
