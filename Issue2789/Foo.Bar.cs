namespace Issue2789;


    using NUnit.Framework;

    [TestFixture(typeof(int))]
    [TestFixture(typeof(double))]
    public partial class Foo<T>
    {
        public class Bar
        {
            [Test]
            public void BarTest()
            {
            }
        }
    }

