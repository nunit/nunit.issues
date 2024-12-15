namespace checkredisnull;

public struct StructWithInt
{
    public int Value;

}

public struct StructWithStringAndImplicitOperator
{
    public string? Value;

    /// <summary>
    /// Adding this causes the Assert.That to choose the EqualStringConstraint instead of the EqualConstraint
    /// </summary>
    /// <param name="values"></param>
    public static implicit operator string?(StructWithStringAndImplicitOperator values)
    {
        return values.Value;
    }
}

public struct StructWithString
{
    public string? Value;
}



[TestFixture]
public class MyStructTestsWithInt
{
    [Test]
    public void MyStruct_Equals_ShouldReturnTrue_WhenValuesAreEqual()
    {
        // Arrange
        var structActual = new StructWithInt { Value = 0 };
        var structExpected = new StructWithInt { Value = 0 };

        // Act & Assert
        Assert.That(structActual, Is.EqualTo(structExpected), "StructWithInt.Equals should return true for equal values.");
    }

    [Test]
    public void MyStruct_Equals_ShouldReturnFalse_WhenValuesAreNotEqual()
    {
        // Arrange
        var structActual = new StructWithInt { Value = 0 };
        var structExpected = new StructWithInt { Value = 10 };

        // Act & Assert
        Assert.That(structActual,Is.Not.EqualTo(structExpected), "StructWithInt.Equals should return false for unequal values.");
    }

    [Test]
    public void MyStruct_Equals_ShouldReturnFalse_WhenComparedToDifferentType()
    {
        // Arrange
        var structActual = new StructWithInt { Value = 0 };

        // Act & Assert
        Assert.That(structActual, Is.Not.EqualTo("string"), "StructWithInt.Equals should return false when compared to a different type.");
    }
}

[TestFixture]
public class MyStructTestsWithStringAndImplicitOperator
{
    [Test]
    public void MyStruct_Equals_ShouldReturnTrue_WhenValuesAreEqual()
    {
        // Arrange
        var structActual = new StructWithStringAndImplicitOperator { Value = "0" };
        var structExpected = new StructWithStringAndImplicitOperator { Value = "0" };
        // Act & Assert
        Assert.That(structActual, Is.EqualTo(structExpected), "StructWithStringAndImplicitOperator.Equals should return true for equal values.");
    }
    [Test]
    public void MyStruct_Equals_ShouldReturnFalse_WhenValuesAreNotEqual()
    {
        // Arrange
        var structActual = new StructWithStringAndImplicitOperator { Value = "0" };
        var structExpected = new StructWithStringAndImplicitOperator { Value = "10" };
        // Act & Assert
        Assert.That(structActual, Is.Not.EqualTo(structExpected), "StructWithStringAndImplicitOperator.Equals should return false for unequal values.");
    }
    [Test]
    public void MyStruct_Equals_ShouldReturnFalse_WhenComparedToDifferentType()
    {
        // Arrange
        var structActual = new StructWithStringAndImplicitOperator { Value = "0" };
        // Act & Assert
        Assert.That(structActual, Is.Not.EqualTo("string"), "StructWithStringAndImplicitOperator.Equals should return false when compared to a different type.");
    }
}

[TestFixture]
public class MyStructTestsWithString
{
    [Test]
    public void MyStruct_Equals_ShouldReturnTrue_WhenValuesAreEqual()
    {
        // Arrange
        var structActual = new StructWithString { Value = "0" };
        var structExpected = new StructWithString { Value = "0" };
        // Act & Assert
        Assert.That(structActual, Is.EqualTo(structExpected), "StructWithString.Equals should return true for equal values.");
    }
    [Test]
    public void MyStruct_Equals_ShouldReturnFalse_WhenValuesAreNotEqual()
    {
        // Arrange
        var structActual = new StructWithString { Value = "0" };
        var structExpected = new StructWithString { Value = "10" };
        // Act & Assert
        Assert.That(structActual, Is.Not.EqualTo(structExpected), "StructWithString.Equals should return false for unequal values.");
    }
    [Test]
    public void MyStruct_Equals_ShouldReturnFalse_WhenComparedToDifferentType()
    {
        // Arrange
        var structActual = new StructWithString { Value = "0" };
        // Act & Assert
        Assert.That(structActual, Is.Not.EqualTo("string"), "StructWithString.Equals should return false when compared to a different type.");
    }
}