using NUnit.Framework.Constraints;

namespace nu46_net9;

public class Tests
{
    // Row 1: Inline lambda - ✅ Works today
    [Test]
    public void TestInlineLambda()
    {
        Assert.That(() => { }, Is.Not.EqualTo(null));
    }

    // Row 2: Action/Func<T> (inline lambda) - ✅ Works today (via implicit conversion)
    [Test]
    public void TestActionInlineLambda()
    {
        Action action = () => { };
        Assert.That(() => action(), Is.Not.EqualTo(null));
    }

    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2021:Incompatible types for EqualTo constraint", Justification = "<Pending>")]
    public void TestFuncInlineLambda()
    {
        Func<int> func = () => 42;
        Assert.That(() => func(), Is.EqualTo(42));
    }

    [Test]
    public void TestActionAsVariable()
    {
        Action action = () => { };
        Assert.That(action, Is.Not.EqualTo(null));  // Error: no overload for Action
    }

    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2021:Incompatible types for EqualTo constraint", Justification = "<Pending>")]
    public void TestFuncAsVariable()
    {
        Func<int> func = () => 42;
        Assert.That(func, Is.EqualTo(42));  // Error: no overload for Func<T>
    }

    // Row 4: Explicit TestDelegate/ActualValueDelegate usage - ✅ Works today
    [Test]
    public void TestExplicitTestDelegate()
    {
        TestDelegate td = () => { };
        Assert.That(td, Is.Not.EqualTo(null));
    }

    [Test]
    public void TestExplicitActualValueDelegate()
    {
        ActualValueDelegate<int> avd = () => 42;
        Assert.That(avd, Is.EqualTo(42));
    }
    
    //Row 5: Func with constraint that evaluates result
    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2021:Incompatible types for EqualTo constraint", Justification = "<Pending>")]
    public void TestFuncWithConstraintResult()
    {
        Assert.That(() => 42, Is.EqualTo(42)); // Example constraint that evaluates func's result
    }
}
