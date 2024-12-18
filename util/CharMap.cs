using System.Collections;
using System.Text;

namespace AdventOfCode.Util;

public sealed class CharMap(char[] map, int width) : IEnumerable<char>
{
    private readonly char[] map = map;

    public CharMap(string map, int width) 
    : this(map.ToCharArray(), width) { }

    public int Width { get; } = width;
    public int Height { get; } = map.Length / width;

    public char this[int x, int y]
    {
        get => IsOnMap(x, y) ? map[y * Width + x] : '\0';
        set
        {
            if (IsOnMap(x, y))
            {
                map[y * Width + x] = value;
            }
        }
    }

    public char this[Vector2 pos]
    {
        get => this[pos.X, pos.Y];
        set => this[pos.X, pos.Y] = value;
    }

    public bool IsOnMap(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
    public bool IsOnMap(Vector2 pos) => IsOnMap(pos.X, pos.Y);
    public Vector2 IndexToPos(int index) => new(index % Width, index / Width);
    public Vector2 PositionOf(char symbol) => IndexToPos(Array.IndexOf(map, symbol));

    public static CharMap CreateFromLines(string map)
        => CreateFromLines(InputHelper.EnumerateLines(map));
    public static CharMap CreateFromLines(IEnumerable<string> lines)
        => new(string.Join("", lines), lines.First().Length);

    public static CharMap Create(int width, int height, char symbol)
    {
        var data = new char[width * height];
        data.AsSpan().Fill(symbol);
        return new(data, height);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < map.Length; i += Width)
        {
            sb.Append(map.AsSpan(i, Width));
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public IEnumerator<char> GetEnumerator()
    {
        foreach (var @char in map)
        {
            yield return @char;
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => map.GetEnumerator();
}