using System;
using NUnit.Framework;

namespace smdn;

[TestFixture]
public class Tests
{
    [Test]
    public void Test()
    {
        // Tests a structure that implements both IEnumerable<T> and IConvertible
        //   NUNit 4.2.2: Succeeds as expected
        //   NUNit 4.3.0: IConvertible.ToInt32 is called and fails
        Assert.That(new Number(), Is.EqualTo(new Number()));
    }
}

public readonly struct Number : IEquatable<Number>, IConvertible
{
    // IEquatable<Number>
    public bool Equals(Number other)
      => true;

    // IConvertible
    TypeCode IConvertible.GetTypeCode() => TypeCode.Object;
    byte IConvertible.ToByte(IFormatProvider provider) => throw new NotImplementedException();
    sbyte IConvertible.ToSByte(IFormatProvider provider) => throw new NotImplementedException();
    ushort IConvertible.ToUInt16(IFormatProvider provider) => throw new NotImplementedException();
    short IConvertible.ToInt16(IFormatProvider provider) => throw new NotImplementedException();
    uint IConvertible.ToUInt32(IFormatProvider provider) => throw new NotImplementedException();
    int IConvertible.ToInt32(IFormatProvider provider) => throw new NotImplementedException();
    ulong IConvertible.ToUInt64(IFormatProvider provider) => throw new NotImplementedException();
    long IConvertible.ToInt64(IFormatProvider provider) => throw new NotImplementedException();
    string IConvertible.ToString(IFormatProvider provider) => throw new NotImplementedException();
    bool IConvertible.ToBoolean(IFormatProvider provider) => throw new NotImplementedException();
    char IConvertible.ToChar(IFormatProvider provider) => throw new NotImplementedException();
    DateTime IConvertible.ToDateTime(IFormatProvider provider) => throw new NotImplementedException();
    decimal IConvertible.ToDecimal(IFormatProvider provider) => throw new NotImplementedException();
    double IConvertible.ToDouble(IFormatProvider provider) => throw new NotImplementedException();
    float IConvertible.ToSingle(IFormatProvider provider) => throw new NotImplementedException();
    object IConvertible.ToType(Type conversionType, IFormatProvider provider) => throw new NotImplementedException();
}