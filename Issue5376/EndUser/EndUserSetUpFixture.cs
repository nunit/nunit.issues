namespace EndUser;

internal class EndUserSetUpFixture : BaseSetUpFixture
{
    public override void SomeStuff()
    {
        GlobalData.SetUpFixtureExecuted = true;
    }
}
