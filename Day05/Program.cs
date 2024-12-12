using Utility;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            (Dictionary<int, List<int>> rules, int[][] updates) = ParseInput(input);

            Console.WriteLine(Part1(rules, updates));

            Console.WriteLine(Part2(rules, updates));
        }

        static int Part1(Dictionary<int, List<int>> rules, int[][] updates)
        {
            int sum = 0;
            foreach (int[] arr in updates)
            {
                if (InOrder(arr, rules))
                {
                    sum += arr[arr.Length / 2];
                }
            }
            return sum;
        }

        static int Part2(Dictionary<int, List<int>> rules, int[][] updates)
        {
            int sum = 0;
            foreach (int[] arr in updates)
            {
                if (!InOrder(arr, rules))
                {
                    SortArray(arr, rules);
                    sum += arr[arr.Length / 2];
                }
            }
            return sum;
        }

        static int[] SortArray(int[] arr, Dictionary<int, List<int>> rules)
        {
            int n = arr.Length - 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    if (rules.ContainsKey(arr[j]) && rules[arr[j]].Contains(arr[j + 1]))
                    {
                        arr.SwapValues(j, j + 1);
                    }
                }
            }
            return arr;
        }

        static bool InOrder(int[] arr, Dictionary<int, List<int>> rules)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (!rules.ContainsKey(arr[i]) || !rules[arr[i]].Contains(arr[i + 1]))
                {
                    return false;
                }
            }
            return true;
        }

        static (Dictionary<int, List<int>>, int[][]) ParseInput(string[] input)
        {
            int id = Array.IndexOf(input, string.Empty);
            Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
            int[][] updates = new int[input.Length - id - 1][];

            // Create map of rules
            for (int i = 0; i < id; i++)
            {
                int leftPage = int.Parse(input[i].Split('|')[0]), rightPage = int.Parse(input[i].Split('|')[1]);
                if (rules.ContainsKey(leftPage))
                {
                    rules[leftPage].Add(rightPage);
                }
                else
                {
                    rules[leftPage] = new List<int> {rightPage};
                }
            }
            // Create jagged array of updates
            for (int i = id + 1; i < input.Length; i++)
            {
                string[] strings = input[i].Split(",");
                int[] nums = new int[strings.Length];
                for (int j = 0; j < strings.Length; j++)
                {
                    nums[j] = int.Parse(strings[j]);
                }
                updates[i - id - 1] = nums;
            }
            return (rules, updates);
        }
    }
}
