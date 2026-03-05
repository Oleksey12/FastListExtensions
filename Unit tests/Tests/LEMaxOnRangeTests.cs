namespace Unit_tests.Tests
{
    using ListExtensions;

    public class LEMaxOnRangeTests
    {
        [Test, Category("Generic")]
        public void MaxOnRange_InputNull_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT

            // ASSERT
            Assert.Throws<NullReferenceException>(() => list.MaxOnRange());
        }

        [Test, Category("Generic")]
        public void MaxOnRange_FloatListWithNan_Throws()
        {
            // ARRANGE
            List<float> list = new List<float> { float.NaN };

            // ACT

            // ASSERT
            Assert.Throws<InvalidOperationException>(() => list.MaxOnRange());
        }

        [Test, Category("Generic")]
        public void MaxOnRange_DoubleListWithNan_Throws()
        {
            // ARRANGE
            List<double> list = new List<double> { double.NaN };

            // ACT

            // ASSERT
            Assert.Throws<InvalidOperationException>(() => list.MaxOnRange());
        }

        [Test, Category("Generic")]
        public void MaxOnRange_12345List6Count_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.MaxOnRange(0, 6));
        }

        [Test, Category("Generic")]
        public void MaxOnRange_NegativeSize_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.MaxOnRange(0, -3));
        }

        [Test, Category("Span")]
        public void MaxOnRange_OneElementListWith2_Returns2()
        {
            // ARRANGE
            List<int> list = new List<int> { 2 };

            // ACT
            int result = list.MaxOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(2));
        }

        [Test, Category("Span")]
        public void MaxOnRange_12345List_Returns5()
        {
            // ARRANGE
            List<int> list = new List<int> {1, 2, 3, 4, 5};

            // ACT
            int result = list.MaxOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(5));
        }

        [Test, Category("Span")]
        public void MaxOnRange_12345List_NotChanged()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            List<int> copy = new List<int>(list);

            // ACT
            int result = list.MaxOnRange();

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
        public void MaxOnRange_StringList_ReturnsLongest()
        {
            // ARRANGE
            List<string> list = new List<string> { "a", "ccc", "bb" };

            // ACT
            string result = list.MaxOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo("ccc"));
        }

        [Test, Category("Span")]
        public void MaxOnRange_StringList_ReturnsBiggest()
        {
            // ARRANGE
            List<string> list = new List<string> { "aaa", "ccc", "bbb" };

            // ACT
            string result = list.MaxOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo("ccc"));
        }

        [Test, Category("SIMD")]
        public void MaxOnRange_IntListfrom1to32_Returns32()
        {
            // ARRANGE
            List<int> list = Enumerable.Range(1, 32).ToList();

            // ACT
            int result = list.MaxOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(32));
        }

        [Test, Category("SIMD")]
        public void MaxOnRange_IntListfrom32to1_Returns32()
        {
            // ARRANGE
            List<int> list = new List<int>(32);
            for (int i = 32; i >= 1; i--)
            {
                list.Add(i);
            }

            // ACT
            int result = list.MaxOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(32));
        }

        [Test, Category("Span")]
        public void MaxOnRange_5Repeated10TimesList_Returns5()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(5, 10).ToList();

            // ACT
            int result = list.MaxOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(5));
        }

        [Test, Category("SIMD")]
        public void MaxOnRange_6Repeated100TimesList_Returns6()
        {
            // ARRANGE
            List<int> list = Enumerable.Repeat(6, 100).ToList();

            // ACT
            int result = list.MaxOnRange();

            // ASSERT
            Assert.That(result, Is.EqualTo(6));
        }
    }
}