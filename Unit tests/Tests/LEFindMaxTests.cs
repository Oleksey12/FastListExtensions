namespace Unit_tests.Tests
{
    using LE;

    public class LEFindMaxTests
    {
        [Test]
        public void FindMax_InputNull_Throws()
        {
            // ARRANGE
            List<int> list = null;

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => list.FindMax());
        }

        [Test]
        public void FindMax_OneElementListWith2_Returns2()
        {
            // ARRANGE
            List<int> list = new List<int> { 2 };

            // ACT
            int result = list.FindMax();

            // ASSERT
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void FindMax_12345List_Returns5()
        {
            // ARRANGE
            List<int> list = new List<int> {1, 2, 3, 4, 5};

            // ACT
            int result = list.FindMax();

            // ASSERT
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void FindMax_12345List_NotChanged()
        {
            // ARRANGE
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            List<int> copy = new List<int>(list);

            // ACT
            int result = list.FindMax();

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

        [Test]
        public void FindMax_StringList_ReturnsLongest()
        {
            // ARRANGE
            List<string> list = new List<string> { "a", "ccc", "bb" };

            // ACT
            string result = list.FindMax();

            // ASSERT
            Assert.That(result, Is.EqualTo("ccc"));
        }

        [Test]
        public void FindMax_StringList_ReturnsBiggest()
        {
            // ARRANGE
            List<string> list = new List<string> { "aaa", "ccc", "bbb" };

            // ACT
            string result = list.FindMax();

            // ASSERT
            Assert.That(result, Is.EqualTo("ccc"));
        }
    }
}