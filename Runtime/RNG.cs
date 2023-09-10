using System;

namespace PixelWizards.Shared.Extensions
{
    public enum InclusionOptions
    {
        /// <summary>
        /// Includes both the lower and the upper bound
        /// </summary>
        Both,
        /// <summary>
        /// Only includes the lower bound
        /// </summary>
        Lower,
        /// <summary>
        /// Only includes the upper bound
        /// </summary>
        Upper,
        /// <summary>
        /// Both bounds are excluded
        /// </summary>
        None
    }

    public static class RNG
    {
        private static Random _rng;
        private static int _seed;
        private static bool _initialized;

        /// <summary>
        /// Initializes the random number generator with a random seed
        /// </summary>
        public static void Initialize()
        {
            _seed = Environment.TickCount;
            _rng = new Random(_seed);
            _initialized = true;
        }

        /// <summary>
        /// Initializes the random number generator with a given seed
        /// </summary>
        /// <param name="seed"></param>
        public static void Initialize(int seed)
        {
            _rng = new Random(seed);
            _seed = seed;
            _initialized = true;
        }

        public static int Seed { get { return _seed; } }

        /// <summary>
        /// Returns a random float between 0 and 1
        /// </summary>
        public static float RandFloat()
        {
            return (float)RandDouble();
        }

        /// <summary>
        /// Returns a random float between 0 and max
        /// </summary>
        /// <param name="max">The upper bound for the random value</param>
        public static float RandFloat(float max)
        {
            return (float)RandDouble(0, max);
        }

        /// <summary>
        /// Returns a random float between min and max, both included
        /// </summary>
        /// <param name="min">The lower bound for the random value</param>
        /// <param name="max">The upper bound for the random value</param>
        public static float RandFloat(float min, float max)
        {
            return (float)RandDouble(min, max);
        }

        /// <summary>
        /// Returns a random double between 0 and 1
        /// </summary>
        public static double RandDouble()
        {
            if (!_initialized)
                Initialize();
            return _rng.NextDouble();
        }

        /// <summary>
        /// Returns a random double between 0 and max
        /// </summary>
        /// <param name="max">The upper bound for the random value</param>
        public static double RandDouble(double max)
        {
            return RandDouble() * max;
        }

        /// <summary>
        /// Returns a random double between min and max, both included
        /// </summary>
        /// <param name="min">The lower bound for the random value</param>
        /// <param name="max">The upper bound for the random value</param>
        public static double RandDouble(double min, double max)
        {
            if (min < max)
                return (min + (max - min) * RandDouble());
            else if (min == max)
                return min;
            else
                return RandDouble(max, min);
        }

        /// <summary>
        /// Returns a random integer between 0 and int.MaxValue (both included)
        /// </summary>
        public static int RandInt()
        {
            return RandInt(0, int.MaxValue, InclusionOptions.Both);
        }

        /// <summary>
        /// Returns a random integer between 0 and max. Max is excluded by default
        /// </summary>
        /// <param name="max">The upper bound for the random value</param>
        /// <param name="option">Determines which bounds are included</param>
        /// <returns></returns>
        public static int RandInt(int max, InclusionOptions option = InclusionOptions.Lower)
        {

            return RandInt(0, max, option);
        }

        /// <summary>
        /// Returns a random int between min and max. Both bounds are included by default
        /// </summary>
        public static int RandInt(int min, int max, InclusionOptions option = InclusionOptions.Both)
        {
            if (min < max)
            {
                //Handle int overflow
                if (max == int.MaxValue)
                    if (option == InclusionOptions.Upper)
                        option = InclusionOptions.None;
                    else if (option == InclusionOptions.Both)
                        option = InclusionOptions.Lower;
                if (min == int.MinValue)
                    if (option == InclusionOptions.Lower)
                        option = InclusionOptions.None;
                    else if (option == InclusionOptions.Both)
                        option = InclusionOptions.Upper;

                switch (option)
                {
                    case InclusionOptions.Both:
                        return RandIntInRange(min, max + 1);
                    case InclusionOptions.Lower:
                        return RandIntInRange(min, max);
                    case InclusionOptions.Upper:
                        return RandIntInRange(min + 1, max + 1);
                    case InclusionOptions.None:
                        return RandIntInRange(min + 1, max);
                    default:
                        throw new ArgumentException("Invalid InclusionOption");
                }
            }
            else if (min == max)
                return min;
            else
                return RandInt(max, min, option);
        }

        /// <summary>
        /// Returns a random long between 0 and long.MaxValue
        /// </summary>
        public static long RandLong()
        {
            return RandLong(0, long.MaxValue, InclusionOptions.Both);
        }

        /// <summary>
        /// Returns a random long between 0 and max. Max is excluded by default
        /// </summary>
        /// <param name="max">The upper bound for the random value</param>
        /// <param name="option">Determines which bounds are included</param>
        /// <returns></returns>
        public static long RandLong(long max, InclusionOptions option = InclusionOptions.Lower)
        {
            return RandLong(0, max, option);
        }

        /// <summary>
        /// Returns a random long between min and max. Both bounds are included by default
        /// </summary>
        public static long RandLong(long min, long max, InclusionOptions option = InclusionOptions.Both)
        {
            if (min < max)
            {
                //Handle long overflow
                if (max == long.MaxValue)
                    if (option == InclusionOptions.Upper)
                        option = InclusionOptions.None;
                    else if (option == InclusionOptions.Both)
                        option = InclusionOptions.Lower;
                if (min == long.MinValue)
                    if (option == InclusionOptions.Lower)
                        option = InclusionOptions.None;
                    else if (option == InclusionOptions.Both)
                        option = InclusionOptions.Upper;

                switch (option)
                {
                    case InclusionOptions.Both:
                        return RandLongInRange(min, max + 1);
                    case InclusionOptions.Lower:
                        return RandLongInRange(min, max);
                    case InclusionOptions.Upper:
                        return RandLongInRange(min + 1, max + 1);
                    case InclusionOptions.None:
                        return RandLongInRange(min + 1, max);
                    default:
                        throw new ArgumentException("Invalid InclusionOption");
                }
            }
            else if (min == max)
                return min;
            else
                return RandLong(max, min, option);
        }
        
        /// <summary>
        /// Returns true or false
        /// </summary>
        public static bool RandBool()
        {
            return RandDouble() < 0.5F;
        }

        /// <summary>
        /// Returns 1 or -1
        /// </summary>
        public static int RandSign()
        {
            return RandBool() ? 1 : -1;
        }

        /// <summary>
        /// Shuffles the elements of the given array
        /// </summary>
        public static void Shuffle<T>(params T[] elements)
        {
            //Fisher-Yates shuffle
            for (int i = 0; i < elements.Length; i++)
            {
                int j = RandInt(i, elements.Length, InclusionOptions.Lower);
                T temp = elements[j];
                elements[j] = elements[i];
                elements[i] = temp;
            }
        }

        private static int RandIntInRange(int lower, int upper)
        {
            if (!_initialized)
                Initialize();

            return _rng.Next(lower, upper);
        }

        private static long RandLongInRange(long lower, long upper)
        {
            if (!_initialized)
                Initialize();

            byte[] randBytes = new byte[8];
            _rng.NextBytes(randBytes);
            long r = BitConverter.ToInt64(randBytes, 0);
            return Math.Abs(r % (upper - lower)) + lower;
        }

    }
}