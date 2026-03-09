namespace Unit_tests.Tests
{
    using ListExtensions;

    public class LETryPopFirstTests
    {
        [Test, Category("Generic")]
        public void TryPopFirst_NullList_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT
            int item = 0;

            // ASSERT
            Assert.Throws<NullReferenceException>(() => list.TryPopFirst(out item));
        }

        [Test, Category("Generic")]
        public void TryPopFirst_EmptyList_ReturnsFalse()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT
            int item = 0;

            // ASSERT
            Assert.That(list.TryPopFirst(out item), Is.EqualTo(false));
        }

        [Test, Category("Logic")]
        public void TryPopFirst_IntListWith1_ReturnsTrue()
        {
            // ARRANGE
            List<int> list = new List<int> { 1 };

            // ACT
            int item = 0;

            // ASSERT
            Assert.That(list.TryPopFirst(out item), Is.EqualTo(true));
        }

        [Test, Category("Logic")]
        public void TryPopFirst_IntListWith1_Extracts1()
        {
            // ARRANGE
            List<int> list = new List<int>{ 1 };

            // ACT
            int item = 0;
            list.TryPopFirst(out item);

            // ASSERT
            Assert.That(item, Is.EqualTo(1));
        }

        [Test, Category("Logic")]
        public void TryPopFirst_IntListWith1_Count0()
        {
            // ARRANGE
            List<int> list = new List<int>{ 1 };

            // ACT
            int item = 0;
            list.TryPopFirst(out item);

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test, Category("Logic")]
        public void TryPopFirst_StringListWithA_ReturnsTrue()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            string item = string.Empty;

            // ASSERT
            Assert.That(list.TryPopFirst(out item), Is.EqualTo(true));
        }

        [Test, Category("Logic")]
        public void TryPopFirst_StringListWithA_ExtractsA()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            string item = string.Empty;
            list.TryPopFirst(out item);

            // ASSERT
            Assert.That(item, Is.EqualTo("A"));
        }

        [Test, Category("Logic")]
        public void TryPopFirst_StringListWithA_Count0()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            string item = string.Empty;
            list.TryPopFirst(out item);

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test, Category("Logic")]
        public void TryPopFirst_IntList12345_ReturnsTrue()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int item = 0;

            // ASSERT
            Assert.That(list.TryPopFirst(out item), Is.EqualTo(true));
        }

        [Test, Category("Logic")]
        public void TryPopFirst_IntList12345_Returns1()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int item = 0;
            list.TryPopFirst(out item);

            // ASSERT
            Assert.That(item, Is.EqualTo(1));
        }

        [Test, Category("Logic")]
        public void TryPopFirst_IntList12345_Count4()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int item = 0;
            list.TryPopFirst(out item);

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(4));
        }
    }
}