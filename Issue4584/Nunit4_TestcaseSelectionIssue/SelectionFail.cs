using System.Text.Json.Nodes;

namespace Nunit4_TestcaseSelectionIssue
{
    public class SelectionFail
    {
        [TestCaseSource(nameof(Data))]
        public void TestFailure(JsonArray jsonArray, int maxSize)
        {
            int count = 0;
            foreach (JsonNode? ja in jsonArray)
            {
                Assert.That(ja, Is.Not.Null);
                count++;
            }

            Assert.Pass();
        }

        public static IEnumerable<TestCaseData> Data
        {
            get
            {
                yield return new TestCaseData(new JsonArray((new int[] { 1, 2, 3, 4, 5, 6 }).Select(v => (JsonNode?)v).ToArray()), 4); ;
                yield return new TestCaseData(new JsonArray((new int[] { 1, 2, 3, 4, 5, 6 }).Select(v => (JsonNode?)v).ToArray()), 2);
                yield return new TestCaseData(new JsonArray((new int[] { 1, 2, 3, 4, 5, 6 }).Select(v => (JsonNode?)v).ToArray()), 1);
                yield return new TestCaseData(new JsonArray((new int[] { 1, 2, 3, 4, 5, 6 }).Select(v => (JsonNode?)v).ToArray()), 7);
            }
        }
    }
}