namespace DevelopListExtensions
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Running;

    public class Program
    {
        public static void Main(string[] args)
        {
            //RunMaxOnRangeBenchmarks();
            //RunMaxIndexOnRangeBenchmarks();
            //RunIndexOfOnRangeBenchmarks();
            //RunAggregateOnRangeBenchmarks();
            RunForEachOnRangeBenchmarks();
        }

        private static void RunMaxOnRangeBenchmarks()
        {
            //var result1 = BenchmarkRunner.Run<IntMaxRangeBenchmnark>();
            //var result2 = BenchmarkRunner.Run<DoubleMaxRangeBenchmnark>();
            var result3 = BenchmarkRunner.Run<StringMaxRangeBenchmnark>();
        }

        private static void RunMaxIndexOnRangeBenchmarks()
        {
            //var result1 = BenchmarkRunner.Run<IntMaxIndexOnRangeBenchmark>();
            //var result2 = BenchmarkRunner.Run<DoubleMaxIndexOnRangeBenchmark>();
            //var result3 = BenchmarkRunner.Run<StringMaxIndexOnRangeBenchmark>();
        }

        private static void RunIndexOfOnRangeBenchmarks()
        {
            //var result1 = BenchmarkRunner.Run<IntIndexOfOnRangeBenchmark>();
            //var result2 = BenchmarkRunner.Run<DoubleIndexOfOnRangeBenchmark>();
            var result3 = BenchmarkRunner.Run<StringIndexOfOnRangeBenchmark>();
        }

        private static void RunAggregateOnRangeBenchmarks()
        {
            //var result1 = BenchmarkRunner.Run<IntSumAggregateOnRangeBenchmnark>();
            //var result2 = BenchmarkRunner.Run<DoubleSumAggregateOnRangeBenchmnark>();
            var result3 = BenchmarkRunner.Run<StringLengthAggregateOnRangeenchmnark>();
        }

        private static void RunForEachOnRangeBenchmarks()
        {
            //var result1 = BenchmarkRunner.Run<IntForEachOnRangeBenchmark>();
            //var result2 = BenchmarkRunner.Run<DoubleForEachOnRangeBenchmark>();
            var result3 = BenchmarkRunner.Run<StringForEachOnRangeBenchmark>();
        }
    }
}