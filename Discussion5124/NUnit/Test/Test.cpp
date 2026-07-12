using namespace NUnit::Framework;

[TestFixture]
ref struct TestFixture
{
  [Test]
  void Test()
  {
    Assert::That(4, Is::EqualTo(2 * 2));
  }
};
