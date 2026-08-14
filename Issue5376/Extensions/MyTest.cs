namespace Extensions;

public interface IMyTest { }

public class MyTestAttribute : TestAttribute, IMyTest{ }

public class MyTestCaseAttribute : TestCaseAttribute, IMyTest { }
