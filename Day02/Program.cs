using Utility;

namespace Day02
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
            foreach (string report in input)
            {
                int[] numbers = ParseLine(report.Split(' '));

                if (numbers[0] < numbers[1])
                {
                    if (IsAscending(numbers))
                    {
                        if (DifferenceCheck(numbers))
                        {
                            sum++;
                        }
                    }
                }
                else
                {
                    if (IsDescending(numbers))
                    {
                        if (DifferenceCheck(numbers))
                        {
                            sum++;
                        }
                    }
                }
            }

            return sum;
        }

        static int Part2(string[] input)
        {
            int sum = 0;
            
            foreach (string report in input)
            {
                bool oneFailure = false;
                bool failure = false;
                int[] numbers = ParseLine(report.Split(' '));

                if (numbers[0] < numbers[1])
                {
                    (failure, oneFailure) = IsAscending(numbers, oneFailure);
                }
                else
                {
                    (failure, oneFailure) = IsDescending(numbers, oneFailure);
                }
                if (failure)
                {
                    if (DifferenceCheck(numbers, oneFailure))
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }

        static int[] ParseLine(string[] line)
        {
            int size = line.Length;
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = int.Parse(line[i]);
            }
            return arr;
        }

        static bool IsDescending(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] < arr[i])
                {
                    return false;
                }
            }
            return true;
        }

        static (bool, bool) IsDescending(int[] arr, bool fail)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] < arr[i])
                {
                    if (!fail)
                    {
                        fail = true;
                    }
                    else
                    {
                        return (false, fail);
                    }
                }
            }
            return (true, fail);
        }

        static bool IsAscending(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] > arr[i])
                {
                    return false;
                }
            }
            return true;
        }

        static (bool, bool) IsAscending(int[] arr, bool fail)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] > arr[i])
                {
                    if (!fail)
                    {
                        fail = true;
                    }
                    else
                    {
                        return (false, fail);
                    }
                }
            }
            return (true, fail);
        }

        static bool DifferenceCheck(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int dif = Math.Abs(arr[i - 1] - arr[i]);

                if (dif == 0 || dif > 3)
                {
                    return false;
                }
            }
            return true;
        }

        static bool DifferenceCheck(int[] arr, bool fail)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int dif = Math.Abs(arr[i - 1] - arr[i]);

                if (dif == 0 || dif > 3)
                {
                    if (!fail)
                    {
                        fail = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        
    }
}
