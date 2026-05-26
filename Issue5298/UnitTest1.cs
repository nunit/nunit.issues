using NUnit.Framework.Constraints;

namespace throwssamples;

public class Tests
{
    [Test]
    public void Test1A()
    {
        Assert.That(WhateverA,  Throws.Exception.Not.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Test1B()
    {
        Assert.That(WhateverB, Throws.Exception.Not.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Test1C()
    {
        Assert.That(WhateverC, Throws.Exception.Not.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Test2A()
    {
        Assert.That(Assert.Catch(() => WhateverA()), Is.Null.Or.Not.InstanceOf<InvalidOperationException>());
    }

    /// <summary>
    /// Should pass
    /// </summary>
    public int WhateverA()
    {
        throw new ArgumentException("This is an valid exception.");
    }

    [Test]
    public void Test2B()
    {
        Assert.That(Assert.Catch(() => WhateverB()), Is.Null.Or.Not.InstanceOf<InvalidOperationException>());
    }

    /// <summary>
    /// Should fail
    /// </summary>
    public int WhateverB()
    {
        throw new InvalidOperationException("This is an invalid operation.");
    }

    [Test]
    public void Test2C()
    {
        Assert.That(Assert.Catch(() => WhateverC()), Is.Null.Or.Not.InstanceOf<InvalidOperationException>());
    }

    public int? WhateverC()
    {
        return null;
    }


   

    

    
}
