namespace ListExtensions
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class IntForEachOnRangeBenchmark
    {
        private List<int> data = new List<int>();
        private int res = 0;

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
        public void ListForEachOnRange() => data.ForEachOnRangeListImpl(x => res = x, 0, N);

        [Benchmark]
        public void SpanForEachOnRange() => data.ForEachOnRangeSpanImpl(x => res = x, 0, N);

        [Benchmark]
        public void PrimitiveAlgorithm() => Calculate();

        private void Calculate()
        {
            for (int i = 0; i < N; i++)
            {
                res = data[i];
            }
        }

        [Benchmark]
        public void ListForEach() => data.ForEach(x => res = x);

        [Benchmark]
        public void SliceListForEach()
        {
            var rangedData = data.Slice(0, N);
            data.ForEach(x => res = x);
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class DoubleForEachOnRangeBenchmark
    {
        private List<double> data = new List<double>();
        private double res = 0;

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
        public void ListForEachOnRange() => data.ForEachOnRangeListImpl(x => res = x, 0, N);

        [Benchmark]
        public void SpanForEachOnRange() => data.ForEachOnRangeSpanImpl(x => res = x, 0, N);

        [Benchmark]
        public void PrimitiveAlgorithm() => Calculate();

        private void Calculate()
        {
            for (int i = 0; i < N; i++)
            {
                res = data[i];
            }
        }

        [Benchmark]
        public void ListForEach() => data.ForEach(x => res = x);

        [Benchmark]
        public void SliceListForEach()
        {
            var rangedData = data.Slice(0, N);
            data.ForEach(x => res = x);
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class StringForEachOnRangeBenchmark
    {
        private List<string> data = new List<string>();
        private string res = "";

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
        public void ListForEachOnRange() => data.ForEachOnRangeListImpl(x => res = x, 0, N);

        [Benchmark]
        public void SpanForEachOnRange() => data.ForEachOnRangeSpanImpl(x => res = x, 0, N);

        [Benchmark]
        public void PrimitiveAlgorithm() => Calculate();

        private void Calculate()
        {
            for (int i = 0; i < N; i++)
            {
                res = data[i];
            }
        }

        [Benchmark]
        public void ListForEach() => data.ForEach(x => res = x);

        [Benchmark]
        public void SliceListForEach()
        {
            var rangedData = data.Slice(0, N);
            data.ForEach(x => res = x);
        }
    }
}

