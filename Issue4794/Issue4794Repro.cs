using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Repro
{

    public class Issue4794Repro
    {

        [Test]
        public void TestCollectionCount()
        {
            var utility = new UtilityClass("something\nto\nparse");
            Assert.That(utility.Lines, Has.Count.EqualTo(3)); // throws
                                                              //Assert.That(utility.Lines.Count(), Is.EqualTo(3)); // also throws
                                                              //ClassicAssert.AreEqual(3, utility.Lines.Count()); // also throws
        }
        [TestCase(null, false)]
        [TestCase("anything", true)]
        public void TestCollectionEmpty(string input, bool expected)
        {
            var utility = new UtilityClass(input);
            Assert.That(utility.HasLines, Is.EqualTo(expected)); // throws
        }

        [Test]
        public void SanityTest()
        {
            Assert.Pass();
        }
    }

    // with a simplified (and somewhat pointless) test class:
    class UtilityClass
    {
        public UtilityClass(string? lines)
        {
            Lines = lines?.Split('\n').ToList() ?? [];
            HasLines = Lines.Count > 0;
        }
        public List<string> Lines { get; }
        public bool HasLines { get; }
    }

}