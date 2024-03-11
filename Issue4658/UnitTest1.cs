using NUnit.Framework;

namespace ApartmentTest
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class Tests
    {
        [Test]
        public void ApartmentStateShouldBeSTA()
        {
            Assert.That(Thread.CurrentThread.GetApartmentState(), Is.EqualTo(ApartmentState.STA));
        }
    }
}