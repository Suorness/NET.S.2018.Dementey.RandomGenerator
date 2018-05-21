namespace Generator
{
    using System;
    using System.Linq;

    /// <summary>
    /// A random <see cref="Array"/> generator.
    /// </summary>
    public class ArrayGenerator
    {
        private const double ElementsSum = 1d;
        private Random _random;

        public ArrayGenerator()
        {
            _random = new Random();
        }

        /// <summary>
        /// Generates a random array.
        /// </summary>
        /// <param name="count"> Count elements in array.</param>
        /// <param name="maxValue">Maximum value in array</param>
        /// <returns>
        /// Array with random value.
        /// </returns>
        public double[] GenerateRandomArray(int count, double maxValue)
        {
            VerifyInputValue(count, maxValue, ElementsSum);

            var generatedArray = new double[count];

            generatedArray[0] = maxValue;
            double remainder = ElementsSum - generatedArray[0];

            for (int i = 1; i < count; i++)
            {
                double generationMax = remainder > maxValue ? maxValue : remainder;
                generationMax = generationMax / (count - i);

                generatedArray[i] = GetRandomDouble(0, generationMax);
                remainder -= generatedArray[i];
            }

            while (remainder != 0d)
            {
                int index = GetRandomInt(1, count);
                double differenceToMax = maxValue - generatedArray[index];

                if (differenceToMax > remainder)
                {
                    generatedArray[index] += remainder;
                    remainder = 0d;
                }
                else
                {
                    double randomValue = GetRandomDouble(0, differenceToMax);
                    generatedArray[index] += randomValue;
                    remainder -= randomValue;
                }
            }

            return MixingArray(generatedArray);
        }

        private double[] MixingArray(double[] array)
        {
            return array.OrderBy(r => _random.Next()).ToArray();
        }

        private void VerifyInputValue(int count, double maxValue, double sum)
        {
            if (count <= 0)
            {
                throw new ArgumentException(nameof(count));
            }

            if (maxValue >= sum)
            {
                throw new ArgumentException(
                    $"{nameof(maxValue)} must be less than {sum}", nameof(maxValue));
            }

            if (!PossibiliteToCreateArrayWithSum(count, maxValue, sum))
            {
                throw new ArgumentException($"It is impossible to create an array of " +
                    $"{count} elements with the sum of the elements {sum}" +
                    $" and the maximum element {maxValue}.");
            }
        }

        private double GetRandomDouble(double minimum, double maximum)
        {
            return (_random.NextDouble() * (maximum - minimum)) + minimum;
        }

        private int GetRandomInt(int minimum, int maximum)
        {
            return _random.Next(minimum, maximum);
        }

        private bool PossibiliteToCreateArrayWithSum(int count, double maxValue, double sum)
        {
            return count * maxValue > sum;
        }
    }
}
