using NUnit.Framework.Constraints;

namespace Issue4545;

[TestFixture]
public class MyTests
{
    [Test]
    public void ShouldWriteSameMessage()
    {
        var actualValue = false;
        Assert.Multiple(() =>
        {
            Assert.That(actualValue, new MyConstraint());
            Assert.That(actualValue, new MyConstraint().After(20));
        });
    }

    public class MyConstraint : Constraint
    {
        public override string Description => "Custom description";

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            return new MyConstraintResult(this);
        }
    }

    public class MyConstraintResult : ConstraintResult
    {
        public override void WriteMessageTo(MessageWriter writer)
        {
            writer.Write("Custom message");
        }

        public MyConstraintResult(MyConstraint myConstraint) : base(myConstraint, actualValue: null, ConstraintStatus.Failure)
        {
        }
    }
}