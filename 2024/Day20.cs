namespace AdventOfCode2024;

public static class Day20
{
    public static void Run(string input)
    {
        var map = CharMap.CreateFromLines(input);
        var start = map.PositionOf('S');
        var end = map.PositionOf('E');

        map[start] = '.';
        map[end] = '.';

        var (normalTime, path) = FindFewestStepsPath(map, start, end);
        Console.WriteLine(normalTime);
        var counter = new Dictionary<int, int>();

        var distance = (char)(path.Length + '#' + 1);
        foreach (var block in path)
        {
            map[block] = distance;
            distance--;
        }

        foreach (var position in map.EnumeratePositions())
        {
            if (map[position] != '#')
            {
                continue;
            }

            var freeDirections = directions.Where(dir => map[position + dir] is not ('#' or '\0'));

            var max = 0;
            foreach (var from in freeDirections)
            {
                foreach (var to in freeDirections)
                {
                    var fromChar = map[position + from];
                    var toChar = map[position + to];
                    var timeSaved = fromChar - toChar - 2;
                    if (timeSaved > max)
                    {
                        max = timeSaved;
                    }
                }
            }

            if (counter.ContainsKey(max))
            {
                counter[max]++;
            }
            else
            {
                counter[max] = 1;
            }
        }
        counter.Remove(0);

        Console.WriteLine(counter.Where(p => p.Key >= 100).Sum(p => p.Value));
        // Console.WriteLine(string.Join('\n', counter.OrderBy(p => p.Key).Select(p => $"{p.Value} save {p.Key}ps")));
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
