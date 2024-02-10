using System.Text.Json.Nodes;

namespace Nunit4_TestcaseSelectionIssue
{
    public class SelectionData
    {
        public static IEnumerable<TestCaseData> Data
        {
            get
            {
                yield return new TestCaseData(new JsonArray((new int[] { 1, 2, 3, 4, 5, 6 }).Select(v => (JsonNode?)v).ToArray()), 4)
                {
                    ExpectedResult = 2,
                };
                yield return new TestCaseData(new JsonArray((new int[] { 1, 2, 3, 4, 5, 6 }).Select(v => (JsonNode?)v).ToArray()), 2)
                {
                    ExpectedResult = 3,
                };
                yield return new TestCaseData(new JsonArray((new int[] { 1, 2, 3, 4, 5, 6 }).Select(v => (JsonNode?)v).ToArray()), 1)
                {
                    ExpectedResult = 6,
                };
                yield return new TestCaseData(new JsonArray((new int[] { 1, 2, 3, 4, 5, 6 }).Select(v => (JsonNode?)v).ToArray()), 7)
                {
                    ExpectedResult = 1,
                };
            }
        }
    }
}