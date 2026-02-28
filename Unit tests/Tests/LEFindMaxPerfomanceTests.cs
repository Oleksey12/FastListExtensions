namespace Unit_tests.Tests
{
    using LE;
    using System.Diagnostics;

    [TestFixture, Category("Perfomance")]
    public class LEFindMaxPerfomanceTests
    {
        [TestCase(5)]
        [TestCase(100000)]
        [TestCase(10000000)]
        [TestCase(100000000)]
        public void FindMax_IntList_MeasureTime(int count)
        {
            List<int> list1 = new List<int>(count);
            List<int> list2 = new List<int>(count);

            for (int i = 0; i < count; i++)
            {
                list1.Add(Random.Shared.Next(int.MinValue, int.MaxValue));
                list2.Add(Random.Shared.Next(int.MinValue, int.MaxValue));
            }

            double firstMaxTime = 0;
            double secondMaxTime = 0;

            // Warmup
            list1.SimpleMax();
            list1.SimpleMax();
            list2.Max();
            list2.Max();

            Stopwatch sw = Stopwatch.StartNew();
            list1.SimpleMax();
            firstMaxTime = sw.Elapsed.TotalMilliseconds;
            sw.Stop();

            sw.Restart();
            list2.Max();
            secondMaxTime = sw.Elapsed.TotalMilliseconds;
            sw.Stop();

            TestContext.WriteLine($"Время выполнения для кастомной функции {list1.Count} = {firstMaxTime} ms");
            TestContext.WriteLine($"Время выполнения для LINQ функции {list2.Count} = {secondMaxTime} ms");
        }
    }
}