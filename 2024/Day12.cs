namespace AdventOfCode2024;

public static class Day12
{
    private static CharMap map = null!;
    private static readonly HashSet<Vector2> knownBlocks = [];
    public static void Run(string input)
    {
        map = CharMap.CreateFromLines(input);

        var count = 0;

        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                if (knownBlocks.Contains((x, y)))
                {
                    continue;
                }
                count += Price((x, y));
            }

        }
        Console.WriteLine(count);
    }

    private static int Price(Vector2 pos)
    {
        var group = FindGroup(map[pos], pos);
        var sides = group.Sum(CountOpenSides);
        Console.WriteLine($"{map[pos]}: {group.Count} * {sides}");
        return group.Count * sides;
    }

    private static int CountOpenSides(Vector2 pos)
    {
        var plant = map[pos];
        var count = 0;
        if (map[pos + (1, 0)] != plant)
        {
            count++;
        }
        if (map[pos + (-1, 0)] != plant)
        {
            count++;
        }
        if (map[pos + (0, 1)] != plant)
        {
            count++;
        }
        if (map[pos + (0, -1)] != plant)
        {
            count++;
        }
        return count;
    }

    private static HashSet<Vector2> FindGroup(char plant, Vector2 pos)
    {
        var result = new HashSet<Vector2>();

        FloodFill(plant, pos);

        return result;

        void FloodFill(char plant, Vector2 pos)
        {
            if (map[pos] != plant || !knownBlocks.Add(pos))
                return;

            result.Add(pos);

            FloodFill(plant, pos + (1, 0));
            FloodFill(plant, pos + (-1, 0));
            FloodFill(plant, pos + (0, -1));
            FloodFill(plant, pos + (0, 1));
        }
    }
}