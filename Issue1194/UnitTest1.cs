namespace Issue1194;

public class Tests
{


    [Test(ExpectedResult = true)]
    public bool LessThan([ValueSource(nameof(X))] int x, [ValueSource(nameof(Y))] int y)
    {
        return x < y;
    }

    static int[] X = new int[] { 1, 2, 3, 4 };
    static int[] Y = new int[] { 5, 6, 7, 8 };
}
