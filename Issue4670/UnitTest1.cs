namespace Issue4670;

public class Tests
{
    [Test]
    public void BugReport()
    {
        var dictionary = new Dictionary<string, int>
    {
        { "a", 123 },
        { "b", 456 }
    };

        Assert.Multiple(() =>
        {
            Assert.That(dictionary, Does.ContainKey("a").WithValue(456).Or.ContainKey("b").WithValue(123), "Passes but should fail");
            Assert.That(dictionary, Does.ContainKey("a").WithValue(456).And.ContainKey("b").WithValue(456), "Passes but should fail");

            // Expected: dictionary containing key "c" or dictionary containing entry ["c", 456]
            // But was:  < ["a", 123], ["b", 456] >
            Assert.That(dictionary, Does.ContainKey("a").WithValue(123).Or.ContainKey("c").WithValue(456), "Fails but should pass");

            // Expected: dictionary containing key "c" and dictionary containing entry ["c", 456]
            // But was:  < ["a", 123], ["b", 456] >
            Assert.That(dictionary, Does.ContainKey("a").WithValue(123).And.ContainKey("c").WithValue(456), "Fails but for the wrong reason");
        });
    }
}