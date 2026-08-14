// This BaseSetUpFixture has no namespace to act as a global setup
[SetUpFixture]
public abstract class BaseSetUpFixture
{
   [OneTimeSetUp]
   public async Task OneTimeSetUpAsync()
   {
      // do stuff
      SomeStuff();
   }

   public virtual void SomeStuff()
   {
      // do stuff
   }
}

