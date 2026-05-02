using NUnit.Framework.Constraints;

namespace nu46_net8_lang13;

public class Tests_NUnit46_With_Net8_And_LangVersion_13
{
    //// Row 1: Inline lambda - ✅ Works with NUnit 4.6
    
    [Test]
    public void Row_1_TestInlineLambda()
    {
        Assert.That(() => { }, Is.Not.EqualTo(null));  // Error CS0121: ambiguous call
    }

    // Row 2: Action/Func<T> (inline lambda) - ✅ Works with NUnit 4.6
    [Test]
    public void Row_2_TestActionInlineLambda()
    {
        Action action = () => { };
        Assert.That(() => action(), Is.Not.EqualTo(null));  // Error CS0121: ambiguous call
    }
    
    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2021:Incompatible types for EqualTo constraint", Justification = "<Pending>")]
    public void Row_2_TestFuncInlineLambda()
    {
        Func<int> func = () => 42;
        Assert.That(() => func(), Is.EqualTo(42));  // Error CS0121: ambiguous call
    }

    // Row 3: Action/Func<T> (as variable) - ✅ Works with NUnit 4.6
    [Test]
    public void Row_3_TestActionAsVariable()
    {
        Action action = () => { };
        Assert.That(action, Is.Not.EqualTo(null));
    }

    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2021:Incompatible types for EqualTo constraint", Justification = "<Pending>")]
    public void Row_3_TestFuncAsVariable()
    {
        Func<int> func = () => 42;
        Assert.That(func, Is.EqualTo(42));
    }

    // Row 4: Explicit TestDelegate usage - ✅ Works with NUnit 4.6
    // These types are marked with obsolete warning and should eventually be replaced with Action / Func<T>:
    [Test]
    public void Row_4_TestExplicitTestDelegate()
    {
        TestDelegate td = () => { };
        Assert.That(td, Is.Not.EqualTo(null));  // Warning CS0618: TestDelegate is obsolete
    }

    [Test]
    public void Row_4_TestExplicitActualValueDelegate()
    {
        ActualValueDelegate<int> avd = () => 42;
        Assert.That(avd, Is.EqualTo(42));  // Warning CS0618: ActualValueDelegate is obsolete
    }

    //Row 5: Func with constraint that evaluates result
    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2021:Incompatible types for EqualTo constraint", Justification = "<Pending>")]
    public void Row_5_TestFuncWithConstraintResult()
    {
        Assert.That(() => 42, Is.EqualTo(42)); // Example constraint that evaluates func's result
    }
}
