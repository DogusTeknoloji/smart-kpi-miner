using System;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks
{
    public static class RandomNumberGenerator
    {
        private readonly static Random _randomObj = new Random();
        /// <summary>
        /// This chosen numbers should not be prime number to improve decimal result rate. 
        /// In Example; The rule of the division by 6 means division by both of 2 and 3.
        /// </summary>
        private readonly static int[] _factor = new[] { 4, 6, 8, 9, 12, 15, 18, 20 };

        /// <summary>
        /// Gets a random integer between 0 and 100
        /// </summary>
        /// <returns></returns>
        public static int GetRandomInt()
        {
            int result = GetRandomInt(0, 100);
            return result;
        }
        /// <summary>
        /// Gets a random integer starts from 0
        /// </summary>
        /// <param name="max">Maximum value of random</param>
        /// <returns></returns>
        public static int GetRandomInt(int max)
        {
            int result = GetRandomInt(0, max);
            return result;
        }
        public static int GetRandomInt(int min, int max)
        {
            int result = _randomObj.Next(min, max);
            return result;
        }

        /// <summary>
        /// Gets a random double between 0 and 1
        /// </summary>
        /// <returns></returns>
        public static double GetRandomDouble()
        {
            double result = _randomObj.NextDouble();
            return result;
        }
        /// <summary>
        /// Gets a random double between starts from 0
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double GetRandomDouble(int max)
        {
            double result = GetRandomDouble(1, max);
            return result;
        }
        public static double GetRandomDouble(int min, int max)
        {
            int randomInt = 1 / _randomObj.Next(min, max);
            return randomInt;
        }
    }
}