namespace Unit_tests.Tests
{
    using ListExtensions;

    public class LETryConvertTests
    {
        [Test, Category("Generic")]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            // ARRANGE
            List<int> list = null;

            // ACT
            BaseInterface inst = null;
            InheritClass result = null;

            // ASSERT
            Assert.That(inst.TryConvert(out result), Is.EqualTo(false));
        }

        [Test, Category("Generic")]
        public void TryConvert_NotInheritClass_ReturnsFalse()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT
            BaseInterface inst = new InheritClass();
            RandomClass result = null;

            // ASSERT
            Assert.That(inst.TryConvert(out result), Is.EqualTo(false));
        }

        [Test, Category("Logic")]
        public void TryConvert_InheritClass_ReturnsTrue()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT
            BaseInterface inst = new InheritClass();
            InheritClass result = null;

            // ASSERT
            Assert.That(inst.TryConvert(out result), Is.EqualTo(true));
        }

        [Test, Category("Logic")]
        public void TryConvert_InheritClass_ConvertsSuccessfully()
        {
            // ARRANGE
            List<int> list = new List<int>();

            // ACT
            BaseInterface inst = new InheritClass();
            InheritClass result = null;
            inst.TryConvert(out result);

            // ASSERT
            Assert.That(result is not null);
        }
    }
}