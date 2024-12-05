using System.Numerics;
using Utility;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = Advent.GetInput(typeof(Program).Namespace);

            string[] input = PadArray(lines);

            Console.WriteLine(Part1(input));

            Console.WriteLine(Part2(input));
        }

        static int Part1(string[] input)
        {
            int sum = 0;
            int xMax = input[0].Length, yMax = input.Length;

            for (int y = 1; y < yMax - 1; y++) 
            {
                for (int x = 1; x < xMax - 1; x++)
                {
                    if (input[y][x] == 'X')
                    {
                        sum += CountXMAS(input, x, y);
                    }
                }
            }
            return sum;
        }
        
        static int Part2(string[] input)
        {
            int sum = 0;
            int xMax = input[0].Length, yMax = input.Length;

            for (int y = 1; y < yMax - 1; y++)
            {
                for (int x = 1; x < xMax - 1; x++)
                {
                    if (input[y][x] == 'A')
                    {
                        if (CheckX_MAS(input, x, y))
                        {
                            sum++;
                        }
                    }
                }
            }
            return sum;
        }

        static int CountXMAS(string[] input, int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    // Checking entire line 
                    if (input[y + j][x + i] == 'M' && input[y + (j * 2)][x + (i * 2)] == 'A' && input[y + (j * 3)][x + (i * 3)] == 'S')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        static bool CheckX_MAS(string[] input, int x, int y)
        {
            char upLeft = input[y - 1][x - 1];
            char upRight = input[y - 1][x + 1];
            char downLeft = input[y + 1][x - 1];
            char downRight = input[y + 1][x + 1];

            char[] letters = { 'M', 'S' };
            
            if (letters.Contains(upLeft) && letters.Contains(upRight) && letters.Contains(downLeft) && letters.Contains(downRight))
            {
                if ((upLeft != downRight) && (upRight != downLeft))
                {
                    return true;
                }
            }
            return false;
        }

        static string[] PadArray(string[] input)
        {
            // Padding array (lazy to make proper bound checks, could also try/catch instead)
            string[] lines = new string[input.Length + 2];
            string filler = string.Empty;

            for (int i = 0; i < input[0].Length + 2; i++)
            {
                filler += '.';
            }

            for (int i = 0; i < input.Length; i++)
            {
                lines[i + 1] = '.' + input[i] + '.';
            }

            lines[0] = filler;
            lines[input.Length + 1] = filler;
            return lines;
        }
    }
}
