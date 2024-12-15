
namespace checkredisnull;

using NUnit.Framework;
using StackExchange.Redis;

[TestFixture]
public class RedisValueTests
{
    [Test]
    public void RedisValue_Default_ShouldBeNullOrEmpty()
    {
        // Arrange
        RedisValue value = default;

        // Act & Assert
        Assert.That(value.IsNullOrEmpty, Is.True, "Default RedisValue should be null or empty.");
    }

    [Test]
    public void RedisValue_NullConstant_ShouldBeNullOrEmpty()
    {
        // Arrange
        RedisValue value = RedisValue.Null;

        // Act & Assert
        Assert.That(value.IsNullOrEmpty, Is.True, "RedisValue.Null should be null or empty.");
    }

    [Test]
    public void RedisValue_WithValue_ShouldNotBeNullOrEmpty()
    {
        // Arrange
        RedisValue value = "TestValue";

        // Act & Assert
        Assert.That(value.IsNullOrEmpty, Is.False, "RedisValue with a valid value should not be null or empty.");
    }

    [Test]
    public void RedisValue_NullConstant_ShouldEqual_Default()
    {
        // Arrange
        RedisValue defaultValue = default;
        RedisValue nullValue = RedisValue.Null;

        // Act & Assert
        Assert.That(nullValue, Is.EqualTo(defaultValue), "RedisValue.Null should equal the default RedisValue.");
    }

    [Test]
    public void RedisValue_RedisVariable_SetToRedisValueNull_ShouldEqual_RedisValueNull()
    {
        // Arrange
        RedisValue redisResult = RedisValue.Null;

        // Act & Assert
        Assert.That(redisResult, Is.EqualTo(RedisValue.Null), "RedisValue.Null should equal the default RedisValue.");
    }

    [Test]
    public void NullableRedisValue_ShouldSupportNullAssignment()
    {
        // Arrange
        RedisValue? nullableValue = null;

        // Act & Assert
        Assert.That(nullableValue, Is.Null, "Nullable RedisValue should support explicit null assignment.");
    }

}

public struct MyStruct
{
    public int Value;

}

[TestFixture]
public class MyStructTests
{
    [Test]
    public void MyStruct_Equals_ShouldReturnTrue_WhenValuesAreEqual()
    {
        // Arrange
        var struct1 = new MyStruct { Value = 0 };
        var struct2 = new MyStruct { Value = 0 };

        // Act & Assert
        Assert.That(struct1, Is.EqualTo(struct2), "MyStruct.Equals should return true for equal values.");
    }

    [Test]
    public void MyStruct_Equals_ShouldReturnFalse_WhenValuesAreNotEqual()
    {
        // Arrange
        var struct1 = new MyStruct { Value = 0 };
        var struct2 = new MyStruct { Value = 10 };

        // Act & Assert
        Assert.That(struct1,Is.Not.EqualTo(struct2), "MyStruct.Equals should return false for unequal values.");
    }

    [Test]
    public void MyStruct_Equals_ShouldReturnFalse_WhenComparedToDifferentType()
    {
        // Arrange
        var struct1 = new MyStruct { Value = 0 };

        // Act & Assert
        Assert.That(struct1, Is.Not.EqualTo("string"), "MyStruct.Equals should return false when compared to a different type.");
    }
}

