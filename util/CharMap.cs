using System.Collections;

namespace AdventOfCode.Util;

public sealed class CharMap(string map, int width) : IEnumerable<char>
{
    private readonly string map = map;
    public int Width { get; } = width;
    public int Height { get; } = map.Length / width;

    public char this[int x, int y] => IsOnMap(x, y) ? map[y * Width + x] : '\0';
    public char this[Vector2 pos] => this[pos.X, pos.Y];
    public bool IsOnMap(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
    public bool IsOnMap(Vector2 pos) => IsOnMap(pos.X, pos.Y);
    public Vector2 IndexToPos(int index) => new(index % Width, index / Width);

    public static CharMap CreateFromLines(string map)
    {
        var lines = InputHelper.EnumerateLines(map);
        return new CharMap(string.Join("", lines), lines.First().Length);
    }

    public IEnumerator<char> GetEnumerator() => map.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => map.GetEnumerator();
}