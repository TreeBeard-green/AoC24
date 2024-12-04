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

            for (int i = 3; i < yMax - 3; i++) 
            {
                for (int j = 3; j < xMax - 3; j++)
                {
                    if (input[i][j] == 'X')
                    {
                        sum += CountXMAS(input, j, i);
                    }
                }
            }
            return sum;
        }
        
        static int Part2(string[] input)
        {
            int sum = 0;
            int xMax = input[0].Length, yMax = input.Length;

            for (int i = 3; i < yMax - 3; i++)
            {
                for (int j = 3; j < xMax - 3; j++)
                {
                    if (input[i][j] == 'A')
                    {
                        if (CheckX_MAS(input, j, i))
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
                    if (input[y + j][x + i] == 'M')
                    {
                        if (input[y + (j * 2)][x + (i * 2)] == 'A' && input[y + (j * 3)][x + (i * 3)] == 'S')
                        {
                            count++;
                        }
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
            string[] lines = new string[input.Length + 6];
            string filler = string.Empty;
            for (int i = 0; i < input[0].Length + 6; i++)
            {
                filler += '.';
            }
            for (int i = 0; i < input.Length; i++)
            {
                lines[i + 3] = "..." + input[i] + "...";
            }
            for (int i = 0; i < 3; i++)
            {
                lines[i] = filler;
                lines[i + input.Length + 3] = filler;
            }
            return lines;
        }
    }
}
