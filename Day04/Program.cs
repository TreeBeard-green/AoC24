using System.Numerics;
using Utility;

namespace Day04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            var grid = Advent.ConvertInputToGrid<char>(input, 1, '.');

            Console.WriteLine(Part1(grid));

            Console.WriteLine(Part2(grid));
        }

        static int Part1(Grid<char> grid)
        {
            int sum = 0;

            for (int y = 1; y < grid.height - 1; y++) 
            {
                for (int x = 1; x < grid.width - 1; x++)
                {
                    if (grid.GetValue(x, y) == 'X')
                    {
                        sum += CountXMAS(grid, x, y);
                    }
                }
            }
            return sum;
        }
        
        static int Part2(Grid<char> grid)
        {
            int sum = 0;

            for (int y = 1; y < grid.height - 1; y++)
            {
                for (int x = 1; x < grid.width - 1; x++)
                {
                    if (grid.GetValue(x, y) == 'A')
                    {
                        if (CheckX_MAS(grid, new Coordinates(x, y)))
                        {
                            sum++;
                        }
                    }
                }
            }
            return sum;
        }

        static int CountXMAS(Grid<char> grid, int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    // Checking entire line 
                    if (grid.GetValue(x + j, y + i) == 'M' && grid.GetValue(x + (j * 2), y + (i * 2)) == 'A' && grid.GetValue(x + (j * 3), y + (i * 3)) == 'S')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        static bool CheckX_MAS(Grid<char> grid, Coordinates coords)
        {
            char upLeft = grid.GetValue(coords + Coordinates.UpLeft);
            char upRight = grid.GetValue(coords + Coordinates.UpRight);
            char downLeft = grid.GetValue(coords + Coordinates.DownLeft);
            char downRight = grid.GetValue(coords + Coordinates.DownRight);

            char[] letters = ['M', 'S'];
            
            if (letters.Contains(upLeft) && letters.Contains(upRight) && letters.Contains(downLeft) && letters.Contains(downRight))
            {
                if ((upLeft != downRight) && (upRight != downLeft))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
