using NUnit.Framework;

namespace Issue3678;

public class Issue3678Tests
{
    [Test]
    public void StringDiffCaretOffsetWithInvisibleCharacters()
    {
        var actual = "\u200E\u200E\u200E\u200E\u200E\u200E\u200E\u200E1768";
        var expected = "\u200E\u200E\u200E\u200E\u200E\u200E\u200E\u200E\u200E768";

        Assert.That(actual, Is.EqualTo(expected));
    }
}
