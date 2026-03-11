namespace Unit_tests.Tests
{
    using FastListExtensions;
    using NUnit.Framework.Interfaces;

    public class LEPopFirstTests
    {
        [Test, Category("Generic")]
        public void PopFirst_NullList_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT

            // ASSERT
            Assert.Throws<NullReferenceException>(() => list.PopFirst());
        }

        [Test, Category("Generic")]
        public void PopFirst_EmptyList_Throws()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT

            // ASSERT
            Assert.Throws<InvalidOperationException>(() => list.PopFirst());
        }

        [Test, Category("Logic")]
        public void PopFirst_IntListWith1_Returns1()
        {
            // ARRANGE
            List<int> list = new List<int>{ 1 };

            // ACT
            int item = list.PopFirst();

            // ASSERT
            Assert.That(item, Is.EqualTo(1));
        }

        [Test, Category("Logic")]
        public void PopFirst_IntListWith1_Count0()
        {
            // ARRANGE
            List<int> list = new List<int>{ 1 };

            // ACT
            list.PopFirst();

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test, Category("Logic")]
        public void PopFirst_StringListWithA_Returns1()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            string item = list.PopFirst();

            // ASSERT
            Assert.That(item, Is.EqualTo("A"));
        }

        [Test, Category("Logic")]
        public void PopFirst_StringListWithA_Count0()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            list.PopFirst();

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test, Category("Logic")]
        public void PopFirst_IntList12345_Returns1()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int item = list.PopFirst();

            // ASSERT
            Assert.That(item, Is.EqualTo(1));
        }

        [Test, Category("Logic")]
        public void PopFirst_IntList12345_Count4()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            list.PopFirst();

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(4));
        }
    }
}