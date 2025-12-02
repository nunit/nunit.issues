namespace docs909;

public class Tests
{
   
    
[Test]
public void CollectionContains_Examples()
{
    int[] intArray = {1, 2, 3};
    string[] stringArray = ["a", "b", "c"];
    
    Assert.That(intArray, Has.Member(3));
    Assert.That(stringArray, Has.Member("b"));
    Assert.That(stringArray, Contains.Item("c"));
    Assert.That(stringArray, Has.No.Member("x"));
    Assert.That(intArray, Does.Contain(3));
}

}
