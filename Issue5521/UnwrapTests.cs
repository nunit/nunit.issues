using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Repro;

public static class Thrower
{
    public static void Pass() => Assert.Pass();
}

public class UnwrapTests
{
    // What MethodBase.Invoke throws on a runtime without stack traces: the wrapper has no StackTrace.
    // Unwrap only strips it when StackTrace contains "NUnit.Framework", so the wrapper is kept.
    [Test]
    public void Unwrap_TargetInvocationExceptionWithoutStackTrace_ReturnsInnerException()
    {
        var noTrace = new TargetInvocationException(new SuccessException("passed"));
        Assert.That(noTrace.StackTrace, Is.Null);

        Assert.That(noTrace.Unwrap(), Is.TypeOf<SuccessException>());
    }

    // Control: NUnit's own invoke path on a runtime with stack traces unwraps the wrapper.
    [Test]
    public void ReflectInvokeMethod_WithStackTraces_RethrowsInnerException()
    {
        var e = Assert.Throws<NUnitException>(() => Reflect.InvokeMethod(typeof(Thrower).GetMethod(nameof(Thrower.Pass))!, null));

        Assert.That(e!.InnerException, Is.TypeOf<SuccessException>());
    }
}
