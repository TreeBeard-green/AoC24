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
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (input[x, y] == 'X')
                    {
                        list.Add((x, y));
                    }
                }
            }
            return list;
        }

        static int CountX(char[,] input)
        {
            int w = input.GetLength(0), h = input.GetLength(1);
            int sum = 0;
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (input[x, y] == 'X')
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
            (x, y) = guardsPos;
            int w = input.GetLength(0), h = input.GetLength(1);
            // Coordinates in (y, x) arrengement
            (int, int)[] walk = { (-1, 0), (0, 1), (1, 0), (0, -1) };
            List<(int, int, int)> hits = new List<(int, int, int)>();

            // replace an X with new obstacle, revert change after checking
            input[xPos.Item1, xPos.Item2] = '#';

            (nextX, nextY) = AddTuples((x, y), walk[direction]);

            while (nextY != -1 && nextX != -1 && nextY != h && nextX != w)
            {
                if (input[nextX, nextY] == '#')
                {
                    if (hits.Contains((nextX,nextY, direction)))
                    {
                        input[xPos.Item1, xPos.Item2] = 'X';
                        return true;
                    }
                    else
                    {
                        hits.Add((nextX, nextY, direction));
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
                    (x, y) = (nextX, nextY);
                }
                (nextX, nextY) = AddTuples((x, y), walk[direction]);
            }
            input[xPos.Item1, xPos.Item2] = 'X';
            return false;
        }

        static void MarkPath(char[,] input, (int, int) guardPos)
        {
            int x, y, nextX, nextY, direction = 0;
            (x, y) = guardPos;
            int w = input.GetLength(0), h = input.GetLength(1);
            // Coordinates in (y, x) arrengement
            (int, int)[] walk = { (-1, 0), (0, 1), (1, 0), (0, -1) };

            (nextX, nextY) = AddTuples((x, y), walk[direction]);

            while (nextY != -1 && nextX != -1 && nextY != h && nextX != w)
            {
                if (input[nextX, nextY] == '#')
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
                    input[x, y] = 'X';
                    (x, y) = (nextX, nextY);
                }
                (nextX, nextY) = AddTuples((x, y), walk[direction]);
            }
            input[x, y] = 'X';
        }

        static (int, int) GetGuardPos(char[,] input)
        {
            int w = input.GetLength(0), h = input.GetLength(1);

            for (int x = 0; x < h; x++)
            {
                for (int y = 0; y < w; y++)
                {
                    if (input[x, y] == '^')
                    {
                        return (x, y);
                    }
                }
            }
            return (-1, -1);
        }

        static char[,] ParseInput(string[] input)
        {
            int w = input[0].Length, h = input.Length;
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
