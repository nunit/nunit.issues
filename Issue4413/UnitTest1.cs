namespace Issue4413;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SingleTest()
    {
        int nunitAge = 23;
        Verify.That(nunitAge, Is.GreaterThan(42).And.LessThan(85));
    }

    [Test]
    public void SingleTest2()
    {
        double nunitAge = 23.4;
        Verify.That(nunitAge, Is.GreaterThan(42.0).Within(0.2).And.LessThan(85.4), null);
    }

    [Test]
    public void SingleTest3()
    {
        int nunitAge = 23;
        Verify.That(nunitAge, Is.GreaterThan(42).And.LessThan(85),$"The age {nunitAge} fails to meet the objective");
    }

    [Test]
    public void SingleTest4()
    {
        int nunitAge = 23;
        Verify.That(nunitAge, Is.GreaterThan(42).And.LessThan(85), "The age {0} fails to meet the objective",nunitAge);
    }


    [Test]
    public void Test1()
    {
        int age = 42;
        int employment = 5;
        Assert.That(age, Is.GreaterThan(2));
        Assert.Multiple(() =>
        {
            Verify.That(age, Is.EqualTo(2));
            Verify.That(age, Is.EqualTo(3));
            Verify.That(age,Is.EqualTo(42));
            Verify.That(employment, Is.GreaterThan(55));
            Verify.That(age, Is.LessThan(42));
            Verify.That(age, Is.EqualTo(4));
            Verify.That(employment, Is.EqualTo(55));
        });
    }
}