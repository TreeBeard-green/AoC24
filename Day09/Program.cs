using System.Collections;
using Utility;

namespace Day09
{
    /*
     * Part 1 functional but inefficient, needs remaking
     * Part 2 seems not terrible, but needs cleaning
    */

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Advent.GetInput(typeof(Program).Namespace);

            Console.WriteLine(Part1(input[0]));
            Console.WriteLine(Part2(input[0]));
        }

        static long Part1(string input)
        {
            long sum = 0, empty = 0, dot = -1;
            string disk = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                int number = input[i] - '0';
                if (i % 2 == 0)
                {
                    for (int j = 0; j < number; j++)
                    {
                        disk += (char)(i / 2);
                    }
                }
                else
                {
                    for (int j = 0; j < number; j++)
                    {
                        disk += (char)dot;
                    }
                    empty += number;
                }    
            }
            int length = disk.Length - 1;
            for (int i = 0; i < empty; i++)
            {
                char temp = disk[length];
                int id = disk.IndexOf((char)dot);
                disk = disk.Remove(id) + temp + disk[(id + 1)..];
                disk = disk.Remove(length);
                length--;
            }
            for (int i = 1; i < disk.Length; i++)
            {
                int temp = disk[i];
                sum += temp * i;
            }
            return sum;
        }

        static long Part2(string input)
        {
            long sum = 0;
            // key = id ||| value = size
            Dictionary<int, int> ogDisk = [];
            List<int> list = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                int number = input[i] - '0';
                if (i % 2 == 0)
                {
                    ogDisk[i / 2] = number;
                }
                else
                {
                    list.Add(number);
                }
            }

            Dictionary<int, List<(int, int)>> movedKeys = [];
            Dictionary<int, int> removedKeys = [];
            int idr = ogDisk.Keys.Last();

            while (idr > 0)
            {
                if (list.Max() >= ogDisk[idr])
                {
                    int idl = 0;
                    while (list[idl] < ogDisk[idr] && idl < idr)
                    {
                        idl++;
                    }
                    if (idl < idr)
                    {
                        // new location of key
                        if (!movedKeys.TryGetValue(idl, out List<(int, int)>? value))
                        {
                            value = new List<(int, int)>();
                            movedKeys[idl] = value;
                        }
                        movedKeys[idl].Add((idr, ogDisk[idr]));
                        // empty space decreased
                        list[idl] -= ogDisk[idr];
                        // remember where number used to be
                        removedKeys[idr] = ogDisk[idr];
                        // remove from original
                        ogDisk.Remove(idr);
                    }
                }
                idr--;
            }

            // Files that didnt move > files that were removed (empty spaces left behind) > files that were moved > empty spots 
            int count = 0;
            for (int i = 0; i <= ogDisk.Keys.Last(); i++)
            {
                if (ogDisk.TryGetValue(i, out int value))
                {
                    for (int j = 0; j < value; j++)
                    {
                        sum += i * count;
                        count++;
                    }
                }

                if (removedKeys.TryGetValue(i, out int value3))
                {
                    for (int j = 0; j < value3; j++)
                    {
                        count++;
                    }
                }

                if (movedKeys.TryGetValue(i, out List<(int, int)>? value2))
                {
                    if (value2 != null)
                    {
                        foreach (var item in value2)
                        {
                            for (int j = 0; j < item.Item2; j++)
                            {
                                sum += item.Item1 * count;
                                count++;
                            }
                        }
                    }
                }
                if(i < list.Count)
                for (int j = 0; j < list[i]; j++)
                {
                    count++;
                }
            }
            return sum;
        }


    }
}
