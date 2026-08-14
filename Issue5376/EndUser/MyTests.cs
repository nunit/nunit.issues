using Extensions;

namespace EndUser;

public class MyTests
{
   [MyTest]
   public async Task VerifySetUpFixtureExecuted()
   {
      Assert.That(GlobalData.SetUpFixtureExecuted, Is.True);
    }
}

