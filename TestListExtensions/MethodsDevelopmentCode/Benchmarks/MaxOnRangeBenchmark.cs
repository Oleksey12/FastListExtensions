namespace DevelopListExtensions
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class IntMaxRangeBenchmnark
    {
        private List<int> data = new List<int>();

        [Params(10, 100, 1000, 10000, 100000)]
        public int N = 100;

        [GlobalSetup]
        public void Setup()
        {
            data.Clear();
            for (int i = 0; i < N; i++)
            {
                data.Add(Random.Shared.Next(int.MinValue, int.MaxValue));
            }
        }

        [Benchmark]
        public int ListMax() => data.MaxOnRangeListGenericImpl(0, N);

        [Benchmark]
        public int SpanMax() => data.MaxOnRangeGenericImpl(0, N);

        [Benchmark]
        public int SimdMax() => data.MaxOnRangeSIMDImpl(0, N);

        [Benchmark]
        public int LINQMax() => data.Max();

        [Benchmark]
        public int SliceMax()
        {
            var rangedData = data.Slice(0, N);
            return rangedData.Max();
        }

        [Benchmark]
        public int GetRangeMax()
        {
            var rangedData = data.GetRange(0, N);
            return rangedData.Max();
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class DoubleMaxRangeBenchmnark
    {
        private List<double> data = new List<double>();

        [Params(10, 100, 1000, 10000, 100000)]
        public int N = 100;

        [GlobalSetup]
        public void Setup()
        {
            data.Clear();
            for (int i = 0; i < N; i++)
            {
                data.Add(Random.Shared.Next(int.MinValue, int.MaxValue));
            }
        }

        [Benchmark]
        public double ListMax() => data.MaxOnRangeListGenericImpl(0, N);

        [Benchmark]
        public double SpanMax() => data.MaxOnRangeGenericImpl(0, N);

        [Benchmark]
        public double SimdMax() => data.MaxOnRangeSIMDImpl(0, N);

        [Benchmark]
        public double LINQMax() => data.Max();

        [Benchmark]
        public double SliceMax()
        {
            var rangedData = data.Slice(0, N);
            return rangedData.Max();
        }

        [Benchmark]
        public double GetRangeMax()
        {
            var rangedData = data.GetRange(0, N);
            return rangedData.Max();
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class StringMaxRangeBenchmnark
    {
        private List<string> data = new List<string>();

        [Params(10, 100, 1000, 10000, 100000)]
        public int N = 100;

        [GlobalSetup]
        public void Setup()
        {
            data.Clear();
            for (int i = 0; i < N; i++)
            {
                data.Add(Random.Shared.Next(int.MinValue, int.MaxValue).ToString());
            }
        }

        [Benchmark]
        public string ListMax() => data.MaxOnRangeListGenericImpl(0, N);

        [Benchmark]
        public string SpanMax() => data.MaxOnRangeGenericImpl(0, N);

        [Benchmark]
        public string SimdMax() => data.MaxOnRangeSIMDImpl(0, N);

        [Benchmark]
        public string LINQMax() => data.Max();

        [Benchmark]
        public string SliceMax()
        {
            var rangedData = data.Slice(0, N);
            return rangedData.Max();
        }

        [Benchmark]
        public string GetRangeMax()
        {
            var rangedData = data.GetRange(0, N);
            return rangedData.Max();
        }
    }
}

