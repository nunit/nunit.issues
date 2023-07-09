namespace Issue4413;

/*
Assert.That(bool condition);
Assert.That(bool condition, string message, params object[] params);
Assert.That(bool condition, Func<string> getExceptionMessage);

Assert.That(Func<bool> condition);
Assert.That(Func<bool> condition, string message, params object[] params);
Assert.That(Func<bool> condition, Func<string> getExceptionMessage);

Assert.That<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint constraint)
Assert.That<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint constraint,
    string message, object[] params)
Assert.That<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr,
    Func<string> getExceptionMessage)

Assert.That<TActual>(TActual actual, IResolveConstraint constraint)
Assert.That<TActual>(TActual actual, IResolveConstraint constraint, string message,
    params object[] params)
Assert.That<TActual>(TActual actual, IResolveConstraint expression,
    Func<string> getExceptionMessage)

Assert.That(TestDelegate del, IResolveConstraint constraint)
Assert.That(TestDelegate code, IResolveConstraint constraint, string message,
    params object[] args)
Assert.That(TestDelegate code, IResolveConstraint constraint,
    Func<string> getExceptionMessage)
 
 */




public class AllOverloadsOfThatTests
{
    /// <summary>
    /// Assert.That(bool condition);
    /// </summary>
    [Test]
    public void WithBooleanTest()
    {
        bool b = false;
        Verify.That(b);
    }

    /// <summary>
    /// Assert.That(bool condition,string message);
    /// </summary>
    [Test]
    public void WithBooleanAndMessageTest()
    {
        bool b = false;
        Verify.That(b,"Wrong");
    }

    /// <summary>
    /// Assert.That(bool condition,string message,params);
    /// </summary>
    //[Test]
    //public void WithBooleanAndMessageWithParamsTest()
    //{
    //    bool b = false;
    //    Verify.That(b, "Wrong {0}",b);
    //}

    /// <summary>
    /// Assert.That(actual,constraint,message,params)
    /// </summary>
    [Test]
    public void WithMessageParamStringTest()
    {
        int x = 42;
        Verify.That(x,Is.EqualTo(43),"Variable x={0} is not 43",x);
    }


    /// <summary>
    /// Assert.That(actual,constraint,message,params)
    /// </summary>
    [Test]
    public void WithMessageParamStringTestOnStrings()
    {
        string x = "Whatever";
        Verify.That(x, Is.EqualTo("Something"), "Variable x={0} is not 43", x);
    }




    public void WithBooleanTestCheck()
    {
        bool b = false;
        Verify.Check(b);
    }
}