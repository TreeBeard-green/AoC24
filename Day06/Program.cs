using Utility;

namespace Day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            Grid<char> grid = Advent.ConvertInputToGrid<char>(input);
            var guardPos = GetGuardPos(grid);
            MarkPath(grid, guardPos);

            Console.WriteLine(Part1(grid));

            Console.WriteLine(Part2(grid, guardPos));
        }
        
        static int Part1(Grid<char> grid)
        {
            return CountX(grid);
        }

        static int Part2(Grid<char> grid, Coordinates guardPos)
        {
            int sum = 0;
            List<Coordinates> listOfX = FindAllX(grid);

            foreach (var x in listOfX)
            {
                if (IsGuardLooped(grid, guardPos, x))
                {
                    sum++;
                }
            }
            return sum;
        }

        static List<Coordinates> FindAllX(Grid<char> grid)
        {
            var list = new List<Coordinates>();
            for (int y = 0; y < grid.height; y++)
            {
                for (int x = 0; x < grid.width; x++)
                {
                    if (grid.GetValue(x, y) == 'X')
                    {
                        list.Add(new (x, y));
                    }
                }
            }
            return list;
        }

        static int CountX(Grid<char> grid)
        {
            int sum = 0;
            for (int y = 0; y < grid.height; y++)
            {
                for (int x = 0; x < grid.width; x++)
                {
                    if (grid.GetValue(x, y) == 'X')
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }

        static bool IsGuardLooped(Grid<char> grid, Coordinates curPos, Coordinates xPos)
        {
            int direction = 0;
            Coordinates nextPos;
            Coordinates[] walk = { Coordinates.Up, Coordinates.Right, Coordinates.Down, Coordinates.Left };
            var hits = new List<(Coordinates, int)>();

            // replace an X with new obstacle, revert change after checking
            grid.SetValue(xPos, '#');

            nextPos = curPos + walk[direction];

            while (nextPos.X != -1 && nextPos.Y != -1 && nextPos.Y != grid.height && nextPos.X != grid.width)
            {
                if (grid.GetValue(nextPos) == '#')
                {
                    if (hits.Contains((nextPos, direction)))
                    {
                        grid.SetValue(xPos, 'X');
                        return true;
                    }
                    else
                    {
                        hits.Add((nextPos, direction));
                    }
                    if (direction == 3)
                    {
                        direction = 0;
                    }
                    else
                    {
                        direction++;
                    }
                }
                else
                {
                    curPos = nextPos;
                }
                nextPos = curPos + walk[direction];
            }
            grid.SetValue(xPos, 'X');
            return false;
        }

        static void MarkPath(Grid<char> grid, Coordinates curPos)
        {
            int direction = 0;

            Coordinates nextPos;

            Coordinates[] walk = { Coordinates.Up, Coordinates.Right, Coordinates.Down, Coordinates.Left };

            nextPos = curPos + walk[direction];

            while (nextPos.X != -1 && nextPos.Y != -1 && nextPos.Y != grid.height && nextPos.X != grid.width)
            {
                if (grid.GetValue(nextPos) == '#')
                {
                    if (direction == 3)
                    {
                        direction = 0;
                    }
                    else
                    {
                        direction++;
                    }
                }
                else
                {
                    grid.SetValue(curPos, 'X');
                    curPos = nextPos;
                }
                nextPos = curPos + walk[direction];
            }
            grid.SetValue(curPos, 'X');
        }

        static Coordinates GetGuardPos(Grid<char> grid)
        {
            for (int x = 0; x < grid.height; x++)
            {
                for (int y = 0; y < grid.width; y++)
                {
                    if (grid.GetValue(x, y) == '^')
                    {
                        return new(x, y);
                    }
                }
            }
            return new(-1, -1);
        }

    }
}
