namespace AdventOfCode2025;

public static class Day03
{
    public static void RunExample() => Run("""
        987654321111111
        811111111111119
        234234234234278
        818181911112111
        """);
    public static async Task Run() => Run(await InputHelper.GetInput(2025, 3));

    public static void Run(ReadOnlySpan<char> input)
    {
        var joltage = 0L;

        foreach (var range in input.Split('\n'))
        {
            var bank = input[range].Trim();

            if (bank.IsEmpty) continue;

            var indexMax = MaxIndex(bank[..^1]);
            var secondMax = MaxIndex(bank[(indexMax + 1)..]) + indexMax + 1;
            joltage += (bank[indexMax] - '0') * 10;
            joltage += bank[secondMax] - '0';
        }

        Console.WriteLine($"Part 1: {joltage}");

        joltage = 0;

        foreach (var range in input.Split('\n'))
        {
            var bank = input[range].Trim();

            if (bank.IsEmpty) continue;

            var count = 11;
            var ptr = 0;

            while (count >= 0)
            {
                var indexMax = MaxIndex(bank[ptr..^count]) + ptr;
                joltage += (bank[indexMax] - '0') * (long)Math.Pow(10, count);
                ptr = indexMax + 1;
                count--;
            }
        }

        Console.WriteLine($"Part 2: {joltage}");
    }

    private static int MaxIndex(ReadOnlySpan<char> chars)
    {
        var max = 0;
        for (var i = 1; i < chars.Length; i++)
        {
            if (chars[i] > chars[max])
            {
                max = i;
            }
        }

        return max;
    }
}
