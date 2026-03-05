namespace ListExtensions
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Running;

    public class Program
    {
        public static void Main(string[] args)
        {
            RunMaxOnRangeBenchmarks();
        }

        private static void RunMaxOnRangeBenchmarks()
        {
            //var result1 = BenchmarkRunner.Run<IntMaxRangeBenchmnark>();
            //var result2 = BenchmarkRunner.Run<DoubleMaxRangeBenchmnark>();
            var result3 = BenchmarkRunner.Run<StringMaxRangeBenchmnark>();
        }
    }
}