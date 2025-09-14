


using Fhi.ClientCredentialsKeypairs;

namespace lastversion
{
    /// <summary>
    /// These tests reproduces the actual error.
    /// Note that 
    /// </summary>
    public class ReproClientTest
    {
        [Test]
        public void TestClientId()
        {
            var clientConfig = new ClientCredentialsConfiguration
            {

            };
            string clientProductionId = "whatever";
#pragma warning disable NUnit2021
            Assert.That(clientProductionId, Is.Not.EqualTo(clientConfig));
#pragma warning restore NUnit2021

        }

        [Test]
        public void TestClientIdAsList()
        {
            var clientConfigList = new List<ClientCredentialsConfiguration?>
            {
                new ClientCredentialsConfiguration()
            };
            string clientProductionId = "whatever";
#pragma warning disable NUnit2021
            Assert.That(clientProductionId, Is.Not.EqualTo(clientConfigList.First()));
#pragma warning restore NUnit2021

        }
    }
}
