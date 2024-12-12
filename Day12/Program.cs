using Utility;

namespace Day12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);
            int paddingSize = 1;

            var grid = Advent.ConvertInputToGrid<char>(input, paddingSize, '.');

            Console.WriteLine(Part1(grid, paddingSize));

            Console.WriteLine(Part2(grid, paddingSize));
        }

        static int Part1(Grid<char> grid, int pad)
        {
            int sum = 0, number = 0;
            var visitedCoords = new List<Coordinates>();
            for (int y = pad; y < grid.height - pad; y++)
            {
                for (int x = pad; x < grid.width - pad; x++)
                {
                    if (!visitedCoords.Contains(new(x, y)))
                    {
                        sum += RegionPerimeter(grid, new(x, y), visitedCoords) * (visitedCoords.Count - number);
                        number = visitedCoords.Count;
                    }
                }
            }

            return sum;
        }

        static int Part2(Grid<char> grid, int pad)
        {
            int sum = 0, number = 0, n2;
            var visitedCoords = new List<Coordinates>();
            for (int y = pad; y < grid.height - pad; y++)
            {
                for (int x = pad; x < grid.width - pad; x++)
                {
                    if (!visitedCoords.Contains(new(x, y)))
                    {
                        n2 = RegionSides(grid, new(x, y), visitedCoords);
                        sum += n2 * (visitedCoords.Count - number);
                        number = visitedCoords.Count;
                    }
                }
            }

            return sum;
        }

        static int RegionPerimeter(Grid<char> grid, Coordinates coords, List<Coordinates> visited)
        {
            if (visited.Contains(coords))
            {
                return 0;
            }
            else
            {
                visited.Add(coords);
            }
            int perimeter = 4;
            char c = grid.GetValue(coords);

            if (grid.GetValue(coords + Coordinates.Up) == c)
            {
                perimeter--;
                perimeter += RegionPerimeter(grid, coords + Coordinates.Up, visited);
            }

            if (grid.GetValue(coords + Coordinates.Right) == c)
            {
                perimeter--;
                perimeter += RegionPerimeter(grid, coords + Coordinates.Right, visited);
            }

            if (grid.GetValue(coords + Coordinates.Down) == c)
            {
                perimeter--;
                perimeter += RegionPerimeter(grid, coords + Coordinates.Down, visited);
            }

            if (grid.GetValue(coords + Coordinates.Left) == c)
            {
                perimeter--;
                perimeter += RegionPerimeter(grid, coords + Coordinates.Left, visited);
            }
            return perimeter;
        }

        static int RegionSides(Grid<char> grid, Coordinates coords, List<Coordinates> visited)
        {
            // Corners = sides <<< math voodoo
            if (visited.Contains(coords))
            {
                return 0;
            }
            else
            {
                visited.Add(coords);
            }
            int corners = 0;
            char c = grid.GetValue(coords);

            bool up = grid.GetValue(coords + Coordinates.Up) == c,
                down = grid.GetValue(coords + Coordinates.Down) == c,
                right = grid.GetValue(coords + Coordinates.Right) == c,
                left = grid.GetValue(coords + Coordinates.Left) == c;

            if (up && down && right && left)
            {
                if (grid.GetValue(coords + Coordinates.UpLeft) != c)
                {
                    corners++;
                }
                if (grid.GetValue(coords + Coordinates.UpRight) != c)
                {
                    corners++;
                }
                if (grid.GetValue(coords + Coordinates.DownLeft) != c)
                {
                    corners++;
                }
                if (grid.GetValue(coords + Coordinates.DownRight) != c)
                {
                    corners++;
                }
                corners += RegionSides(grid, coords + Coordinates.Up, visited);
                corners += RegionSides(grid, coords + Coordinates.Down, visited);
                corners += RegionSides(grid, coords + Coordinates.Left, visited);
                corners += RegionSides(grid, coords + Coordinates.Right, visited);
            }
            else if (up && down && left)
            {
                if (grid.GetValue(coords + Coordinates.UpLeft) != c)
                {
                    corners++;
                }
                if (grid.GetValue(coords + Coordinates.DownLeft) != c)
                {
                    corners++;
                }
                corners += RegionSides(grid, coords + Coordinates.Up, visited);
                corners += RegionSides(grid, coords + Coordinates.Down, visited);
                corners += RegionSides(grid, coords + Coordinates.Left, visited);
            }
            else if (up && down && right)
            {
                if (grid.GetValue(coords + Coordinates.UpRight) != c)
                {
                    corners++;
                }
                if (grid.GetValue(coords + Coordinates.DownRight) != c)
                {
                    corners++;
                }
                corners += RegionSides(grid, coords + Coordinates.Up, visited);
                corners += RegionSides(grid, coords + Coordinates.Down, visited);
                corners += RegionSides(grid, coords + Coordinates.Right, visited);
            }
            else if (up && left && right)
            {
                if (grid.GetValue(coords + Coordinates.UpLeft) != c)
                {
                    corners++;
                }
                if (grid.GetValue(coords + Coordinates.UpRight) != c)
                {
                    corners++;
                }
                corners += RegionSides(grid, coords + Coordinates.Up, visited);
                corners += RegionSides(grid, coords + Coordinates.Left, visited);
                corners += RegionSides(grid, coords + Coordinates.Right, visited);
            }
            else if (down && left && right)
            {
                if (grid.GetValue(coords + Coordinates.DownLeft) != c)
                {
                    corners++;
                }
                if (grid.GetValue(coords + Coordinates.DownRight) != c)
                {
                    corners++;
                }
                corners += RegionSides(grid, coords + Coordinates.Down, visited);
                corners += RegionSides(grid, coords + Coordinates.Left, visited);
                corners += RegionSides(grid, coords + Coordinates.Right, visited);
            }
            else if (up && down)
            {
                corners += RegionSides(grid, coords + Coordinates.Up, visited);
                corners += RegionSides(grid, coords + Coordinates.Down, visited);
            }
            else if (left && right)
            {
                corners += RegionSides(grid, coords + Coordinates.Left, visited);
                corners += RegionSides(grid, coords + Coordinates.Right, visited);
            }
            else if (left && up)
            {
                if (grid.GetValue(coords + Coordinates.UpLeft) == c)
                {
                    corners++;
                }
                else
                {
                    corners += 2;
                }
                corners += RegionSides(grid, coords + Coordinates.Left, visited);
                corners += RegionSides(grid, coords + Coordinates.Up, visited);
            }
            else if (left && down)
            {
                if (grid.GetValue(coords + Coordinates.DownLeft) == c)
                {
                    corners++;
                }
                else
                {
                    corners += 2;
                }
                corners += RegionSides(grid, coords + Coordinates.Left, visited);
                corners += RegionSides(grid, coords + Coordinates.Down, visited);
            }
            else if (right && up)
            {
                if (grid.GetValue(coords + Coordinates.UpRight) == c)
                {
                    corners++;
                }
                else
                {
                    corners += 2;
                }
                corners += RegionSides(grid, coords + Coordinates.Up, visited);
                corners += RegionSides(grid, coords + Coordinates.Right, visited);
            }
            else if (right && down)
            {
                if (grid.GetValue(coords + Coordinates.DownRight) == c)
                {
                    corners++;
                }
                else
                {
                    corners += 2;
                }
                corners += RegionSides(grid, coords + Coordinates.Down, visited);
                corners += RegionSides(grid, coords + Coordinates.Right, visited);
            }
            else if (up)
            {
                corners += 2;
                corners += RegionSides(grid, coords + Coordinates.Up, visited);
            }
            else if (down)
            {
                corners += 2;
                corners += RegionSides(grid, coords + Coordinates.Down, visited);
            }
            else if (left)
            {
                corners += 2;
                corners += RegionSides(grid, coords + Coordinates.Left, visited);
            }
            else if (right)
            {
                corners += 2;
                corners += RegionSides(grid, coords + Coordinates.Right, visited);
            }
            else
            {
                corners += 4;
            }
            return corners;
        }
    }
}
