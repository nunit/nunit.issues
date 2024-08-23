using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repro
{
    internal class Issue4795Repro
    {
        public class Repro
        {
            [Test]
            public void TestEnum()
            {
                Assert.That(Foo.Bar, Is.EqualTo(Foo.Bar));
            }

            [Test]
            public void TestChar()
            {
                Assert.That('x', Is.EqualTo('x'));
            }
        }

        public enum Foo
        {
            Bar = 1,
        }
    }
}
