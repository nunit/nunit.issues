namespace Issue5031;

public class Tests
{
    private int i = 1;
    [Repeat(5)]
    [Test]
    public void Test1()
    {
        Log($"{i++} hi");
    }

    private void Log(string msg)
    {
        File.AppendAllText(@"..\..\..\log.log", msg + "\n");
        Console.WriteLine(msg);
    }
}
