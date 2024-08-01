using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issue548
{
    public class MultipleProps
    {
        [Test]
        [Property("Answer", 42)]
        [Property("Answer", 84)]
        public void TestMultipleProps()
        {
            var answers = TestContext.CurrentContext.Test.AllPropertyValues("Answer");
            var answer = answers.FirstOrDefault() as int?;
            Assert.That(answer, Is.EqualTo(42));
            var props = TestContext.CurrentContext.Test.AllPropertyValues("Answer").Cast<int>().ToList();
            Assert.That(props, Has.Count.EqualTo(2));
            var prop = props[0] as int?;
            Assert.That(prop, Is.EqualTo(42));
            prop = props[1];
            Assert.That(prop, Is.EqualTo(84));
        }
    }
}
