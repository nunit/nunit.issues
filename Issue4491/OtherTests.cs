using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issue4491;

internal class OtherTests
{
    internal enum TestType
    {
        Test,
        Theory,
        Whatever
    }


    [Theory]
    public void TestTheory(TestType t)
    {
        TestContext.WriteLine(TestContext.CurrentContext.Test.ID);
        Assert.Pass();
    }
}