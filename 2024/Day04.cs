namespace AdventOfCode2024;

public static class Day04
{
    public static void Run(string input)
    {
        var lines = InputHelper.EnumerateLines(input);
        var map = new Map(string.Join("", lines), lines.First().Length);
        var countXMAS = 0;
        var countXedMAS = 0;

        for (var x = 0; x < map.Width; x++)
        {
            for (var y = 0; y < map.Height; y++)
            {
                if (map.Is(x, y, 'X'))
                {
                    //right
                    if (map.IsXMAS(x, y, 1, 0))
                    {
                        countXMAS++;
                    }
                    //left
                    if (map.IsXMAS(x, y, -1, 0))
                    {
                        countXMAS++;
                    }
                    //up
                    if (map.IsXMAS(x, y, 0, -1))
                    {
                        countXMAS++;
                    }
                    //down
                    if (map.IsXMAS(x, y, 0, 1))
                    {
                        countXMAS++;
                    }

                    // top left
                    if (map.IsXMAS(x, y, -1, -1))
                    {
                        countXMAS++;
                    }
                    // top right
                    if (map.IsXMAS(x, y, 1, -1))
                    {
                        countXMAS++;
                    }
                    // down right
                    if (map.IsXMAS(x, y, 1, 1))
                    {
                        countXMAS++;
                    }
                    // down left
                    if (map.IsXMAS(x, y, -1, 1))
                    {
                        countXMAS++;
                    }
                }

                if (map.Is(x, y, 'A'))
                {
                    if (map.IsXedMAS(x, y))
                    {
                        countXedMAS++;
                    }
                }
            }
        }
        Console.WriteLine(countXMAS);
        Console.WriteLine(countXedMAS);
    }

    private sealed class Map(string map, int width)
    {
        private readonly string map = map;
        public int Width { get; } = width;
        public int Height { get; } = map.Length / width;
        public char this[int x, int y]
        {
            get
            {
                ArgumentOutOfRangeException.ThrowIfNegative(x);
                ArgumentOutOfRangeException.ThrowIfNegative(y);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(x, Width);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(y, Height);
                return map[y * Width + x];
            }
        }

        public bool Is(int x, int y, char value)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return false;
            }

            return this[x, y] == value;
        }

        public bool IsXedMAS(int x, int y)
        {
            return ((Is(x + 1, y + 1, 'M') && Is(x - 1, y - 1, 'S')) || (Is(x + 1, y + 1, 'S') && Is(x - 1, y - 1, 'M')))
                && ((Is(x - 1, y + 1, 'M') && Is(x + 1, y - 1, 'S')) || (Is(x - 1, y + 1, 'S') && Is(x + 1, y - 1, 'M')));
        }

        public bool IsXMAS(int x, int y, int dx, int dy)
        {
            return Is(x + dx, y + dy, 'M') && Is(x + dx + dx, y + dy + dy, 'A') && Is(x + dx + dx + dx, y + dy + dy + dy, 'S');
        }
    }
}
