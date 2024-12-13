using Utility;
using System.Text.RegularExpressions;

namespace Day07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            Console.WriteLine(Part1(input));

            Console.WriteLine(Part2(input));
        }

        static long Part1(string[] input)
        {
            long sum = 0;
            var equations = ParseInput(input);
            foreach (var equation in equations)
            {
                if (IsEquationPossible(equation.Key, equation.Value, equation.Value[0], 1, false))
                {
                    sum += equation.Key;
                }
            }

            return sum;
        }

        static long Part2(string[] input)
        {
            long sum = 0;
            var equations = ParseInput(input);
            foreach (var equation in equations)
            {
                if (IsEquationPossible(equation.Key, equation.Value, equation.Value[0], 1, true))
                {
                    sum += equation.Key;
                }
            }

            return sum;
        }

        static bool IsEquationPossible(long total, List<long> numbers, long current, int id, bool isPart2)
        {
            if (numbers.Count == id)
            {
                if (total == current)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            long option1;
            long option2;
            long option3;
            long number = numbers[id];
            option1 = current + number;
            option2 = current * number;
            option3 = CombineNumber(current, number);
            if (IsEquationPossible(total, numbers, option1, id + 1, isPart2))
            {
                return true;
            }
            if (IsEquationPossible(total, numbers, option2, id + 1, isPart2))
            {
                return true;
            }
            if (isPart2)
            {
                if (IsEquationPossible(total, numbers, option3, id + 1, isPart2))
                {
                    return true;
                }
            }
            return false;
        }

        static long CombineNumber(long a, long b)
        {
            string x = a.ToString(), y = b.ToString();
            return long.Parse(x + y);
        }

        static Dictionary<long, List<long>> ParseInput(string[] input)
        {
            Regex rx = new Regex(@"\d+");
            MatchCollection matches;
            Dictionary<long, List<long>> output = new Dictionary<long, List<long>>();
            foreach (string s in input)
            {
                matches = rx.Matches(s);
                long key = long.Parse(matches[0].Value);
                for (int i = 1; i < matches.Count; i++)
                {
                    
                    if (!output.TryGetValue(key, out List<long>? value))
                    {
                        value = new List<long>();
                        output[key] = value;
                    }
                    value.Add(long.Parse(matches[i].Value));
                }
            }
            return output;
        }
    }
}
