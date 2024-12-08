using Utility;

namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            var grid = ParseInput(input);
            var antennas = GetAllAntennas(grid);

            Console.WriteLine(Part1(grid, antennas));

            Console.WriteLine(Part2(grid, antennas));
        }

        static int Part1((char[,], int, int) grid, Dictionary<char, List<(int, int)>> antennas)
        {
            int sum = 0;
            foreach (var antenna in antennas)
            {
                if (antenna.Value.Count >= 2)
                {
                    for (int i = 0; i < antenna.Value.Count; i++)
                    {
                        for (int j = i + 1; j < antenna.Value.Count; j++)
                        {
                            PlaceNodes(grid, antenna.Value[i], antenna.Value[j], false);
                        }
                    }
                }
            }
            // j = x i = y
            for (int i = 0; i < grid.Item3; i++)
            {
                for (int j = 0; j < grid.Item2; j++)
                {
                    if (grid.Item1[j, i] == '#')
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }

        static int Part2((char[,], int, int) grid, Dictionary<char, List<(int, int)>> antennas)
        {
            int sum = 0;
            foreach (var antenna in antennas)
            {
                if (antenna.Value.Count > 1)
                {
                    for (int i = 0; i < antenna.Value.Count; i++)
                    {
                        for (int j = i + 1; j < antenna.Value.Count; j++)
                        {
                            PlaceNodes(grid, antenna.Value[i], antenna.Value[j], true);
                        }
                    }
                }
            }
            for (int i = 0; i < grid.Item3; i++)
            {
                for (int j = 0; j < grid.Item2; j++)
                {
                    if (grid.Item1[j, i] != '.')
                    {
                        if (grid.Item1[j, i] == '#')
                        {
                            sum++;
                        }
                        else
                        {
                            if (antennas[grid.Item1[j, i]].Count > 1)
                            {
                                sum++;
                            }
                        }
                    }
                }
            }
            return sum;
        }

        static void PlaceNodes((char[,], int, int) grid, (int , int) pos1, (int, int) pos2, bool part2)
        {
            int w = grid.Item2, h = grid.Item3;
            var dif = SubstractTuples(pos2, pos1);
            var np1 = SubstractTuples(pos1, dif);
            var np2 = AddTuples(pos2, dif);
            if (part2)
            {
                while (np1.Item1 >= 0 && np1.Item1 < w && np1.Item2 >= 0 && np1.Item2 < h)
                {
                    grid.Item1[np1.Item1, np1.Item2] = '#';
                    np1 = SubstractTuples(np1, dif);
                }
                while (np2.Item1 >= 0 && np2.Item1 < w && np2.Item2 >= 0 && np2.Item2 < h)
                {
                    grid.Item1[np2.Item1, np2.Item2] = '#';
                    np2 = AddTuples(np2, dif);
                }
            }
            else
            {
                if (np1.Item1 >= 0 && np1.Item1 < w && np1.Item2 >= 0 && np1.Item2 < h)
                {
                    grid.Item1[np1.Item1, np1.Item2] = '#';
                }
                if (np2.Item1 >= 0 && np2.Item1 < w && np2.Item2 >= 0 && np2.Item2 < h)
                {
                    grid.Item1[np2.Item1, np2.Item2] = '#';
                }
            }
        }
        static (int, int) AddTuples((int, int) t1, (int, int) t2)
        {
            return (t1.Item1 + t2.Item1, t1.Item2 + t2.Item2);
        }
        static (int, int) SubstractTuples((int, int) t1, (int, int) t2)
        {
            return (t1.Item1 - t2.Item1, t1.Item2 - t2.Item2);
        }
        static (char[,], int, int) ParseInput(string[] input)
        {
            int w = input[0].Length, h = input.Length;
            char[,] output = new char[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    output[j, i] = input[i][j];
                }
            }
            return (output, w, h);
        }

        static Dictionary<char, List<(int, int)>> GetAllAntennas((char[,], int, int) grid)
        {
            int w = grid.Item2, h = grid.Item3;
            char[,] chars = grid.Item1;

            Dictionary<char, List<(int, int)>> output = new Dictionary<char, List<(int, int)>>();

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (chars[j, i] != '.')
                    {
                        char c = chars[j, i];
                        if (!output.TryGetValue(c, out List<(int, int)>? value))
                        {
                            value = new List<(int, int)>();
                            output[c] = value;
                        }
                        value.Add((j, i));
                    }
                }
            }
            return output;
        }
    }
}
