
namespace Issue1713;

[SetUpFixture]
public class AssemblySetUpAndTearDown
{
    [OneTimeSetUp]
    public void AssemblyInitialize()
    {
        throw new Exception("Didnt expect this to throw");
    }
}
