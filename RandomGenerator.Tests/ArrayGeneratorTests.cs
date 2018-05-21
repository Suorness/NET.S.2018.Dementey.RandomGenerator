namespace RandomGenerator.Tests
{
    using System;
    using System.Linq;
    using Generator;
    using NUnit.Framework;

    [TestFixture]
    public class ArrayGeneratorTests
    {
        private const double Delta = 0.00001d;

        [TestCase(0, 0.2d)]
        [TestCase(-1, 0.2d)]
        [TestCase(-11, 0.2d)]
        public void GenereteRandomArray_NegativeCount_ThrowsArgumentException(int count, double maxValue)
        {
            var generator = new ArrayGenerator();
            Assert.Throws<ArgumentException>(() => generator.GenerateRandomArray(count, maxValue));
        }

        [TestCase(1, 1d)]
        [TestCase(1, 10d)]
        public void GenereteRandomArray_maxValueGreatOne_ThrowsArgumentException(int count, double maxValue)
        {
            var generator = new ArrayGenerator();
            Assert.Throws<ArgumentException>(() => generator.GenerateRandomArray(count, maxValue));
        }

        [TestCase(1, 0.5d)]
        [TestCase(9, 0.1d)]
        [TestCase(500, 0.001d)]
        public void GenereteRandomArray_InabilityMakeAount_ThrowsArgumentException(int count, double maxValue)
        {
            var generator = new ArrayGenerator();
            Assert.Throws<ArgumentException>(() => generator.GenerateRandomArray(count, maxValue));
        }

        [TestCase(10, 0.2d)]
        [TestCase(500, 0.01d)]
        [TestCase(1000, 0.2d)]
        [TestCase(99999, 0.1d)]
        [TestCase(2, 0.99d)]
        [TestCase(10, 0.99d)]
        [TestCase(100, 0.99d)]
        public void GenerateRandomArrayTests(int count, double maxValue)
        {
            var generator = new ArrayGenerator();
            var array = generator.GenerateRandomArray(count, maxValue);

            Assert.AreEqual(count, array.Length);
            Assert.That(array, Is.All.InRange(0d, maxValue));
            Assert.AreEqual(1d, array.Sum(), Delta);
        }
    }
}
