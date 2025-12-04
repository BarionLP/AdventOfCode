namespace AdventOfCode2025;

public static class Day04
{
    public static void RunExample() => Run("""
        ..@@.@@@@.
        @@@.@.@.@@
        @@@@@.@.@@
        @.@@@@..@.
        @@.@@@@.@@
        .@@@@@@@.@
        .@.@.@.@@@
        @.@@@.@@@@
        .@@@@@@@@.
        @.@.@@@.@.
        """);
    public static async Task Run() => Run(await InputHelper.GetInput(2025, 4));

    public static void Run(string input)
    {
        var map = CharMap.CreateFromLines(input);
        var movable = 0;

        foreach (var (x, y) in map.EnumeratePositions())
        {
            if (map[x, y] is not '@') continue;

            if (IsMovable(x, y)) movable++;
        }

        Console.WriteLine($"Part 1: {movable}");

        var total = 0;
        int removed;
        do
        {
            removed = Remove();
            total += removed;
        } while (removed > 0);

        Console.WriteLine($"Part 2: {total}");

        int Remove()
        {
            var count = 0;
            foreach (var (x, y) in map.EnumeratePositions())
            {
                if (map[x, y] is not '@') continue;

                if (IsMovable(x, y))
                {
                    map[x, y] = 'x';
                    count++;
                }
            }
            return count;
        }


        bool IsMovable(int x, int y)
        {
            Debug.Assert(map[x, y] is '@');

            var occupied = 0;

            if (map[x + 1, y] is '@') occupied++;
            if (map[x - 1, y] is '@') occupied++;
            if (map[x + 1, y + 1] is '@') occupied++;
            if (map[x + 1, y - 1] is '@') occupied++;
            if (map[x - 1, y + 1] is '@') occupied++;
            if (map[x - 1, y - 1] is '@') occupied++;
            if (map[x, y + 1] is '@') occupied++;
            if (map[x, y - 1] is '@') occupied++;

            return occupied < 4;
        }
    }
}