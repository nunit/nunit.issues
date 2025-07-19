using Fhi.ClientCredentials.TestSupport;

namespace currentversion
{
    [TestFixture]
    public class ClientCredentialsTests : Fhi.ClientCredentials.TestSupport.ClientCredentialKeyPairsConfigConsistencyTests
    {
        public ClientCredentialsTests(): base(new List<string>{"dev"}, SetupBaseConfigTests.AppSettingsUsage.AppSettingsIsProd,"Prod")
        {

        }
    }


    [TestFixture]
    public class ReproClientTestCurrentVersion : lastversion.ReproClientTest
    {
       
    }
}
