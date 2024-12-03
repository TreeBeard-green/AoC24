using Utility;

namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            Console.WriteLine(Part1(input));

            Console.WriteLine(Part2(input));
        }
        static (int[], int[]) GetArrays(string[] lines)
        {
            int[] left = new int[lines.Length];
            int[] right = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                left[i] = int.Parse(lines[i].Split(new string[] { "   " }, StringSplitOptions.None)[0]);
                right[i] = int.Parse(lines[i].Split(new string[] { "   " }, StringSplitOptions.None)[1]);
            }
            return (left, right);
        }

        static int Part1(string[] lines)
        {
            int sum = 0;
            (int[] leftArray, int[] rightArray) = GetArrays(lines);

            Array.Sort(leftArray);
            Array.Sort(rightArray);

            for (int i = 0; i < leftArray.Length; i++)
            {
                sum += Math.Abs(leftArray[i] - rightArray[i]);
            }

            return sum;
        }

        static int Part2(string[] lines)
        {
            int sum = 0;

            (int[] leftArray, int[] rightArray) = GetArrays((string[])lines);

            Dictionary <int, int> rightNumbers = new Dictionary<int, int>();

            foreach (int i in rightArray) 
            {
                if (rightNumbers.ContainsKey(i))
                {
                    rightNumbers[i]++;
                }
                else
                {
                    rightNumbers[i] = 1;
                }
            }

            foreach (int i in leftArray)
            {
                if (rightNumbers.ContainsKey(i))
                {
                    sum += rightNumbers[i] * i;
                }
            }

            return sum;
        }

    }
}
