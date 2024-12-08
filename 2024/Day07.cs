using System.Collections.Immutable;
using System.Diagnostics;

namespace AdventOfCode2024;

public static class Day07
{
    public static void Run(string input)
    {
        var total = 0L;
        foreach (var line in InputHelper.EnumerateLines(input))
        {
            var elements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var testValue = long.Parse(elements[0][..^1]);
            var others = elements.Skip(1).Select(long.Parse).ToImmutableArray();
            var operators = Enumerable.Repeat('+', others.Length - 1).ToArray();

            do
            {
                var result = others.Skip(1).Zip(operators).Aggregate(others[0], (current, p) => p.Second switch
                {
                    '+' => current + p.First,
                    '*' => current * p.First,
                    '|' => long.Parse($"{current}{p.First}"),
                    _ => throw new UnreachableException(),
                });

                if (result == testValue)
                {
                    total += testValue;
                    break;
                }
            } while (Next(operators));
        }
        Console.WriteLine(total);
    }

    private static bool Next(char[] operators, int i = 0)
    {
        if (operators[i] == '+')
        {
            operators[i] = '*';
            return true;
        }

        if (operators[i] == '*')
        {
            operators[i] = '|';
            return true;
        }

        operators[i] = '+';
        if (operators.Length == i + 1)
        {
            return false;
        }
        return Next(operators, i + 1);
    }
}