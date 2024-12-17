namespace AdventOfCode2024;

public static class Day16
{
    public static void Run(string input)
    {
        var map = CharMap.CreateFromLines(input);
        var start = map.PositionOf('S');
        var end = map.PositionOf('E');
        map[start] = '.';
        map[end] = '.';

        var paths = FindFewestTurnsPath(map, start, end).ToArray();
        var (turns, steps, _) = paths[0]; // first is always best (due to priority queue)
        
        var val = turns * 1000 + steps;
        var visitedFields = paths.Where(p => p.turns * 1000 + p.steps == val).SelectMany(p => p.path).ToHashSet();

        Console.WriteLine(val);
        Console.WriteLine(visitedFields.Count);
    }

    private static readonly Vector2[] directions = [(0, 1), (1, 0), (0, -1), (-1, 0)];
    public static IEnumerable<(int turns, int steps, ImmutableArray<Vector2> path)> FindFewestTurnsPath(CharMap grid, Vector2 start, Vector2 end)
    {

        var queue = new PriorityQueue<(Vector2 position, int direction, int turns, int steps, ImmutableArray<Vector2> path), (int turns, int steps)>();
        var visited = new Dictionary<(Vector2 position, int direction), (int turns, int steps)>();

        queue.Enqueue((start, Array.IndexOf(directions, (1, 0)), 0, 0, [start]), (0, 0));

        while (queue.Count > 0)
        {
            var (position, directionIndex, turns, steps, path) = queue.Dequeue();

            if (position == end)
            {
                yield return (turns, steps, path);
                continue;
            }

            if (visited.TryGetValue((position, directionIndex), out var best) && turns > best.turns)
                continue;

            visited[(position, directionIndex)] = (turns, steps);

            foreach (var direction in directions)
            {
                var newDirectionIndex = Array.IndexOf(directions, direction);
                int newTurns = turns + (newDirectionIndex == directionIndex ? 0 : 1);
                var newPos = position + direction;

                if (grid[newPos] == '.')
                {
                    queue.Enqueue((newPos, newDirectionIndex, newTurns, steps + 1, path.Add(newPos)), (newTurns, steps + 1));
                }
            }
        }
    }
}