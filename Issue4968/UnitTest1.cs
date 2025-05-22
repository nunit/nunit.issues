namespace Issue4968;

[TestFixture]
public class RepeatTest
{
    public class ClassToCheck()
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "Name";
        public DateTimeOffset Now { get; } = DateTimeOffset.UtcNow;
    }


    [Test]
    public void Test()
    {
        var expected = new ClassToCheck();
        var actual = new ClassToCheck();
        Assert.That(actual, Is.EqualTo(expected).UsingPropertiesComparer().Within(TimeSpan.FromSeconds(1)));
    }
}



