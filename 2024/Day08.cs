using System.Text;

namespace AdventOfCode2024;

public static class Day08
{
    public static void Run(string input)
    {
        var lines = InputHelper.EnumerateLines(input);
        var mapString = string.Join("", lines);
        var map = new Map(mapString, lines.First().Length);

        var antennas = mapString.Index().Where(x => x.Item != '.')
                                .Select(x => (frequency: x.Item, position: map.GetPos(x.Index)))
                                .GroupBy(antenna => antenna.frequency);
        
        foreach (var antennaGroup in antennas)
        {
            foreach (var antenna in antennaGroup)
            {
                foreach (var other in antennaGroup)
                {
                    var diff = antenna.position - other.position;
                    if (diff.MagnitudeSqr > 0)
                    {
                        var target = antenna.position;
                        while (map.IsOnMap(target))
                        {
                            map[target] = '#';
                            target += diff;
                        }
                    }
                }
            }
        }

        Console.WriteLine(map);
        Console.WriteLine(map.array.Count(c => c == '#'));
    }

    private sealed class Map(string map, int width)
    {
        public readonly char[] array = map.ToCharArray();

        public ref char this[int x, int y] => ref array[y * Width + x];
        public ref char this[Vector2 pos] => ref array[pos.Y * Width + pos.X];
        public int Width { get; } = width;
        public int Height { get; } = map.Length / width;

        public bool IsOnMap(Vector2 pos) => pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;

        public Vector2 GetPos(int index) => new(index % Width, index / Width);

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < array.Length; i += Width)
            {
                sb.Append(array.AsSpan(i, Width));
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}