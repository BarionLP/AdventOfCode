namespace AdventOfCode2025;

public static class Day05
{
    public static void RunExample() => Run("""
        3-5
        10-14
        16-20
        12-18

        1
        5
        8
        11
        17
        32
        """);
    public static async Task Run() => Run(await InputHelper.GetInput(2025, 5));

    public static void Run(ReadOnlySpan<char> input)
    {
        List<Range> freshRanges = [];
        var lineEnum = input.Split('\n');

        while (lineEnum.MoveNext())
        {
            var rangeSpan = input[lineEnum.Current].Trim();
            var separatorIndex = rangeSpan.IndexOf('-');
            if (separatorIndex is -1) break;
            var start = long.Parse(rangeSpan[..separatorIndex]);
            var end = long.Parse(rangeSpan[(separatorIndex + 1)..]);
            freshRanges.Add(new(start, end + 1));
        }

        var freshCount = 0;

        while (lineEnum.MoveNext())
        {
            var span = input[lineEnum.Current].Trim();
            if (span.IsEmpty) break;
            var id = long.Parse(span);

            foreach (var range in freshRanges)
            {
                if (range.Contains(id))
                {
                    freshCount++;
                    break;
                }
            }
        }

        Console.WriteLine($"Part 1: {freshCount}");


        List<Range> overlapFreeRanges = [];
        List<Range> toRemove = [];

        foreach (var range in freshRanges)
        {
            var newRange = range;
            foreach (var r in overlapFreeRanges)
            {
                if (newRange.Overlap(r))
                {
                    newRange = new Range(long.Min(newRange.Start, r.Start), long.Max(newRange.End, r.End));
                    toRemove.Add(r);
                }
            }

            foreach (var r in toRemove)
            {
                overlapFreeRanges.Remove(r);
            }
            toRemove.Clear();

            overlapFreeRanges.Add(newRange);
        }

        foreach (var a in overlapFreeRanges)
        {
            foreach (var b in overlapFreeRanges)
            {
                if (a == b) continue;
                Debug.Assert(!a.Overlap(b));
            }
        }


        var totalFresh = 0L;

        foreach (var freshRange in overlapFreeRanges)
        {
            totalFresh += freshRange.Count;
        }

        Console.WriteLine($"Part 2: {totalFresh}");
    }

    private readonly record struct Range(long Start, long End)
    {
        public long Count => End - Start;
        public bool Contains(long value) => value >= Start && value < End;
        public bool Overlap(Range other) => Contains(other.Start) || Contains(other.End) || other.Contains(Start) || other.Contains(End);
    }
}