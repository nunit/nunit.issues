namespace Issue2789;

[TestFixture(typeof(MyTypeA))]
[TestFixture(typeof(MyTypeB))]
public class MyTypeFixture<T>
  where T : IMyServiceType
{
  [TestFixture]
  public class MyMethodOneFixture
  {
    // test methods here for IMyServiceType.MyMethodOne
    [Test]
    public void TestMyMethodOne()
    {
        // Arrange
        // var service = ServiceLocator.GetService<T>();

        // Act
        // service.MyMethodOne();

        // Assert
        // Add assertions here to verify the behavior of MyMethodOne
    }
  }
  [TestFixture]
  public class MyMethodTwoFixture
  {
    // test methods here for IMyServiceType.MyMethodTwo
  }
}

public interface IMyServiceType
{
  void MyMethodOne();
  void MyMethodTwo();
}

public class MyTypeA : IMyServiceType
{
  public void MyMethodOne() { }
  public void MyMethodTwo() { }
}

public class MyTypeB : IMyServiceType
{
  public void MyMethodOne() { }
  public void MyMethodTwo() { }
}