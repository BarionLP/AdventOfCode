namespace AdventOfCode2024;

public static class Day10
{
    public static void Run(string input)
    {
        var lines = InputHelper.EnumerateLines(input);
        var map = new CharMap(string.Join("", lines), lines.First().Length);
        var startingPoints = map.Index().Where(p => p.Item == '0').Select(p => map.IndexToPos(p.Index));

        //var cout = 0;
        List<Vector2> set = []; //make hashset for pt1
        foreach (var point in startingPoints)
        {
            FindPaths(map, point, set);
            //count += set.Count;
            //set.Clear();
        }

        Console.WriteLine(set.Count);
    }

    private static void FindPaths(CharMap map, Vector2 pos, List<Vector2> endPositions)
    {
        if (map[pos] == '9')
        {
            endPositions.Add(pos);
            return;
        }

        var next = (char)(map[pos] + 1);
        if (map[pos + Vector2.Up] == next)
        {
            FindPaths(map, pos + Vector2.Up, endPositions);
        }
        if (map[pos + Vector2.Down] == next)
        {
            FindPaths(map, pos + Vector2.Down, endPositions);
        }
        if (map[pos + Vector2.Left] == next)
        {
            FindPaths(map, pos + Vector2.Left, endPositions);
        }
        if (map[pos + Vector2.Right] == next)
        {
            FindPaths(map, pos + Vector2.Right, endPositions);
        }
    }
}
