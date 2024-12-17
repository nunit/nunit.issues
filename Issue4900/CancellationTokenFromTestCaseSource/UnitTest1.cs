namespace CancellationTokenFromTestCaseSource
{
    public class Tests
    {
        [TestCaseSource(nameof(DivideCases)), CancelAfter(5000)]
        public void DivideTest(int n, int d, int q, CancellationToken cancellationToken)
        {
            Assert.That(n / d, Is.EqualTo(q));
        }

        public static object[] DivideCases =
        {
            new object[] { 12, 3, 4 },
            new object[] { 12, 2, 6 },
            new object[] { 12, 4, 3 }
        };
    }
}
