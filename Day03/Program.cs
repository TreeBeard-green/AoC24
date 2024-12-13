using Utility;
using System.Text.RegularExpressions;

namespace Day03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            Console.WriteLine(Part1(input));

            Console.WriteLine(Part2(input));
        }

        static int Part1(string[] input)
        {
            int sum = 0;
            Regex rx = new Regex(@"mul\(([0-9]+),([0-9]+)\)");

            foreach (string line in input)
            {
                MatchCollection matches = rx.Matches(line);
                foreach (Match match in matches)
                {
                    sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                }
            }
            return sum;
        }

        static int Part2(string[] input)
        {
            int sum = 0;
            bool doCount = true;
            Regex rx = new Regex(@"mul\(([0-9]+),([0-9]+)\)|do\(\)|don't\(\)");

            foreach (string line in input)
            {
                MatchCollection matches = rx.Matches(line);
                foreach (Match match in matches)
                {
                    if (match.Value == "do()")
                    {
                        doCount = true;
                    }
                    else if (match.Value == "don't()")
                    {
                        doCount = false;
                    }
                    else if (doCount)
                    {
                        sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                    }
                }
            }
            return sum;
        }
    }
}
