using Utility;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            input = PadArray(input);

            var grid = ParseInput(input);

            Console.WriteLine(Part1(grid));

            Console.WriteLine(Part2(grid));
        }

        static int Part1((int[,], int, int) grid)
        {
            int sum = 0, w = grid.Item2, h = grid.Item3;
            List<(int, int)> list = [];
            for (int i = 1; i < h - 1; i++)
            {
                for (int j = 1; j < w - 1; j++)
                {
                    if (grid.Item1[j, i] == 0)
                    {
                        sum += LookAround(0, (j, i), grid.Item1, list, false);
                        list.Clear();
                    }
                }
            }

            return sum;
        }
        
        static int Part2((int[,], int, int) grid)
        {
            int sum = 0, w = grid.Item2, h = grid.Item3;
            List<(int, int)> list = [];
            for (int i = 1; i < h - 1; i++)
            {
                for (int j = 1; j < w - 1; j++)
                {
                    if (grid.Item1[j, i] == 0)
                    {
                        sum += LookAround(0, (j, i), grid.Item1, list, true);
                        list.Clear();
                    }
                }
            }

            return sum;
        }
        static int LookAround(int c, (int, int) coords, int[,] grid, List<(int, int)> list, bool part2)
        {
            (int, int)[] directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };

            for (int i = 0; i < 4; i++)
            {
                coords = AddTuples(coords, directions[i]);
                if (grid[coords.Item1, coords.Item2] == c + 1)
                {
                    if (grid[coords.Item1, coords.Item2] == 9)
                    {
                        if (!part2)
                        {
                            if (!list.Contains(coords))
                            {
                                list.Add(coords);
                            }
                        }
                        else
                        {
                            list.Add(coords);
                        }
                    }
                    else
                    {
                        LookAround(c + 1, coords, grid, list, part2);
                    }
                }
                coords = SubstractTuples(coords, directions[i]);
            }
            return list.Count;
        }

        static (int, int) AddTuples((int, int) t1, (int, int) t2)
        {
            return (t1.Item1 + t2.Item1, t1.Item2 + t2.Item2);
        }
        static (int, int) SubstractTuples((int, int) t1, (int, int) t2)
        {
            return (t1.Item1 - t2.Item1, t1.Item2 - t2.Item2);
        }

        static (int[,], int, int) ParseInput(string[] input)
        {
            int w = input[0].Length, h = input.Length;
            int[,] output = new int[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    output[j, i] = input[i][j] - '0';
                }
            }
            return (output, w, h);
        }

        static string[] PadArray(string[] input)
        {
            string[] lines = new string[input.Length + 2];
            string filler = string.Empty;

            for (int i = 0; i < input[0].Length + 2; i++)
            {
                filler += '0';
            }

            for (int i = 0; i < input.Length; i++)
            {
                lines[i + 1] = '0' + input[i] + '0';
            }

            lines[0] = filler;
            lines[input.Length + 1] = filler;
            return lines;
        }
    }
}
