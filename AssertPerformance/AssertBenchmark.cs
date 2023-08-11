using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using System;

namespace AssertPerformance
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [HideColumns(Column.Job, Column.Error, Column.StdDev, Column.Median, Column.RatioSD, Column.Gen0)]
    public class AssertBenchmark
    {
        private const int TestValue = 99;

        [Benchmark(Baseline = true)]
        public void UseFormatAndArguments()
        {
            UseFormatAndArguments(TestValue);
        }

        [Benchmark]
        public void UsePreformattedMessage()
        {
            UsePreformattedMessage(TestValue);
        }

        [Benchmark]
        public void UseFormattableMessage()
        {
            UseFormattableMessage(TestValue);
        }


        [Benchmark]
        public void UseNonformattedMessage()
        {
            UseNonformattedMessage(TestValue);
        }

        [Benchmark]
        public void UseDefaultMessage()
        {
            UseDefaultMessage(TestValue);
        }

        public static void UseFormatAndArguments(int value)
        {
            Assert.IsNotMultipleOf100FormatAndArguments(value, "Expected {0} not to be a multiple of 100", value);
        }

        public static void UsePreformattedMessage(int value)
        {
            Assert.IsNotMultipleOf100Preformatted(value, $"Expected {value} not to be a multiple of 100");
        }

        public static void UseFormattableMessage(int value)
        {
            Assert.IsNotMultipleOf100Formattable(value, $"Expected {value} not to be a multiple of 100");
        }

        public static void UseDefaultMessage(int value)
        {
            Assert.IsNotMultipleOf100Default(value);
        }

        public static void UseNonformattedMessage(int value)
        {
            Assert.IsNotMultipleOf100Default(value,"Some arbitrary text");
        }
    }
}
