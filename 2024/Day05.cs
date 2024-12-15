namespace AdventOfCode2024;

public static class Day05
{
    public static void Run(string input)
    {
        var lines = input.AsSpan().EnumerateLines();
        var rules = ParseRules(ref lines).ToImmutableArray();

        var middleSumCorrect = 0;
        var middleSumOther = 0;
        while (lines.MoveNext())
        {
            if (lines.Current.IsEmpty)
            {
                continue;
            }
            var update = ParseUpdate(lines.Current).ToImmutableArray();
            if (IsOrdered(update, rules))
            {
                middleSumCorrect += update[update.Length / 2];
                continue;
            }

            var ordered = Order(update, rules);
            middleSumOther += ordered[ordered.Count / 2];
        }

        Console.WriteLine(middleSumCorrect);
        Console.WriteLine(middleSumOther);
    }

    public static bool IsOrdered(ImmutableArray<int> update, ImmutableArray<(int before, int after)> rules)
    {
        for (var i = 0; i < update.Length; i++)
        {
            var afterSpan = update.AsSpan((i + 1)..);
            foreach (var before in rules.Where(p => p.after == update[i]).Select(p => p.before))
            {
                if (afterSpan.Contains(before))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static List<int> Order(ImmutableArray<int> update, ImmutableArray<(int before, int after)> rules)
    {
        var result = new List<int>();
        foreach (var page in update)
        {
            var before = rules.Where(p => p.before == page).Select(p => result.IndexOf(p.after)).Where(i => i >= 0).Append(result.Count).Min();
            result.Insert(before, page);
        }
        return result;
    }

    public static IEnumerable<int> ParseUpdate(ReadOnlySpan<char> line)
    {
        var splits = line.Split(',');
        var result = new List<int>();
        foreach (var split in splits)
        {
            result.Add(int.Parse(line[split]));
        }
        return result;
    }

    public static IEnumerable<(int before, int after)> ParseRules(ref SpanLineEnumerator lines)
    {
        var result = new List<(int before, int after)>();
        while (lines.MoveNext() && !lines.Current.IsEmpty)
        {
            result.Add(ParseRule(lines.Current));
        }

        return result;
    }

    private static (int before, int after) ParseRule(ReadOnlySpan<char> input)
    {
        var pipeIndex = input.IndexOf('|');
        return (int.Parse(input[..pipeIndex]), int.Parse(input[(pipeIndex + 1)..]));
    }
}