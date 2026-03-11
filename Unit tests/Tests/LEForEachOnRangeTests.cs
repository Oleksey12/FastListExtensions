namespace Unit_tests.Tests
{
    using ListExtensions;

    public class LEForEachOnRangeTests
    {
        private int test = 0;

        [Test, Category("Generic")]
        public void ForEachOnRange_InputListNull_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT

            // ASSERT
            Assert.Throws<NullReferenceException>(() => list.ForEachOnRange(x => test = x));
        }

        [Test, Category("Generic")]
        public void ForEachOnRange_NullAggregateFunction_Throws()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentNullException>(() => list.ForEachOnRange(null));
        }

        [Test, Category("Generic")]
        public void ForEachOnRange_12345ListElement16Count_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.ForEachOnRange(x => test = x, 0, 6));
        }

        [Test, Category("Generic")]
        public void ForEachOnRange_NegativeSize_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.ForEachOnRange(x => test = x, 0, -3));
        }

        [Test, Category("Logic")]
        public void ForEachOnRange_IntEmptyList_Throws()
        {
            // ARRANGE
            List<int> list = new List<int>();

            Assert.Throws<ArgumentNullException>(() => list.ForEachOnRange(x => test = x));
        }

        [Test, Category("Logic")]
        public void ForEachOnRange_IntList1SaveValue_AddsEachElement()
        {
            // ARRANGE
            List<int> list = new List<int> { 1 };
            List<int> list2 = new List<int>();

            // ACT
            bool isEqual = true;
            list.ForEachOnRange(x => list2.Add(x));
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != list2[i])
                {
                    isEqual = false;
                }
            }

            // ASSERT
            Assert.That(isEqual);
        }

        [Test, Category("Logic")]
        public void ForEachOnRange_IntList12345SaveValue_AddsEachElement()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            List<int> list2 = new List<int>();

            // ACT
            bool isEqual = true;
            list.ForEachOnRange(x => list2.Add(x));
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != list2[i])
                {
                    isEqual = false;
                }
            }

            // ASSERT
            Assert.That(isEqual);
        }
    }
}