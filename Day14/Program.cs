using Utility;

namespace Day14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            Console.WriteLine(Part1(input, 101, 103));

            Part2(input, 101, 103);
        }

        static int Part1(string[] input, int width, int height)
        {
            var grid = Advent.CreateEmptyGrid(width, height, 0);
            Coordinates pos, velocity;

            foreach (string s in input)
            {
                (pos, velocity) = ParseLine(s);
                // Robots move in straight line just multiply velocity 100X
                pos += velocity.Multiplied(100);

                if (pos.X > width - 1)
                {
                    pos.SubstractCoords((width * (pos.X / width), 0));
                }
                if (pos.Y > height - 1)
                {
                    pos.SubstractCoords((0, height * (pos.Y / height)));
                }
                if (pos.X < 0)
                {
                    double temp = Math.Abs((double)pos.X / width);
                    pos = new Coordinates(width * (int)Math.Ceiling(temp) + pos.X, pos.Y);
                }
                if (pos.Y < 0)
                {
                    double temp = Math.Abs((double)pos.Y / height);
                    pos = new Coordinates(pos.X, height * (int)Math.Ceiling(temp) + pos.Y);
                }
                
                grid.SetValue(pos, (grid.GetValue(pos) + 1));
            }
            return CalculateSafetyFactor(grid);
        }

        static void Part2(string[] input, int width, int height)
        {
            // Done with newest Human_eye© technology

            Coordinates pos;
            var list = ParseInput(input);
            string path = AppDomain.CurrentDomain.BaseDirectory.Split("Day14")[0] + @"Data\out.txt";
            TextWriter textFile = new StreamWriter(path, true);
            // guards cluster at this iterations 
            int out1 = 58, out2 = 99;
            for (int i = 1; i < 10000; i++)
            {
                // Move each robot
                for (int j = 0; j < list.Count; j++)
                {
                    pos = list[j].Item1 + list[j].Item2;
                    list[j] = (CheckRobotBounds(pos, width, height), list[j].Item2);
                }
                // print out only clustered iterations
                if (i == out1 || i == out2)
                {
                    if(i == out1)
                    {
                        out1 += 103;
                    }
                    else
                    {
                        out2 += 101;
                    }
                    Coordinates[] coords = new Coordinates[list.Count];
                    for (int q = 0; q < list.Count; q++)
                    {
                        coords[q] = list[q].Item1;
                    }
                    string temp = string.Empty;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (coords.Contains(new Coordinates(x, y)))
                            {
                                temp += '0';
                            }
                            else
                            {
                                temp += '.';
                            }
                        }
                        textFile.WriteLine(temp);
                        temp = string.Empty;
                    }
                    textFile.WriteLine(i);
                }
            }
            textFile.Close();
        }

        static (Coordinates, Coordinates) ParseLine(string line)
        {
            Coordinates pos, vel;
            string[] sides = line.Split(' ');
            string[] sideL = sides[0].Substring(2).Split(','), sideR = sides[1].Substring(2).Split(',');
            pos = new Coordinates(int.Parse(sideL[0]), int.Parse(sideL[1]));
            vel = new Coordinates(int.Parse(sideR[0]), int.Parse(sideR[1]));
            return (pos, vel);
        }

        static List<(Coordinates, Coordinates)> ParseInput(string[] input)
        {
            Coordinates pos, vel;
            var list = new List<(Coordinates, Coordinates)>();
            foreach (string line in input)
            {
                string[] sides = line.Split(' ');
                string[] sideL = sides[0].Substring(2).Split(','), sideR = sides[1].Substring(2).Split(',');
                pos = new Coordinates(int.Parse(sideL[0]), int.Parse(sideL[1]));
                vel = new Coordinates(int.Parse(sideR[0]), int.Parse(sideR[1]));
                list.Add((pos, vel));
            }
            return list;
        }

        static int CalculateSafetyFactor(Grid<int> grid)
        {
            int temp = 0, sum = 1;
            for (int y = 0; y < grid.height / 2; y++)
            {
                for (int x = 0; x < grid.width / 2; x++)
                {
                    temp += grid.GetValue(x, y);
                }
            }
            sum *= temp;
            temp = 0;
            for (int y = (grid.height + 1) / 2; y < grid.height; y++)
            {
                for (int x = 0; x < grid.width / 2; x++)
                {
                    temp += grid.GetValue(x, y);
                }
            }
            sum *= temp;
            temp = 0;
            for (int y = 0; y < grid.height / 2; y++)
            {
                for (int x = (grid.width + 1) / 2; x < grid.width; x++)
                {
                    temp += grid.GetValue(x, y);
                }
            }
            sum *= temp;
            temp = 0;
            for (int y = (grid.height + 1) / 2; y < grid.height; y++)
            {
                for (int x = (grid.width + 1) / 2; x < grid.width; x++)
                {
                    temp += grid.GetValue(x, y);
                }
            }
            return sum *= temp;
        }

        static Coordinates CheckRobotBounds(Coordinates pos, int width, int height)
        {
            if (pos.X > width - 1)
            {
                pos.SubstractCoords((width, 0));
            }
            if (pos.Y > height - 1)
            {
                pos.SubstractCoords((0, height));
            }
            if (pos.X < 0)
            {
                pos = new Coordinates(width + pos.X, pos.Y);
            }
            if (pos.Y < 0)
            {
                pos = new Coordinates(pos.X, height + pos.Y);
            }
            return pos;
        }

    }
}
