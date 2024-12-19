namespace AdventOfCode2024;

public static class Day18
{
    public static void Run(string input)
    {
        var mapSize = 71;
        var bytePositions = InputHelper.EnumerateLines(input).Select(Vector2.Parse);
        var map = CharMap.Create(mapSize, mapSize, '.');

        foreach (var pos in bytePositions.Take(1024))
        {
            map[pos] = '#';
        }


        var (steps, path) = FindFewestStepsPath(map, (0, 0), (70, 70));

        Console.WriteLine(steps);

        foreach (var pos in bytePositions.Skip(1024))
        {
            map[pos] = '#';

            if (path.Contains(pos))
            {
                (steps, path) = FindFewestStepsPath(map, (0, 0), (70, 70));

                if (steps < 0)
                {
                    Console.WriteLine(pos);
                    break;
                }
            }
        }
    }

    private static readonly Vector2[] directions = [(0, 1), (1, 0), (0, -1), (-1, 0)];
    public static (int steps, ImmutableArray<Vector2> paths) FindFewestStepsPath(CharMap grid, Vector2 start, Vector2 end)
    {
        var queue = new PriorityQueue<(Vector2 position, int steps, ImmutableArray<Vector2> path), int>();
        var visited = new HashSet<Vector2>();

        queue.Enqueue((start, 0, [start]), 0);

        while (queue.Count > 0)
        {
            var (position, steps, path) = queue.Dequeue();

            if (position == end)
            {
                return (steps, path);
            }

            if (!visited.Add(position))
            {
                continue;
            }

            foreach (var direction in directions)
            {
                var newPos = position + direction;
                var newSteps = steps + 1;

                if (grid[newPos] == '.')
                {
                    queue.Enqueue((newPos, newSteps, path.Add(newPos)), newSteps);
                }
            }
        }

        return (-1, []);
    }
}