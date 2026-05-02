using NUnit.Framework.Constraints;

namespace nu46_net8;

public class Tests
{
    //// Row 1: Inline lambda - ✅ Works with NUnit 4.6
    
    [Test]
    public void TestInlineLambda()
    {
        Assert.That(() => { }, Is.Not.EqualTo(null));  // Error CS0121: ambiguous call
    }

    // Row 2: Action/Func<T> (inline lambda) - ✅ Works with NUnit 4.6
    [Test]
    public void TestActionInlineLambda()
    {
        Action action = () => { };
        Assert.That(() => action(), Is.Not.EqualTo(null));  // Error CS0121: ambiguous call
    }
    
    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2021:Incompatible types for EqualTo constraint", Justification = "<Pending>")]
    public void TestFuncInlineLambda()
    {
        Func<int> func = () => 42;
        Assert.That(() => func(), Is.EqualTo(42));  // Error CS0121: ambiguous call
    }

    // Row 3: Action/Func<T> (as variable) - ✅ Works with NUnit 4.6
    [Test]
    public void TestActionAsVariable()
    {
        Action action = () => { };
        Assert.That(action, Is.Not.EqualTo(null));
    }

    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2021:Incompatible types for EqualTo constraint", Justification = "<Pending>")]
    public void TestFuncAsVariable()
    {
        Func<int> func = () => 42;
        Assert.That(func, Is.EqualTo(42));
    }

    // Row 4: Explicit TestDelegate usage - ✅ Works with NUnit 4.6
    // These types are marked with obsolete warning and should eventually be replaced with Action / Func<T>:
    [Test]
    public void TestExplicitTestDelegate()
    {
        TestDelegate td = () => { };
        Assert.That(td, Is.Not.EqualTo(null));  // Warning CS0618: TestDelegate is obsolete
    }

    [Test]
    public void TestExplicitActualValueDelegate()
    {
        ActualValueDelegate<int> avd = () => 42;
        Assert.That(avd, Is.EqualTo(42));  // Warning CS0618: ActualValueDelegate is obsolete
    }
}
