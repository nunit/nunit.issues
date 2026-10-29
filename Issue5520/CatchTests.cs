using System;
using NUnit.Framework;

namespace Repro;

// The string argument is the assert's failure message, printed only if the assert fails.
// It is not compared with the exception's message.
public class CatchTests
{
    // Passes: Assert.Catch(Action) accepts any exception type.
    [Test]
    public void NoMessage() => Assert.Catch(() => throw new ArgumentException());

    // Fails on NUnit 5.0.0: the message overload calls Throws<Exception>, which requires exactly System.Exception.
    [Test]
    public void WithMessage() => Assert.Catch(() => throw new ArgumentException(), "should throw");

    // Control: the message overload does pass when the exception is exactly System.Exception.
    [Test]
    public void WithMessage_ExactException() => Assert.Catch(() => throw new Exception(), "should throw");
}
