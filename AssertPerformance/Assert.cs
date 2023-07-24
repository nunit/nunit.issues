using System;

namespace AssertPerformance
{
    internal abstract class Assert
    {
        public static void IsNotMultipleOf100FormatAndArguments(int value, string format, params object[] args)
        {
            if (IsMultipleOf100(value))
            {
                throw new AssertException(string.Format(format, args));
            }
        }

        public static void IsNotMultipleOf100Preformatted(int value, string message)
        {
            if (IsMultipleOf100(value))
            {
                throw new AssertException(message);
            }
        }

        public static void IsNotMultipleOf100Formattable(int value, FormattableString message)
        {
            if (IsMultipleOf100(value))
            {
                throw new AssertException(message.ToString());
            }
        }

        public static bool IsMultipleOf100(int value) => value % 100 == 0;

    }

    internal sealed class AssertException : Exception
    {
        public AssertException(string message)
            : base(message)
        {
        }
    }
}
