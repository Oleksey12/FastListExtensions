namespace Unit_tests.Tests
{
    using FastListExtensions;

    public class LELastIndexOfOnRangeTests
    {
        [Test, Category("Generic")]
        public void LastIndexOfOnRange_InputNull_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT

            // ASSERT
            Assert.Throws<ArgumentNullException>(() => list.LastIndexOfOnRange(1));
        }

        [Test, Category("Generic")]
        public void LastIndexOfOnRange_FloatListNaNElement_Throws()
        {
            // ARRANGE
            List<float> list = new List<float> { 1 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => list.LastIndexOfOnRange(float.NaN));
        }

        [Test, Category("Generic")]
        public void LastIndexOfOnRange_DoubleListNaNElement_LastThrows()
        {
            // ARRANGE
            List<double> list = new List<double> { 1 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => list.LastIndexOfOnRange(double.NaN));
        }

        [Test, Category("Span")]
        public void LastIndexOfOnRange_FloatListWithNaN12_For1Return0()
        {
            // ARRANGE
            List<float> list = new List<float> {1, 2, float.NaN };

            // ACT

            // ASSERT
            Assert.That(list.LastIndexOfOnRange(1), Is.EqualTo(0));
        }

        [Test, Category("Span")]
        public void LastIndexOfOnRange_DoubleListWithNaN12_For1Return0()
        {
            // ARRANGE
            List<double> list = new List<double> { 1, 2, double.NaN };

            // ACT

            // ASSERT
            Assert.That(list.LastIndexOfOnRange(1), Is.EqualTo(0));
        }

        [Test, Category("Generic")]
        public void LastIndexOfOnRange_12345ListElement16Count_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.LastIndexOfOnRange(1, 0, 6));
        }

        [Test, Category("Generic")]
        public void LastIndexOfOnRange_NegativeSize_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.LastIndexOfOnRange(1, 0, -3));
        }

        [Test, Category("Span")]
        public void LastIndexOfOnRange_OneElementListWith2_For2Returns0()
        {
            // ARRANGE
            List<int> list = new List<int> { 2 };

            // ACT
            int result = list.LastIndexOfOnRange(2);

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("Span")]
        public void LastIndexOfOnRange_12345List_For5Returns4()
        {
            // ARRANGE
            List<int> list = new List<int> {1, 2, 3, 4, 5};

            // ACT
            int result = list.LastIndexOfOnRange(5);

            // ASSERT
            Assert.That(result, Is.EqualTo(4));
        }

        [Test, Category("Span")]
        public void LastIndexOfOnRange_12345List5Element_NotChanged()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            List<int> copy = new List<int>(list);

            // ACT
            int result = list.LastIndexOfOnRange(5);

            // ASSERT
            bool areEqual = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != copy[i])
                {
                    areEqual = false;
                }
            }

            Assert.That(areEqual);
        }

        [Test, Category("Span")]
        public void LastIndexOfOnRange_StringListbbElement_Returns2()
        {
            // ARRANGE
            List<string> list = new List<string> { "a", "ccc", "bb" };

            // ACT
            int result = list.LastIndexOfOnRange("bb");

            // ASSERT
            Assert.That(result, Is.EqualTo(2));
        }

        [Test, Category("Span")]
        public void LastIndexOfOnRange_StringListcccElement_Returns1()
        {
            // ARRANGE
            List<string> list = new List<string> { "aaa", "ccc", "bbb" };

            // ACT
            int result = list.LastIndexOfOnRange("ccc");

            // ASSERT
            Assert.That(result, Is.EqualTo(1));
        }

        [Test, Category("SIMD")]
        public void LastIndexOfOnRange_IntListfrom1to32_For32Returns31()
        {
            // ARRANGE
            List<int> list = Enumerable.Range(1, 32).ToList();

            // ACT
            int result = list.LastIndexOfOnRange(32);

            // ASSERT
            Assert.That(result, Is.EqualTo(31));
        }

        [Test, Category("SIMD")]
        public void LastIndexOfOnRange_IntListfrom32to1_For1Returns31()
        {
            // ARRANGE
            List<int> list = new List<int>(32);
            for (int i = 32; i >= 1; i--)
            {
                list.Add(i);
            }

            // ACT
            int result = list.LastIndexOfOnRange(1);

            // ASSERT
            Assert.That(result, Is.EqualTo(31));
        }

        [Test, Category("Span")]
        public void LastIndexOfOnRange_5Repeated10TimesList_For6ReturnsMinusOne()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(5, 10).ToList();

            // ACT
            int result = list.LastIndexOfOnRange(6);

            // ASSERT
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test, Category("SIMD")]
        public void LastIndexOfOnRange_IntListfrom32to1_For40ReturnsMinusOne()
        {
            // ARRANGE
            List<int> list = new List<int>(32);
            for (int i = 32; i >= 1; i--)
            {
                list.Add(i);
            }

            // ACT
            int result = list.LastIndexOfOnRange(40);

            // ASSERT
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test, Category("Span")]
        public void LastIndexOfOnRange_5Repeated10TimesList_For5Returns9()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(5, 10).ToList();

            // ACT
            int result = list.LastIndexOfOnRange(5);

            // ASSERT
            Assert.That(result, Is.EqualTo(9));
        }

        [Test, Category("SIMD")]
        public void LastIndexOfOnRange_6Repeated100TimesList_For6Returns99()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(6, 100).ToList();

            // ACT
            int result = list.LastIndexOfOnRange(6);

            // ASSERT
            Assert.That(result, Is.EqualTo(99));
        }
    }
}