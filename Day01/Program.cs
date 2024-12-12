using Utility;

namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            (int[], int[]) arrays = GetArrays(input);

            Console.WriteLine(Part1(arrays.Item1, arrays.Item2));

            Console.WriteLine(Part2(arrays.Item1, arrays.Item2));
        }

        static int Part1(int[] leftArray, int[] rightArray)
        {
            Array.Sort(leftArray);
            Array.Sort(rightArray);
            int sum = 0;

            for (int i = 0; i < leftArray.Length; i++)
            {
                sum += Math.Abs(leftArray[i] - rightArray[i]);
            }

            return sum;
        }

        static int Part2(int[] leftArray, int[] rightArray)
        {
            var rightNumbers = new Dictionary<int, int>();

            foreach (int i in rightArray) 
            {
                if (rightNumbers.TryGetValue(i, out int value))
                {
                    rightNumbers[i] = ++value;
                }
                else
                {
                    rightNumbers[i] = 1;
                }
            }
            int sum = 0;

            foreach (int i in leftArray)
            {
                if (rightNumbers.TryGetValue(i, out int value))
                {
                    sum += value * i;
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
                var arrays = input[i].Split(new string[] { "   " }, StringSplitOptions.None);
                left[i] = int.Parse(arrays[0]);
                right[i] = int.Parse(arrays[1]);
            }
            return (left, right);
        }
    }
}
