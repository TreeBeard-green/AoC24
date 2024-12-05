namespace Utility;

public static class Advent
{
    public static string[] GetInput(string day)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory.Split(day)[0] + @"Data\" + day + ".txt";
        return File.ReadAllLines(path);
    }

    public static void SwapValues<T>(this T[] source, int index1, int index2)
    {
        T temp = source[index1];
        source[index1] = source[index2];
        source[index2] = temp;
    }
}