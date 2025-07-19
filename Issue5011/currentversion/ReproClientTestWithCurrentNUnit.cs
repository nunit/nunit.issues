using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace currentversion
{



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

}
