namespace ListExtensions
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;


    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class IntIndexOfOnRangeBenchmark
    {
        private List<int> data = new List<int>();
        private int element = 0;

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

            int elementIndex = Random.Shared.Next(0, N);
            element = data[elementIndex];
        }

        [Benchmark]
        public int ListIndexOf() => data.IndexOfOnRangeListImpl(element, 0, N);

        [Benchmark]
        public int SpanIndexOf() => data.IndexOfOnRangeSpanImpl(element, 0, N);

        [Benchmark]
        public int SimdIndexOf() => data.IndexOfOnRangeSIMDImpl(element, 0, N);

        [Benchmark]
        public int LINQIndexOf()
        {
            return data.IndexOf(element);
        }

        [Benchmark]
        public int SliceIndexOf()
        {
            var rangedData = data.Slice(0, N);
            return rangedData.IndexOf(element);
        }

        [Benchmark]
        public int GetRangeIndexOf()
        {
            var rangedData = data.GetRange(0, N);
            return rangedData.IndexOf(element);
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class DoubleIndexOfOnRangeBenchmark
    {
        private List<double> data = new List<double>();
        private double element = 0;

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

            int elementIndex = Random.Shared.Next(0, N);
            element = data[elementIndex];
        }

        [Benchmark]
        public int ListIndexOf() => data.IndexOfOnRangeListImpl(element, 0, N);

        [Benchmark]
        public int SpanIndexOf() => data.IndexOfOnRangeSpanImpl(element, 0, N);

        [Benchmark]
        public int SimdIndexOf() => data.IndexOfOnRangeSIMDImpl(element, 0, N);

        [Benchmark]
        public int LINQIndexOf()
        {
            return data.IndexOf(element);
        }

        [Benchmark]
        public int SliceIndexOf()
        {
            var rangedData = data.Slice(0, N);
            return rangedData.IndexOf(element);
        }

        [Benchmark]
        public int GetRangeIndexOf()
        {
            var rangedData = data.GetRange(0, N);
            return rangedData.IndexOf(element);
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class StringIndexOfOnRangeBenchmark
    {
        private List<string> data = new List<string>();
        private string element = string.Empty;

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

            int elementIndex = Random.Shared.Next(0, N);
            element = data[elementIndex];
        }

        [Benchmark]
        public int ListIndexOf() => data.IndexOfOnRangeListImpl(element, 0, N);

        [Benchmark]
        public int SpanIndexOf() => data.IndexOfOnRangeSpanImpl(element, 0, N);

        [Benchmark]
        public int SimdIndexOf() => data.IndexOfOnRangeSIMDImpl(element, 0, N);

        [Benchmark]
        public int LINQIndexOf()
        {
            return data.IndexOf(element);
        }

        [Benchmark]
        public int SliceIndexOf()
        {
            var rangedData = data.Slice(0, N);
            return rangedData.IndexOf(element);
        }

        [Benchmark]
        public int GetRangeIndexOf()
        {
            var rangedData = data.GetRange(0, N);
            return rangedData.IndexOf(element);
        }
    }
}

