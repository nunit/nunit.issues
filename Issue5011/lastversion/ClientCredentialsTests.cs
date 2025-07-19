using Fhi.ClientCredentials.TestSupport;

namespace lastversion
{
    [TestFixture]
    public class ClientCredentialsTests : Fhi.ClientCredentials.TestSupport.ClientCredentialKeyPairsConfigConsistencyTests
    {
        public ClientCredentialsTests(): base(new List<string>{"dev"}, SetupBaseConfigTests.AppSettingsUsage.AppSettingsIsProd,"Prod")
        {

        }
    }
}
