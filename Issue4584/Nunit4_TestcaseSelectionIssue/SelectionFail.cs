using System.Text.Json.Nodes;

namespace Nunit4_TestcaseSelectionIssue
{
    public class SelectionFail
    {
        [Test(), TestCaseSource(typeof(SelectionData), nameof(SelectionData.Data))]
        public int TestFailure(JsonArray jsonArray, int maxSize)
        {
            int count = 0;
            foreach (JsonNode? ja in jsonArray)
            {
                Assert.That(ja, Is.Not.Null);
                count++;
            }

            Assert.Pass();
            return (int)Math.Ceiling(((double)jsonArray.Count) / maxSize);
        }
    }
}