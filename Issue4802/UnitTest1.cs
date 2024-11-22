namespace Issue4802;

public class Tests
{
     public static async IAsyncEnumerable<TestCaseData> AsyncEnumerableTestCases( )
      {
      await Task.Delay(100); // Simulate an asynchronous operation
      yield return new TestCaseData(42, 42);
      await Task.Delay(100); // Simulate another asynchronous operation
      yield return new TestCaseData(51, 51);
  }
  
  [TestCaseSource(nameof(AsyncEnumerableTestCases))]
  public async Task TestAsyncEnumerable(int expected, int actual)
  {
      await Task.Delay(100); // Simulate an asynchronous operation
      Assert.That(actual, Is.EqualTo(expected));
  }
}