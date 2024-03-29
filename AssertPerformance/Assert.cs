﻿using System;
using System.Runtime.CompilerServices;

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

        public static void IsNotMultipleOf100FormattedWithLineNumber(int value, FormattableString message, [CallerLineNumber] int linenr = 0)
        {
            if (IsMultipleOf100(value))
            {
                throw new AssertException(message.ToString()+$"  Linenr: {linenr}");
            }
        }

        public static void IsNotMultipleOf100Default(int value, string message="")
        {
            if (IsMultipleOf100(value))
            {
                throw new AssertException(message);
            }
        }

        public static void IsNotMultipleOf100DefaultWithLineNumber(int value, string message = "", [CallerLineNumber] int linenr=0)
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
