namespace DevelopListExtensions
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class IntSumAggregateOnRangeBenchmnark
    {
        private List<int> data = new List<int>();
        private int result = 0;
        private int startValue = 0;

        [Params(10, 100, 1000, 10000, 100000)]
        public int N = 100;

        [GlobalSetup]
        public void Setup()
        {
            result = 0;
            data.Clear();
            for (int i = 0; i < N; i++)
            {
                data.Add(Random.Shared.Next(int.MinValue, int.MaxValue));
            }
        }

        [Benchmark]
        public int ListAggregate() => data.AggregateOnRangeListImpl((first, second) => first + second, startValue,  0, N);

        [Benchmark]
        public int SpanAggregate() => data.AggregateOnRangeSpanImpl((first, second) => first + second, startValue, 0, N);

        [Benchmark]
        public int PrimitiveSumAlgorithm() => Calculate();

        private int Calculate()
        {
            result = startValue + data[0];
            for (int i = 1; i < data.Count; i++)
            {
                result += data[i];
            }
            return result;
        }

        [Benchmark]
        public int LINQAggregate() => data.Aggregate((first, second) => first + second);

        [Benchmark]
        public int SliceLINQAggregate()
        {
            var rangedData = data.Slice(0, N);
            return rangedData.Aggregate((first, second) => first + second);
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class DoubleSumAggregateOnRangeBenchmnark
    {
        private List<double> data = new List<double>();
        private double result = 0;
        private double startValue = 0;

        [Params(10, 100, 1000, 10000, 100000)]
        public int N = 100;

        [GlobalSetup]
        public void Setup()
        {
            result = 0;
            data.Clear();
            for (int i = 0; i < N; i++)
            {
                data.Add(Random.Shared.Next(int.MinValue, int.MaxValue));
            }
        }

        [Benchmark]
        public double ListAggregate() => data.AggregateOnRangeListImpl((first, second) => first + second, startValue, 0, N);

        [Benchmark]
        public double SpanAggregate() => data.AggregateOnRangeSpanImpl((first, second) => first + second, startValue, 0, N);

        [Benchmark]
        public double PrimitiveSumAlgorithm()
        {
            result = startValue + data[0];
            for (int i = 1; i < data.Count; i++)
            {
                result += data[i];
            }
            return result;
        }

        [Benchmark]
        public double LINQAggregate() => data.Aggregate((first, second) => first + second);

        [Benchmark]
        public double SliceLINQAggregate()
        {
            var rangedData = data.Slice(0, N);
            return rangedData.Aggregate((first, second) => first + second);
        }
    }

    [SimpleJob(RuntimeMoniker.Net80, launchCount: 1, warmupCount: 5, iterationCount: 10)]
    [RPlotExporter]
    public class StringLengthAggregateOnRangeenchmnark
    {
        private List<string> data = new List<string>();
        private int result = 0;
        private int startValue = 0;

        [Params(10, 100, 1000, 10000, 100000)]
        public int N = 100;

        [GlobalSetup]
        public void Setup()
        {
            result = 0;
            data.Clear();
            for (int i = 0; i < N; i++)
            {
                data.Add(Random.Shared.Next(int.MinValue, int.MaxValue).ToString());
            }
        }

        [Benchmark]
        public int ListAggregate() => data.AggregateOnRangeListImpl((first, second) => first + second.Length, startValue, 0, N);

        [Benchmark]
        public int SpanAggregate() => data.AggregateOnRangeSpanImpl((first, second) => first + second.Length, startValue, 0, N);

        [Benchmark]
        public int PrimitiveSumAlgorithm()
        {
            result = startValue + data[0].Length;
            for (int i = 1; i < data.Count; i++)
            {
                result += data[i].Length;
            }
            return result;
        }

        [Benchmark]
        public int LINQAggregate() 
        {
            throw new NotSupportedException();
        }

        [Benchmark]
        public int SliceLINQAggregate()
        {
            throw new NotSupportedException();
        }
    }
}

