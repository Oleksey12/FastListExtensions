namespace DevelopListExtensions
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class IntMaxIndexOnRangeBenchmark
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
        public int ListMaxIndex() => data.MaxIndexOnRangeListImpl(0, N);

        [Benchmark]
        public int SpanMaxIndex() => data.MaxIndexOnRangeSpanImpl(0, N);

        [Benchmark]
        public int SimdMaxIndex() => data.MaxIndexOnRangeSIMDImpl(0, N);

        [Benchmark]
        public int MaxIndexOf()
        {
            var max = data.Max();
            return data.IndexOf(max);
        }

        [Benchmark]
        public int SliceMaxIndexOf()
        {
            var rangedData = data.Slice(0, N);
            var max = rangedData.Max();
            return rangedData.IndexOf(max);
        }

        [Benchmark]
        public int GetRangeMaxIndexOf()
        {
            var rangedData = data.GetRange(0, N);
            var max = rangedData.Max();
            return rangedData.IndexOf(max);
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class DoubleMaxIndexOnRangeBenchmark
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
        public int ListMaxIndex() => data.MaxIndexOnRangeListImpl(0, N);

        [Benchmark]
        public int SpanMaxIndex() => data.MaxIndexOnRangeSpanImpl(0, N);

        [Benchmark]
        public int SimdMaxIndex() => data.MaxIndexOnRangeSIMDImpl(0, N);

        [Benchmark]
        public int MaxIndexOf()
        {
            var max = data.Max();
            return data.IndexOf(max);
        }

        [Benchmark]
        public int SliceMaxIndexOf()
        {
            var rangedData = data.Slice(0, N);
            var max = rangedData.Max();
            return rangedData.IndexOf(max);
        }

        [Benchmark]
        public int GetRangeMaxIndexOf()
        {
            var rangedData = data.GetRange(0, N);
            var max = rangedData.Max();
            return rangedData.IndexOf(max);
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class StringMaxIndexOnRangeBenchmark
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
        public int ListMaxIndex() => data.MaxIndexOnRangeListImpl(0, N);

        [Benchmark]
        public int SpanMaxIndex() => data.MaxIndexOnRangeSpanImpl(0, N);

        [Benchmark]
        public int SimdMaxIndex() => data.MaxIndexOnRangeSIMDImpl(0, N);

        [Benchmark]
        public int RangeMaxIndexOf()
        {
            var max = data.Max();
            return data.IndexOf(max);
        }

        [Benchmark]
        public int SliceMaxIndexOf()
        {
            var rangedData = data.Slice(0, N);
            var max = rangedData.Max();
            return rangedData.IndexOf(max);
        }

        [Benchmark]
        public int GetRangeMaxIndexOf()
        {
            var rangedData = data.GetRange(0, N);
            var max = rangedData.Max();
            return rangedData.IndexOf(max);
        }
    }
}

