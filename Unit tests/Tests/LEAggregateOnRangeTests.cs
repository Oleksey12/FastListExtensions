namespace Unit_tests.Tests
{
    using FastListExtensions;

    public class LEAggregateOnRangeTests
    {
        [Test, Category("Generic")]
        public void AggregateOnRange_InputListNull_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT

            // ASSERT
            Assert.Throws<ArgumentNullException>(() => list.AggregateOnRange((x, y) => x + y, 0));
        }

        [Test, Category("Generic")]
        public void AggregateOnRange_NullAggregateFunction_Throws()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentNullException>(() => list.AggregateOnRange(null, 0f));
        }

        [Test, Category("Generic")]
        public void AggregateOnRange_12345ListElement16Count_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AggregateOnRange((x, y) => x + y, 0, 0, 6));
        }

        [Test, Category("Generic")]
        public void AggregateOnRange_NegativeSize_Throws()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT

            // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => list.AggregateOnRange((x, y) => x + y, 0, 0, -3));
        }

        [Test, Category("Generic")]
        public void AggregateOnRange_IntEmptyListStartValue1_Throws()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentNullException>(() => list.AggregateOnRange((x, y) => x + y, 0));
        }

        [Test, Category("Logic")]
        public void AggregateOnRange_IntList12345Sum_Returns15()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int result = list.AggregateOnRange((x, y) => x + y, 0);

            // ASSERT
            Assert.That(result, Is.EqualTo(15));
        }

        [Test, Category("Logic")]
        public void AggregateOnRange_IntList12345SumStartValue5_Returns20()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int result = list.AggregateOnRange((x, y) => x + y, 5);

            // ASSERT
            Assert.That(result, Is.EqualTo(20));
        }

        [Test, Category("Logic")]
        public void AggregateOnRange_StringListLengthSum_Returns9()
        {
            // ARRANGE
            List<string> list = new List<string> {"aaa", "bbb", "ccc" };

            // ACT
            int result = list.AggregateOnRange((x, y) => x + y.Length, 0);

            // ASSERT
            Assert.That(result, Is.EqualTo(9));
        }

    }
}