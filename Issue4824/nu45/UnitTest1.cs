using NUnit.Framework.Constraints;

namespace nu45;

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
    public void TestFuncInlineLambda()
    {
        Func<int> func = () => 42;
        Assert.That(() => func(), Is.EqualTo(42));
    }

    // Row 3: Action/Func<T> (as variable) - ❌ Does NOT work today (type mismatch)
    // These would cause compile errors with NUnit 4.5.1:
    // [Test]
    // public void TestActionAsVariable()
    // {
    //     Action action = () => { };
    //     Assert.That(action, Is.Not.EqualTo(null));  // Error: no overload for Action
    // }
    //
    // [Test]
    // public void TestFuncAsVariable()
    // {
    //     Func<int> func = () => 42;
    //     Assert.That(func, Is.EqualTo(42));  // Error: no overload for Func<T>
    // }

    // Row 4: Explicit TestDelegate usage - ✅ Works today
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
}
