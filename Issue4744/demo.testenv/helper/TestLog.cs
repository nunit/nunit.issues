using System.Runtime.CompilerServices;
using System.Text;

namespace demo.protocol.helper
{
    internal static class TestLog
    {
        public static StringBuilder Logs { get; } = new StringBuilder();

        public static void Log(string message, [CallerMemberName] string callerMethodName = "")
        {
            Logs.AppendLine($"├ {callerMethodName} ──> {message} ");
        }
    }
}
