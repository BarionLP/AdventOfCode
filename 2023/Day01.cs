using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public static partial class Day01
{
    private static readonly FrozenDictionary<string, int> map = new Dictionary<string, int>() 
    {
        { "zero", 0 },
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    }.ToFrozenDictionary();

    public static void Run(IEnumerable<string> input)
    {
        var spanMap = map.GetAlternateLookup<ReadOnlySpan<char>>();

        var sum = 0;
        foreach (var line in input)
        {
            var first = Parse(RegexHelper.FirstMatch(Numbers, line));
            var last = Parse(RegexHelper.LastMatch(Numbers, line));
            sum += first * 10 + last;
        }

        Console.WriteLine(sum);

        int Parse(ReadOnlySpan<char> span)
        {
            return char.IsDigit(span[0]) ? int.Parse(span[0..1]) : spanMap[span];
        }
    }

    [GeneratedRegex(@"(zero|one|two|three|four|five|six|seven|eight|nine|\d)")]
    public static partial Regex Numbers { get; }
}

public static partial class RegexHelper
{

    public static ReadOnlySpan<char> LastMatch(Regex regex, string text)
    {
        var span = text.AsSpan();
        var index = span.Length - 1;
        while (true)
        {
            var subSpan = span[index..];
            var matches = regex.EnumerateMatches(subSpan);
            if (matches.MoveNext())
            {
                return subSpan.Slice(matches.Current.Index, matches.Current.Length);
            }
            index--;
            if (index < 0)
            {
                throw new InvalidOperationException();
            }
        }
    }

    public static ReadOnlySpan<char> FirstMatch(Regex regex, string text)
    {
        var matches = regex.EnumerateMatches(text);
        if (matches.MoveNext())
        {
            return text.AsSpan(matches.Current.Index, matches.Current.Length);
        }
        throw new InvalidOperationException();
    }
}
