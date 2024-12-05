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

        static int Part1(string[] input)
        {
            int sum = 0;
            (int[] leftArray, int[] rightArray) = GetArrays(input);

            Array.Sort(leftArray);
            Array.Sort(rightArray);

            for (int i = 0; i < leftArray.Length; i++)
            {
                sum += Math.Abs(leftArray[i] - rightArray[i]);
            }

            return sum;
        }

        static int Part2(string[] input)
        {
            int sum = 0;

            (int[] leftArray, int[] rightArray) = GetArrays((string[])input);

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

        static (int[], int[]) GetArrays(string[] input)
        {
            int size = input.Length;
            int[] left = new int[size];
            int[] right = new int[size];

            for (int i = 0; i < size; i++)
            {
                left[i] = int.Parse(input[i].Split(new string[] { "   " }, StringSplitOptions.None)[0]);
                right[i] = int.Parse(input[i].Split(new string[] { "   " }, StringSplitOptions.None)[1]);
            }
            return (left, right);
        }
    }
}
