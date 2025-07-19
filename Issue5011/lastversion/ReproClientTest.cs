using Fhi.ClientCredentialsKeypairs;

namespace lastversion
{
    public class ReproClientTest
    {
        [Test]
        public void TestClientId()
        {
            var clientConfig = new ClientCredentialsConfiguration
            {

            };
            string clientProductionId = "whatever";
            Assert.That(clientProductionId, Is.Not.EqualTo(clientConfig));

        }

        [Test]
        public void TestCLientIdAsList()
        {
            var clientConfigList = new List<ClientCredentialsConfiguration?>
            {
                new ClientCredentialsConfiguration()
            };
            string clientProductionId = "whatever";
            Assert.That(clientProductionId, Is.Not.EqualTo(clientConfigList.First()));

        }
    }
}
