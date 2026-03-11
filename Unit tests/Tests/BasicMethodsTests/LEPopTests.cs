namespace Unit_tests.Tests
{
    using ListExtensions;
    using NUnit.Framework.Interfaces;

    public class LEPopTests
    {
        [Test, Category("Generic")]
        public void Pop_NullList_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT

            // ASSERT
            Assert.Throws<NullReferenceException>(() => list.Pop());
        }

        [Test, Category("Generic")]
        public void Pop_EmptyList_Throws()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT

            // ASSERT
            Assert.Throws<InvalidOperationException>(() => list.Pop());
        }

        [Test, Category("Logic")]
        public void Pop_IntListWith1_Returns1()
        {
            // ARRANGE
            List<int> list = new List<int>{ 1 };

            // ACT
            int item = list.Pop();

            // ASSERT
            Assert.That(item, Is.EqualTo(1));
        }

        [Test, Category("Logic")]
        public void Pop_IntListWith1_Count0()
        {
            // ARRANGE
            List<int> list = new List<int>{ 1 };

            // ACT
            list.Pop();

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test, Category("Logic")]
        public void Pop_StringListWithA_Returns1()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            string item = list.Pop();

            // ASSERT
            Assert.That(item, Is.EqualTo("A"));
        }

        [Test, Category("Logic")]
        public void Pop_StringListWithA_Count0()
        {
            // ARRANGE
            List<string> list = new List<string> { "A" };

            // ACT
            list.Pop();

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test, Category("Logic")]
        public void Pop_IntList12345_Returns5()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            int item = list.Pop();

            // ASSERT
            Assert.That(item, Is.EqualTo(5));
        }

        [Test, Category("Logic")]
        public void Pop_IntList12345_Count4()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            // ACT
            list.Pop();

            // ASSERT
            Assert.That(list.Count, Is.EqualTo(4));
        }
    }
}