using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public static partial class Day03
{
    [GeneratedRegex(@"mul\(\d+,\d+\)")]
    public static partial Regex MulRegex { get; }

    [GeneratedRegex(@"(mul\(\d+,\d+\))|(do\(\))|(don't\(\))")]
    public static partial Regex MulRegex2 { get; }
    public static void Run(string input)
    {
        var sum = 0;
        var enabled = true;
        foreach (var match in MulRegex2.EnumerateMatches(input))
        {
            var instruction = input.AsSpan(match.Index, match.Length);
            if (instruction.SequenceEqual("don't()"))
            {
                enabled = false;
                continue;
            }

            if (instruction.SequenceEqual("do()"))
            {
                enabled = true;
                continue;
            }

            if (enabled)
            {
                var commaIndex = instruction.IndexOf(',');
                var left = instruction[4..commaIndex];
                var right = instruction[(commaIndex + 1)..^1];
                sum += int.Parse(left) * int.Parse(right);
            }
        }
        Console.WriteLine(sum);
    }
}