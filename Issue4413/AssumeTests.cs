namespace Issue4413;

public class AssumeTests
{
    [TestCase(41, 43)]
    [TestCase(42,43)]
    public void CheckThatAssumeWorks(int x,int y)
    {
        Assume.That(x,Is.EqualTo(42));
    }
}