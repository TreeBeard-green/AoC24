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

    public static Grid ConvertInputToGrid(string[] input)
    {
        // Output array is properly x y oriented
        int w = input[0].Length, h = input.Length;
        char[,] output = new char[w, h];
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                output[j, i] = input[i][j];
            }
        }
        return new Grid(output, w, h);
    }
}
public struct Grid(char[,] g, int w, int h)
{
    public int width = w, height = h;
    private char[,] grid = g;

    public readonly char GetValue(int x, int y)
    {
        return grid[x, y];
    }

    public void SetValue(int x, int y, char c)
    {
        grid [x, y] = c;
    }
}

public struct Coordinates(int x, int y)
{
    private int X = x, Y = y;

    public readonly (int, int) GetCoords()
    {
        return (X, Y);
    }

    public void AddCoords((int, int) t)
    {
        X += t.Item1; Y += t.Item2;
    }
    public void SubstractCoords((int, int) t)
    {
        X -= t.Item1; Y -= t.Item2;
    }
}