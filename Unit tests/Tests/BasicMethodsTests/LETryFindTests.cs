namespace Unit_tests.Tests
{
    using FastListExtensions;

    public class LETryFindTests
    {
        [Test, Category("Generic")]
        public void TryFind_NullList_ReturnsFalse()
        {
            // ARRANGE
            List<int> list = null;

            // ACT
            int result = 0;

            // ASSERT
            Assert.That(list.TryFind(x => x == 1, out result), Is.EqualTo(false));
        }

        [Test, Category("Generic")]
        public void TryFind_IntListNullRegularExpression_ReturnsFalse()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT
            int result = 0;

            // ASSERT
            Assert.That(list.TryFind(null, out result), Is.EqualTo(false));
        }

        [Test, Category("Generic")]
        public void TryFind_EmptyIntList_ReturnsFalse()
        {
            // ARRANGE
            List<int> list = new List<int>();
            // ACT
            int result = 2;

           // ASSERT
           Assert.That(list.TryFind(x => x == 1, out result), Is.EqualTo(false));
        }

        [Test, Category("Logic")]
        public void TryFind_IntListWith1_Find1ReturnsTrue()
        {
            // ARRANGE
            List<int> list = new List<int> { 1 };

            // ACT
            int result = 0;


            // ASSERT
            Assert.That(list.TryFind(x => x == 1, out result), Is.EqualTo(true));
        }

        [Test, Category("Logic")]
        public void TryFind_IntListWith1_Find1Extracts1()
        {
            // ARRANGE
            List<int> list = new List<int> { 1 };

            // ACT
            int result = 0;
            list.TryFind(x => x == 1, out result);

            // ASSERT
            Assert.That(result, Is.EqualTo(1));
        }

        [Test, Category("Logic")]
        public void TryFind_IntListWith1_NotChanged()
        {
            // ARRANGE
            List<int> list = new List<int> { 1 };
            List<int> copy = new List<int>(list);
            
            // ACT
            int result = 0;
            list.TryFind(x => x == 1, out result);
            bool notChanged = true;
            for (int i = 0; i < copy.Count; i++)
            {
                if (list[i] != copy[i])
                {
                    notChanged = false;
                }
            }

            // ASSERT
            Assert.That(notChanged);
        }

        [Test, Category("Logic")]
        public void TryFind_StringListABCFindC_NotChanged()
        {
            // ARRANGE
            List<string> list = new List<string> { "A", "B", "C" };
            List<string> copy = new List<string>(list);

            // ACT
            string result = "";
            list.TryFind(x => x == "C", out result);
            bool notChanged = true;
            for (int i = 0; i < copy.Count; i++)
            {
                if (list[i] != copy[i])
                {
                    notChanged = false;
                }
            }

            // ASSERT
            Assert.That(notChanged);
        }

        [Test, Category("Logic")]
        public void TryFind_StringListABCFindC_ReturnsTrue()
        {
            // ARRANGE
            List<string> list = new List<string> { "A", "B", "C" };

            // ACT
            string result = "";

            // ASSERT
            Assert.That(list.TryFind(x => x == "C", out result), Is.EqualTo(true));
        }

        [Test, Category("Logic")]
        public void TryFind_StringListABCFindC_ExtractsC()
        {
            // ARRANGE
            List<string> list = new List<string> { "A", "B", "C" };

            // ACT
            string result = "";
            list.TryFind(x => x == "C", out result);

            // ASSERT
            Assert.That(result, Is.EqualTo("C"));
        }
    }
}