using demo.protocol.helper;
using NUnitLite;

namespace demo.testenv
{
    static class Program
    {
        static int Main(string[] args)
        {
            try
            {
                return new AutoRun().Execute(args);
            }
            finally
            {
                Console.WriteLine("\n================================   Test Log   =============================");
                Console.WriteLine(TestLog.Logs.ToString());
                Console.WriteLine("===========================================================================");
            }
        }
    }
}
