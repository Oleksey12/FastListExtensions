namespace Unit_tests.Tests
{
    using ListExtensions;

    public class LEIndexOfOnRangeTests
    {
        [Test, Category("Generic")]
        public void IndexOfOnRange_InputNull_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT

            // ASSERT
            Assert.Throws<NullReferenceException>(() => list.IndexOfOnRange(1));
        }

        [Test, Category("Generic")]
        public void IndexOfOnRange_FloatListNaNElement_Throws()
        {
            // ARRANGE
            List<float> list = new List<float> { 1 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => list.IndexOfOnRange(float.NaN));
        }

        [Test, Category("Generic")]
        public void IndexOfOnRange_DoubleListNaNElement_Throws()
        {
            // ARRANGE
            List<double> list = new List<double> { 1 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => list.IndexOfOnRange(double.NaN));
        }

        [Test, Category("Span")]
        public void IndexOfOnRange_FloatListWithNaN12_For1Return0()
        {
            // ARRANGE
            List<float> list = new List<float> {1, 2, float.NaN };

            // ACT

            // ASSERT
            Assert.That(list.IndexOfOnRange(1), Is.EqualTo(0));
        }

        [Test, Category("Span")]
        public void IndexOfOnRange_DoubleListWithNaN12_For1Return0()
        {
            // ARRANGE
            List<double> list = new List<double> { 1, 2, double.NaN };

            // ACT

            // ASSERT
            Assert.That(list.IndexOfOnRange(1), Is.EqualTo(0));
        }

        [Test, Category("Generic")]
        public void IndexOfOnRange_12345ListElement16Count_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.IndexOf(1, 0, 6));
        }

        [Test, Category("Generic")]
        public void IndexOfOnRange_NegativeSize_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.IndexOf(1, 0, -3));
        }

        [Test, Category("Span")]
        public void IndexOfOnRange_OneElementListWith2_For2Returns0()
        {
            // ARRANGE
            List<int> list = new List<int> { 2 };

            // ACT
            int result = list.IndexOf(2);

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("Span")]
        public void IndexOfOnRange_12345List_For5Returns4()
        {
            // ARRANGE
            List<int> list = new List<int> {1, 2, 3, 4, 5};

            // ACT
            int result = list.IndexOf(5);

            // ASSERT
            Assert.That(result, Is.EqualTo(4));
        }

        [Test, Category("Span")]
        public void IndexOfOnRange_12345List5Element_NotChanged()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            List<int> copy = new List<int>(list);

            // ACT
            int result = list.IndexOf(5);

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
        public void IndexOfOnRange_StringListbbElement_Returns2()
        {
            // ARRANGE
            List<string> list = new List<string> { "a", "ccc", "bb" };

            // ACT
            int result = list.IndexOf("bb");

            // ASSERT
            Assert.That(result, Is.EqualTo(2));
        }

        [Test, Category("Span")]
        public void IndexOfOnRange_StringListcccElement_Returns1()
        {
            // ARRANGE
            List<string> list = new List<string> { "aaa", "ccc", "bbb" };

            // ACT
            int result = list.IndexOf("ccc");

            // ASSERT
            Assert.That(result, Is.EqualTo(1));
        }

        [Test, Category("SIMD")]
        public void IndexOfOnRange_IntListfrom1to32_For32Returns31()
        {
            // ARRANGE
            List<int> list = Enumerable.Range(1, 32).ToList();

            // ACT
            int result = list.IndexOf(32);

            // ASSERT
            Assert.That(result, Is.EqualTo(31));
        }

        [Test, Category("SIMD")]
        public void IndexOfOnRange_IntListfrom32to1_For1Returns31()
        {
            // ARRANGE
            List<int> list = new List<int>(32);
            for (int i = 32; i >= 1; i--)
            {
                list.Add(i);
            }

            // ACT
            int result = list.IndexOf(1);

            // ASSERT
            Assert.That(result, Is.EqualTo(31));
        }

        [Test, Category("Span")]
        public void IndexOfOnRange_5Repeated10TimesList_For6ReturnsMinusOne()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(5, 10).ToList();

            // ACT
            int result = list.IndexOf(6);

            // ASSERT
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test, Category("SIMD")]
        public void IndexOfOnRange_IntListfrom32to1_For40ReturnsMinusOne()
        {
            // ARRANGE
            List<int> list = new List<int>(32);
            for (int i = 32; i >= 1; i--)
            {
                list.Add(i);
            }

            // ACT
            int result = list.IndexOf(40);

            // ASSERT
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test, Category("Span")]
        public void IndexOfOnRange_5Repeated10TimesList_For5Returns0()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(5, 10).ToList();

            // ACT
            int result = list.IndexOf(5);

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("SIMD")]
        public void IndexOfOnRange_6Repeated100TimesList_For6Returns6()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(6, 100).ToList();

            // ACT
            int result = list.IndexOf(6);

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }
    }
}