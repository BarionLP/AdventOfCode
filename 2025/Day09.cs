namespace AdventOfCode2025;

public static class Day09
{
    public static void RunExample() => Run("""
        7,1
        11,1
        11,7
        9,7
        9,5
        2,5
        2,3
        7,3
        """);
    public static async Task Run() => Run(await InputHelper.GetInput(2025, 9));

    public static void Run(ReadOnlySpan<char> input)
    {
        var points = new List<Vector2>();

        foreach (var pointRange in input.Split('\n'))
        {
            var pointSpan = input[pointRange].Trim();
            if (pointSpan.IsEmpty) continue;
            points.Add(Vector2.Parse(pointSpan));
        }

        RunPart1(points);
        RunPart2(points);
    }

    public static void RunPart1(List<Vector2> points)
    {
        var max = 0L;

        foreach (var p1 in points)
        {
            foreach (var p2 in points)
            {
                var size = p1 - p2;

                var area = (long.Abs(size.X) + 1) * (long.Abs(size.Y) + 1);
                if (area > max)
                {
                    max = area;
                }
            }
        }

        Console.WriteLine($"Part 1: {max}");
    }

    public static void RunPart2(List<Vector2> points)
    {
        var minX = points.Min(x => x.X);
        var maxX = points.Max(x => x.X);

        var minY = points.Min(x => x.Y);
        var maxY = points.Max(x => x.Y);

        var map = CharMap.Create(maxX - minX, maxX - minY, '.');

        foreach (var i in ..points.Count)
        {
            var i_prev = i - 1;
            if (i_prev < 0) i_prev += points.Count;
            var p = points[i];
            var p_previous = points[i_prev];

            map[p.X - minX, p.Y - minY] = 'X';
        }
    }
}