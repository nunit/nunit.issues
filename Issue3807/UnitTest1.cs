using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issue3807
{
    internal class UnitTest1
    {
        [Test]
        public void ContainsKeyRespectsIEquatable()
        {
            const string myGuidString1 = "00000000-0000-0000-0000-000000000001";
            const string myGuidString2 = "00000000-0000-0000-0000-000000000002";

            var actual = new Dictionary<FooGuid, Guid> {
                {FooGuid.Parse(myGuidString1), Guid.Parse(myGuidString1)},
                {Guid.Parse(myGuidString2), FooGuid.Parse(myGuidString2)}
            };

            var expectedGuid = Guid.Parse(myGuidString2);

            // Calling the ContainsKey method directly works
            Assert.That(actual.ContainsKey(expectedGuid), Is.True);

            // Allowing DictionaryContainsKeyConstraint to call it fails
            Assert.That(actual, Does.ContainKey(expectedGuid));
            //new DictionaryContainsKeyConstraint(expectedGuid).ApplyTo(actual).IsSuccess.Dump("Call via invocation");
        }

        public struct FooGuid : IEquatable<FooGuid>, IEquatable<Guid>
        {
            public Guid Value { get; }

            public FooGuid(Guid value) { Value = value; }

            public static FooGuid Parse(string patientId) => new FooGuid(Guid.Parse(patientId));

            public bool Equals(FooGuid other) => Equals(Value, other.Value);
            public bool Equals(Guid other) => Value.Equals(other);

            public static implicit operator FooGuid(Guid value) => new FooGuid(value);
            public static implicit operator Guid(FooGuid valueType) => valueType.Value;
        }
    }
}
