namespace Unit_tests.Tests
{
    using FastListExtensions;

    public class LEMinIndexOnRangeTests
    {
        [Test, Category("Generic")]
        public void MinIndexOnRange_InputNull_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT

            // ASSERT
            Assert.Throws<ArgumentNullException>(() => list.MinIndexOnRange());
        }

        [Test, Category("Generic")]
        public void MinIndexOnRange_FloatListWithNan_Throws()
        {
            // ARRANGE
            List<float> list = new List<float> { float.NaN };

            // ACT

            // ASSERT
            Assert.Throws<InvalidOperationException>(() => list.MinIndexOnRange());
        }

        [Test, Category("Generic")]
        public void MinIndexOnRange_DoubleListWithNan_Throws()
        {
            // ARRANGE
            List<double> list = new List<double> { double.NaN };

            // ACT

            // ASSERT
            Assert.Throws<InvalidOperationException>(() => list.MinIndexOnRange());
        }

        [Test, Category("Generic")]
        public void MinIndexOnRange_12345List6Count_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.MinIndexOnRange(0, 6));
        }

        [Test, Category("Generic")]
        public void MinIndexOnRangee_NegativeSize_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.MinIndexOnRange(0, -3));
        }

        [Test, Category("Generic")]
        public void MinIndexOnRange_12345List_Range03Returns0()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int result = list.MinIndexOnRange(0, 3);

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("Generic")]
        public void MinIndexOnRange_12345List_Range23Returns0()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int result = list.MinIndexOnRange(2, 3);

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }


        [Test, Category("Span")]
        public void MinIndexOnRange_OneElementListWith2_Returns0()
        {
            // ARRANGE
            List<int> list = new List<int> { 2 };

            // ACT
            int result = list.MinIndexOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("Span")]
        public void MinIndexOnRange_12345List_Returns0()
        {
            // ARRANGE
            List<int> list = new List<int> {1, 2, 3, 4, 5};

            // ACT
            int result = list.MinIndexOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("Span")]
        public void MinIndexOnRange_12345List_NotChanged()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            List<int> copy = new List<int>(list);

            // ACT
            int result = list.MinIndexOnRange();

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
        public void MinIndexOnRange_StringList_ReturnsShortest0()
        {
            // ARRANGE
            List<string> list = new List<string> { "a", "ccc", "bb" };

            // ACT
            int result = list.MinIndexOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("Span")]
        public void MinIndexOnRange_StringList_ReturnsSmallest0()
        {
            // ARRANGE
            List<string> list = new List<string> { "aaa", "ccc", "bbb" };

            // ACT
            int result = list.MinIndexOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("SIMD")]
        public void MinIndexOnRange_IntListfrom1to32_Returns0()
        {
            // ARRANGE
            List<int> list = Enumerable.Range(1, 32).ToList();

            // ACT
            int result = list.MinIndexOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("SIMD")]
        public void MinIndexOnRange_IntListfrom32to1_Returns31()
        {
            // ARRANGE
            List<int> list = new List<int>(32);
            for (int i = 32; i >= 1; i--)
            {
                list.Add(i);
            }

            // ACT
            int result = list.MinIndexOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(31));
        }

        [Test, Category("Span")]
        public void MinIndexOnRange_5Repeated10TimesList_Returns0()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(5, 10).ToList();

            // ACT
            int result = list.MinIndexOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("SIMD")]
        public void MinIndexOnRange_6Repeated100TimesList_Returns0()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(6, 100).ToList();

            // ACT
            int result = list.MinIndexOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(0));
        }
    }
}