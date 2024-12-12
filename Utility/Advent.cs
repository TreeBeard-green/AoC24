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
        (source[index2], source[index1]) = (source[index1], source[index2]);
    }

    static string[] PadInput(string[] input, int size, char pad)
    {
        int h = input.Length, w = input[0].Length;
        string[] lines = new string[h + (size * 2)];
        string filler = string.Empty;

        for (int i = 0; i < w + (size * 2); i++)
        {
            filler += pad;
        }

        for (int i = 0; i < h; i++)
        {
            string t = string.Empty;
            for (int j = 0; j < size; j++)
            {
                t += pad;
            }
            lines[i + size] = t + input[i] + t;
        }

        for (int i = 0; i < size; i++)
        {
            lines[0 + i] = filler;
            lines[h + size + i] = filler;
        }
        return lines;
    }

    public static Grid<T> ConvertInputToGrid<T>(string[] input)
    {
        int w = input[0].Length, h = input.Length;
        T[,] output = new T[w, h];

        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                output[j, i] = (T)Convert.ChangeType(input[i][j], typeof(T));
            }
        }
        return new Grid<T>(output, w, h);
    }

    public static Grid<T> ConvertInputToGrid<T>(string[] input, int padSize, char pad)
    {
        input = PadInput(input, padSize, pad);
        int w = input[0].Length, h = input.Length;
        T[,] output = new T[w, h];

        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                output[j, i] = (T)Convert.ChangeType(input[i][j], typeof(T));
            }
        }
        return new Grid<T>(output, w, h);
    }
}

/// <summary>
/// X goes left to right, y goes north to south
/// </summary>
public struct Grid<T>(T[,] g, int w, int h)
{
    public int width = w, height = h;
    private T[,] grid = g;

    public readonly T GetValue(Coordinates coords)
    {
        int x, y;
        (x, y) = coords.GetCoords();
        return grid[x, y];
    }
    public readonly T GetValue(int x, int y)
    {
        return grid[x, y];
    }
    public void SetValue(Coordinates coords, T value)
    {
        int x, y;
        (x, y) = coords.GetCoords();
        grid[x, y] = value;
    }
    public void SetValue(int x, int y, T value)
    {
        grid[x, y] = value;
    }
}

public struct Coordinates(int n1, int n2)
{
    private int x = n1, y = n2;

    private static readonly Coordinates upCoords = new (0, -1);
    private static readonly Coordinates downCoords = new (0, 1);
    private static readonly Coordinates rightCoords = new (1, 0);
    private static readonly Coordinates leftCoords = new (-1, 0);
    private static readonly Coordinates upLeftCoords = new(-1, -1);
    private static readonly Coordinates upRightCoords = new(1, -1);
    private static readonly Coordinates downLeftCoords = new(-1, 1);
    private static readonly Coordinates downRightCoords = new(1, 1);

    public static Coordinates Up => upCoords;
    public static Coordinates Down => downCoords;
    public static Coordinates Left => leftCoords;
    public static Coordinates Right => rightCoords;
    public static Coordinates UpLeft => upLeftCoords;
    public static Coordinates UpRight => upRightCoords;
    public static Coordinates DownRight => downRightCoords;
    public static Coordinates DownLeft => downLeftCoords;

    public readonly int X => x;
    public readonly int Y => y;

    public readonly (int, int) GetCoords()
    {
        return (x, y);
    }

    public void AddCoords(Coordinates input)
    {
        x += input.x; y += input.y;
    }

    public void AddCoords((int, int) input)
    {
        x += input.Item1; y += input.Item2;
    }

    public void SubstractCoords(Coordinates input)
    {
        x -= input.x; y -= input.y;
    }

    public void SubstractCoords((int, int) input)
    {
        x -= input.Item1; y -= input.Item2;
    }

    public readonly Coordinates SumOfCoords((int, int) input)
    {
        return new(x + input.Item1, y + input.Item2);
    }

    public static Coordinates operator +(Coordinates c1, Coordinates c2)
    {
        return new(c1.x + c2.x, c1.y + c2.y);
    }

    public static Coordinates operator -(Coordinates c1, Coordinates c2)
    {
        return new(c1.x - c2.x, c1.y - c2.y);
    }
}