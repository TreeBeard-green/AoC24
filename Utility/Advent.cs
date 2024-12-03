namespace Utility;

public class Advent
{
    public static string[] GetInput(string day)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory.Split(day)[0] + @"Data\" + day + ".txt";
        return File.ReadAllLines(path);
    }
}