using Utility;
using System.Text.RegularExpressions;

namespace Day13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            var machines = ParseInput(input);

            Console.WriteLine(Part1(machines));

            Console.WriteLine(Part2(machines));
        }

        static long Part1(List<Coordinates[]> machines)
        {
            long sum = 0;
            foreach (var machine in machines)
            {
                float a, b;
                a = (float)(machine[2].Y * machine[1].X - machine[1].Y * machine[2].X) / (machine[0].Y * machine[1].X - machine[0].X * machine[1].Y);
                if (a % 1 == 0)
                {
                    sum += (long)Math.Abs(a * 3) + (long)Math.Abs((machine[2].X - machine[0].X * a) / machine[1].X);
                }
            }

            return sum;
        }

        static long Part2(List<Coordinates[]> machines)
        {
            // Part 2 is a joke 
            long sum = 0;
            foreach (var machine in machines)
            {
                double a, b;
                a = (double)((machine[2].Y + 10000000000000) * machine[1].X - machine[1].Y * (machine[2].X + 10000000000000)) / (machine[0].Y * machine[1].X - machine[0].X * machine[1].Y);
                if (a % 1 == 0)
                {
                    sum += (long)Math.Abs(a * 3) + (long)Math.Abs(((machine[2].X + 10000000000000) - machine[0].X * a) / machine[1].X);
                }
            }

            return sum;
        }


        static List<Coordinates[]> ParseInput(string[] input)
        {
            Regex rx = new Regex(@"\d+");
            var result = new List<Coordinates[]>();
            var arr = new Coordinates[3]; // 0 - A button, 1 - B button, 2 - target
            int id = 0;
            foreach (var line in input)
            {
                if (line == string.Empty)
                {
                    id = 0;
                    result.Add(arr.ToArray());
                    continue;
                }
                MatchCollection matches = rx.Matches(line);
                arr[id++] = new Coordinates(int.Parse(matches[0].Value), int.Parse(matches[1].Value));
            }
            result.Add(arr.ToArray());
            return result;
        }

    }
}
