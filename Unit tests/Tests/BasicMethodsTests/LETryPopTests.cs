namespace Unit_tests.Tests
{
    using FastListExtensions;

    public class LETryPopTests
    {
        [Test, Category("Generic")]
        public void TryPop_NullList_ReturnsFalse()
        {
            // ARRANGE
            List<int> list = null;

            // ACT
            int item = 0;

            // ASSERT
            Assert.That(list.TryPop(out item), Is.EqualTo(false));
        }

        [Test, Category("Generic")]
        public void TryPop_EmptyList_ReturnsFalse()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT
            int item = 0;

            // ASSERT
            Assert.That(list.TryPop(out item), Is.EqualTo(false));
        }

        [Test, Category("Logic")]
        public void TryPop_IntListWith1_ReturnsTrue()
        {
            // ARRANGE
            List<int> list = new List<int> { 1 };

            // ACT
            int item = 0;

            // ASSERT
            Assert.That(list.TryPop(out item), Is.EqualTo(true));
        }

        [Test, Category("Logic")]
        public void TryPop_IntListWith1_Extracts1()
        {
            // ARRANGE
            List<int> list = new List<int>{ 1 };

            // ACT
            int item = 0;
            list.TryPop(out item);

            // ASSERT
            Assert.That(item, Is.EqualTo(1));
        }

        [Test, Category("Logic")]
        public void TryPop_IntListWith1_Count0()
        {
            // ARRANGE
            List<int> list = new List<int>{ 1 };

            // ACT
            int item = 0;
            list.TryPop(out item);

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test, Category("Logic")]
        public void TryPop_StringListWithA_ReturnsTrue()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            string item = string.Empty;

            // ASSERT
            Assert.That(list.TryPop(out item), Is.EqualTo(true));
        }

        [Test, Category("Logic")]
        public void TryPop_StringListWithA_ExtractsA()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            string item = string.Empty;
            list.TryPop(out item);

            // ASSERT
            Assert.That(item, Is.EqualTo("A"));
        }

        [Test, Category("Logic")]
        public void TryPop_StringListWithA_Count0()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            string item = string.Empty;
            list.TryPop(out item);

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test, Category("Logic")]
        public void TryPop_IntList12345_ReturnsTrue()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int item = 0;

            // ASSERT
            Assert.That(list.TryPop(out item), Is.EqualTo(true));
        }

        [Test, Category("Logic")]
        public void TryPop_IntList12345_Returns5()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int item = 0;
            list.TryPop(out item);

            // ASSERT
            Assert.That(item, Is.EqualTo(5));
        }

        [Test, Category("Logic")]
        public void TryPop_IntList12345_Count4()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int item = 0;
            list.TryPop(out item);

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(4));
        }
    }
}