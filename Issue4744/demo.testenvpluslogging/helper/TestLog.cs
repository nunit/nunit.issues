using System.Runtime.CompilerServices;
using System.Text;

namespace demo.protocol.helper
{
    internal static class TestLog
    {
        public static StringBuilder Logs { get; } = new StringBuilder();

        private static string Context;

        public static void SetContext(string context)
        {
            if (!string.IsNullOrEmpty(Context))
            {
                throw new Exception("Misuse of TestLog context: existing context cannot be overwritten by a new one! First it has to be reset!");
            }
            Logs.AppendLine($"├   --------- entering {context} ------------");
            Context = context;
        }

        public static void ResetContext()
        {
            if (string.IsNullOrEmpty(Context))
            {
                throw new Exception("Misuse of TestLog context: without an existing context reset was called!");
            }
            Logs.AppendLine($"├   --------- leaving {Context} ------------");
            Context = null;
        }

        public static void Log(string message, [CallerMemberName] string callerMethodName = "")
        {
            string prefix;
            if (string.IsNullOrEmpty(Context))
            {
                prefix = "├ ";
            }
            else
            {
                prefix = $"├   | ";
            }

            Logs.AppendLine($"{prefix}{callerMethodName} ──> {message} ");
        }
    }
}
