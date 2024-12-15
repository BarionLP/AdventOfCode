namespace AdventOfCode2024;

public static class Day13
{
    public static void Run(string input)
    {
        var machines = GetMachines(input);
        var totalCost = 0L;
        foreach (var (a, b, prize) in machines)
        {
            var aCount = prize.X / a.X / 10;
            var bCount = prize.X / b.X / 10;

            var current = aCount * a + bCount * b;
            while (current.X < prize.X && current.Y < prize.Y)
            {

                if (DividesEvenly(prize - current, b) is long modb)
                {
                    bCount += modb;
                    break;
                }

                if (DividesEvenly(prize - current, a) is long moda)
                {
                    aCount += moda;
                    break;
                }

                aCount++;
                bCount++;
                current = aCount * a + bCount * b;
            }
            current = aCount * a + bCount * b;
            if (current == prize)
            {
                totalCost += aCount * 3 + bCount;
            }
        }

        Console.WriteLine(totalCost);


    }

    private static long? DividesEvenly(Vector2L a, Vector2L b)
    {
        var mod = a.X % b.X;
        var divisor = a.X / b.X;

        if (mod == 0 && a.Y % b.Y == 0 && divisor == a.Y / b.Y)
        {
            return divisor;
        }

        return null;
    }

    private static IEnumerable<(Vector2L A, Vector2L B, Vector2L Prize)> GetMachines(string input)
    {
        var lines = InputHelper.EnumerateLines(input).GetEnumerator();
        while (lines.MoveNext())
        {
            var a = Parse(lines.Current);
            lines.MoveNext();
            var b = Parse(lines.Current);
            lines.MoveNext();
            var p = Parse(lines.Current);
            yield return (a, b, p);
        }

        static Vector2L Parse(string input)
        {
            var line = input.AsSpan();
            var commaIndex = line.IndexOf(',');
            var colonIndex = line.IndexOf(':');
            return (long.Parse(line[(colonIndex + 4)..commaIndex]), long.Parse(line[(commaIndex + 4)..]));
        }
    }
}