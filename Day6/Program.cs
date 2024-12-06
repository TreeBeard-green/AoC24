using Utility;

namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            char[,] arr = ParseInput(input);
            var guardPos = GetGuardPos(arr);
            MarkPath(arr, guardPos);

            Console.WriteLine(Part1(arr));

            Console.WriteLine(Part2(arr, guardPos));
        }
        
        static int Part1(char[,] input)
        {
            return CountX(input);
        }

        static int Part2(char[,] input, (int, int) guardPos)
        {
            int sum = 0;
            List<(int, int)> listOfX = FindAllX(input);

            foreach (var x in listOfX)
            {
                if (IsGuardLooped(input, guardPos, x))
                {
                    sum++;
                }
            }
            return sum;
        }

        static (int, int) AddTuples((int, int) t1, (int, int) t2)
        {
            return (t1.Item1 + t2.Item1, t1.Item2 + t2.Item2);
        }

        static List<(int, int)> FindAllX(char[,] input)
        {
            int w = input.GetLength(0), h = input.GetLength(1);
            List<(int, int)> list = new List<(int, int)>();
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (input[y, x] == 'X')
                    {
                        list.Add((y, x));
                    }
                }
            }
            return list;
        }

        static int CountX(char[,] input)
        {
            int w = input.GetLength(0), h = input.GetLength(1);
            int sum = 0;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (input[y, x] == 'X')
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }

        static bool IsGuardLooped(char[,] input, (int, int) guardsPos, (int, int) xPos)
        {
            int x, y, nextX, nextY, direction = 0;
            (y, x) = guardsPos;
            int w = input.GetLength(0), h = input.GetLength(1);
            // Coordinates in (y, x) arrengement
            (int, int)[] walk = { (-1, 0), (0, 1), (1, 0), (0, -1) };
            List<(int, int, int)> hits = new List<(int, int, int)>();

            // replace an X with new obstacle, revert change after checking
            input[xPos.Item1, xPos.Item2] = '#';

            (nextY, nextX) = AddTuples((y, x), walk[direction]);

            while (nextX != -1 && nextY != -1 && nextX != w && nextY != h)
            {
                if (input[nextY, nextX] == '#')
                {
                    if (hits.Contains((nextY,nextX, direction)))
                    {
                        input[xPos.Item1, xPos.Item2] = 'X';
                        return true;
                    }
                    else
                    {
                        hits.Add((nextY, nextX, direction));
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
                    (y, x) = (nextY, nextX);
                }
                (nextY, nextX) = AddTuples((y, x), walk[direction]);
            }
            input[xPos.Item1, xPos.Item2] = 'X';
            return false;
        }

        static void MarkPath(char[,] input, (int, int) guardPos)
        {
            int x, y, nextX, nextY, direction = 0;
            (y, x) = guardPos;
            int w = input.GetLength(0), h = input.GetLength(1);
            // Coordinates in (y, x) arrengement
            (int, int)[] walk = { (-1, 0), (0, 1), (1, 0), (0, -1) };

            (nextY, nextX) = AddTuples((y, x), walk[direction]);

            while (nextX != -1 && nextY != -1 && nextX != w && nextY != h)
            {
                if (input[nextY, nextX] == '#')
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
                    input[y, x] = 'X';
                    (y, x) = (nextY, nextX);
                }
                (nextY, nextX) = AddTuples((y, x), walk[direction]);
            }
            input[y, x] = 'X';
        }

        static (int, int) GetGuardPos(char[,] input)
        {
            int w = input.GetLength(0), h = input.GetLength(1);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (input[y, x] == '^')
                    {
                        return (y, x);
                    }
                }
            }
            return (-1, -1);
        }

        static char[,] ParseInput(string[] input)
        {
            int w = input.Length, h = input[0].Length;
            char[,] output = new char[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    output[i,j] = input[i][j];
                }
            }
            return output;
        }
    }
}
