
namespace Namespace
{
    internal abstract class Base
    {
        [Test]
        public void SomeTest() => Assert.Pass(); // Please assume this depends on functionality defined in the derived classes.
    }

    internal class Derived1 : Base { }
    internal class Derived2 : Base { }
}