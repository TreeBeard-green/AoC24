using Utility;
using System.Text.RegularExpressions;

namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            Console.WriteLine(Part1(input));

            Console.WriteLine(Part2(input));
        }

        static int Part1(string[] lines)
        {
            int sum = 0;
            Regex rx = new Regex(@"(mul[(][0-9]+[,][0-9]+[)])");

            foreach (string line in lines)
            {
                MatchCollection matches = rx.Matches(line);
                foreach (Match match in matches)
                {
                    MatchCollection numbers = new Regex(@"\d+").Matches(match.Value);
                    sum += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
                }
            }
            return sum;
        }

        static int Part2(string[] lines)
        {
            int sum = 0;
            bool doCount = true;
            Regex rx = new Regex(@"(mul[(][0-9]+[,][0-9]+[)])|(do\(\))|(don't\(\))");

            foreach (string line in lines)
            {
                MatchCollection matches = rx.Matches(line);
                foreach (Match match in matches)
                {
                    if (match.Value.Equals("do()"))
                    {
                        doCount = true;
                    }
                    else if (match.Value.Equals("don't()"))
                    {
                        doCount = false;
                    }
                    else if (doCount)
                    {
                        MatchCollection numbers = new Regex(@"\d+").Matches(match.Value);
                        sum += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
                    }
                }
            }
            return sum;
        }
    }
}
