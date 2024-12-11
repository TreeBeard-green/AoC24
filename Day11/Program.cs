using System.Runtime.CompilerServices;
using Utility;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            Console.WriteLine(Part1(input[0], 25));

            Console.WriteLine(Part1(input[0], 75));
        }

        static long Part1(string input, int blinks)
        {
            long total = 0;
            var stones = ParseInput(input);
            Dictionary<long, long> memory = new Dictionary<long, long>();

            foreach (long stone in stones)
            {
                total += HowManyStones(stone, blinks, memory);
            }

            return total;
        }

        static long HowManyStones(long stone, int blinks, Dictionary<long, long> memory)
        {
            // No blinks = stone isnt gonna change = only 1 stone
            if (blinks == 0)
            {
                return 1;
            }

            // Check if we had same stone / blinks combo /// if exists we know how many stones it makes
            long key = (stone * 100 + blinks) * -1;
            if (memory.TryGetValue(key, out long value))
            {
                return value;
            }

            // Find new stone(s)
            var newStones = Blink(stone);

            // vvv Go next until no more blinks || we have combo in memory vvv
            long thisMany = 0;
            foreach (long newStone in newStones)
            {
                thisMany += HowManyStones(newStone, blinks - 1, memory);
            }

            // Save how many stones current stone/blinks combo makes
            memory.Add(key, thisMany);

            return thisMany;
        }

        static long[] Blink(long stone)
        {
            if (stone == 0)
            {
                return new[] {1L};
            }
            else if (EvenDigits(stone, out long n1, out long n2))
            {
                return new[] { n1, n2 };
            }
            else
            {
                return new[] { stone * 2024 };
            }
        }

        static List<long> ParseInput(string input)
        {
            string[] strings = input.Split(' ');
            List<long> stones = new List<long>();
            foreach (string s in strings)
            {
                stones.Add(long.Parse(s));
            }
            return stones;
        }

        static bool EvenDigits(long number, out long n1, out long n2)
        {
            int digits = 0;
            long copy = number;
            while (number > 0)
            {
                digits++;
                number /= 10;
            }
            n1 = (long)(copy / Math.Pow(10, digits / 2));
            n2 = (long)(copy % Math.Pow(10, digits / 2));
            if (digits % 2 == 0)
            {
                return true;
            }
            return false;
        }
    }
}
