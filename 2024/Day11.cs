namespace AdventOfCode2024;

public static class Day11
{
    public static void Run(string input)
    {
        var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(static s =>
            {
                // Console.WriteLine($"Stone {s}");
                return long.Parse(s);
            });

        //this works because at later iterations there are only a 'few' different stones
        var current = new Dictionary<long, long>(); // stone, count
        var next = new Dictionary<long, long>(); // stone, count
        foreach (var stone in stones)
        {
            Add(stone, 1);
        }

        //Add added them to next so we have to use next as current
        (current, next) = (next, current);

        for (var i = 0; i < 75; i++)
        {
            foreach (var (stone, count) in current)
            {
                var (f, r) = Do(stone);
                Add(f, count);
                Add(r, count);
            }
            next.Remove(-1); // probably faster than checking every time for -1
            current.Clear(); // clear for use as next in next iteration 
            (current, next) = (next, current);
        }

        Console.WriteLine(current.Sum(p => p.Value));
        // Console.WriteLine(stones.AsParallel().Sum(s => Next(s, 0))); //very nice zero alloc iterative solution


        void Add(long stone, long count)
        {
            if (next.ContainsKey(stone))
            {
                next[stone] += count;
            }
            else
            {
                next[stone] = count;
            }
        }


        (long a, long b) Do(long stone)
        {

            if (stone == 0)
            {
                return (1, -1);
            }

            var (split, first, second) = Split(stone);
            if (split)
            {
                return (first, second);
            }

            return (checked(stone * 2024), -1);
        }
    }

    public static int Next(long stone, int depth = 0)
    {
        if (depth >= 75)
        {
            return 1;
        }

        if (stone == 0)
        {
            return Next(1, depth + 1);
        }

        var (split, first, second) = Split(stone);
        if (split)
        {
            return Next(first, depth + 1) + Next(second, depth + 1);
        }

        return Next(checked(stone * 2024), depth + 1);
    }

    static (bool split, long a, long b) Split(long stone)
    {
        Span<char> buffer = stackalloc char[20];
        stone.TryFormat(buffer, out var length);
        if (length % 2 == 1)
        {
            return (false, 0, 0);
        }
        var middle = length / 2;
        var first = long.Parse(buffer[..middle]);
        var second = long.Parse(buffer[middle..]);
        return (true, first, second);
    }
}