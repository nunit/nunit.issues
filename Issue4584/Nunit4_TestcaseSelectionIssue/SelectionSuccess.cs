using System.Text.Json.Nodes;

namespace Nunit4_TestcaseSelectionIssue
{
    public class SelectionSuccess
    {
        [Test(), TestCaseSource(typeof(SelectionData), nameof(SelectionData.Data))]
        public int TestSuccess(JsonArray jsonArray, int maxSize)
        {
            int count = 0;
            foreach (JsonNode? ja in jsonArray)
            {
                count++;
            }
            Assert.Pass();
            return (int)Math.Ceiling(((double)jsonArray.Count) / maxSize);
        }


        [Test()]
        [Ignore("this is to work around a bug in NUnit 4 test selection")]
        public void TestSuccess()
        {
            Assert.Pass();
        }
    }
}