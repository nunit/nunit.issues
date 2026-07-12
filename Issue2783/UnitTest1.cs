namespace Issue2783;

public class Tests
{
    [TestCase(0x00FFFFFF, TestName = "{m}(Clear)")]
    [TestCase(0xFFFFFFFF, TestName = "{m}(White)")]
    public void UIntTest(uint stopColor)
    {
    }
}
