using Utility;

namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            var grid = Advent.ConvertInputToGrid<char>(input);
            var antennas = GetAllAntennas(grid);

            Console.WriteLine(Part1(grid, antennas));

            Console.WriteLine(Part2(grid, antennas));
        }

        static int Part1(Grid<char> grid, Dictionary<char, List<Coordinates>> antennas)
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
            for (int i = 0; i < grid.height; i++)
            {
                for (int j = 0; j < grid.width; j++)
                {
                    if (grid.GetValue(j, i) == '#')
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }

        static int Part2(Grid<char> grid, Dictionary<char, List<Coordinates>> antennas)
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
            for (int i = 0; i < grid.height; i++)
            {
                for (int j = 0; j < grid.width; j++)
                {
                    if (grid.GetValue(j, i) != '.')
                    {
                        if (grid.GetValue(j, i) == '#')
                        {
                            sum++;
                        }
                        else
                        {
                            if (antennas[grid.GetValue(j, i)].Count > 1)
                            {
                                sum++;
                            }
                        }
                    }
                }
            }
            return sum;
        }

        static void PlaceNodes(Grid<char> grid, Coordinates pos1, Coordinates pos2, bool part2)
        {
            var dif = pos2 - pos1;
            var np1 = pos1 - dif;
            var np2 = pos2 + dif;
            if (part2)
            {
                while (np1.X >= 0 && np1.X < grid.width && np1.Y >= 0 && np1.Y < grid.height)
                {
                    grid.SetValue(np1, '#');
                    np1 = np1 - dif;
                }
                while (np2.X >= 0 && np2.X < grid.width && np2.Y >= 0 && np2.Y < grid.height)
                {
                    grid.SetValue(np2, '#');
                    np2 = np2 + dif;
                }
            }
            else
            {
                if (np1.X >= 0 && np1.X < grid.width && np1.Y >= 0 && np1.Y < grid.height)
                {
                    grid.SetValue(np1, '#');
                }
                if (np2.X >= 0 && np2.X < grid.width && np2.Y >= 0 && np2.Y < grid.height)
                {
                    grid.SetValue(np2, '#');
                }
            }
        }

        static Dictionary<char, List<Coordinates>> GetAllAntennas(Grid<char> grid)
        {
            var output = new Dictionary<char, List<Coordinates>>();

            for (int y = 0; y < grid.height; y++)
            {
                for (int x = 0; x < grid.width; x++)
                {
                    if (grid.GetValue(x, y) != '.')
                    {
                        char c = grid.GetValue(x, y);
                        if (!output.TryGetValue(c, out List<Coordinates>? value))
                        {
                            value = new List<Coordinates>();
                            output[c] = value;
                        }
                        value.Add(new (x, y));
                    }
                }
            }
            return output;
        }
    }
}
