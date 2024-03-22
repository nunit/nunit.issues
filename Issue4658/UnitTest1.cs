using NUnit.Framework;

namespace ApartmentTest
{
    [TestFixture]
    public class Tests
    {
        [Test, Apartment(ApartmentState.STA)]
        public void ApartmentStateShouldBeSTA()
        {
            var state = GetApartmentState(Thread.CurrentThread);
            Assert.That(state, Is.EqualTo(ApartmentState.STA));
        }


        protected static ApartmentState GetApartmentState(Thread thread)
        {
            return thread.GetApartmentState();
        }
    }
}