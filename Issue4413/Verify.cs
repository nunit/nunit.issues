using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

namespace Issue4413;

public class Verify
{
    /// <summary>
    /// Handling test with implicit constraint, bool condition
    /// </summary>
    public static void That(bool actual, string? message=null,[CallerArgumentExpression("actual")] string? actualExpression = null)
    {
        That(actual, Is.True, message, actualExpression);
    }

    /// <summary>
    /// Legacy: Handling message with params. 
    /// </summary>
    /// <typeparam name="TActual"></typeparam>
    public static void That<TActual>(
        TActual actual,
        IResolveConstraint expression,
        string? message = null,
        params object?[]? args)
    {
        var msg = args is not null && args.Length > 0 && message != null ? string.Format(message, args) : message;
        That(actual, expression, msg);
    }


    public static void That<TActual>(
        TActual actual,
        IResolveConstraint expression,
        string? message=null,
        [CallerArgumentExpression("actual")] string? actualExpression = null,
        [CallerArgumentExpression("expression")] string? constraintExpression = null)
    {
        var constraint = expression.Resolve();
        // Assert.IncrementAssertCount();
        var result = constraint.ApplyTo(actual);
        if (result.IsSuccess)
            return;
        var expressionMessage = "Assert.That(" + actualExpression + ", " + constraintExpression + ")";
        string msg = message != null ? $"{message}\n{expressionMessage} " : expressionMessage;
        MessageWriter writer = new TextMessageWriter(msg);
        result.WriteMessageTo(writer);
        Assert.Fail(writer.ToString());
    }

    public static void Check(bool inp)
    {
        Console.WriteLine(inp);
    }

    public static void Check<T>(T inp, IResolveConstraint c)
    {
        Console.WriteLine(inp);
    }
}