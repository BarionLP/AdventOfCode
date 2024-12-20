using System.Runtime.InteropServices;

namespace AdventOfCode2024;

public static class Day19
{
    public static void Run(string input)
    {
        var lines = InputHelper.EnumerateLines(input);
        var towels = lines.First().Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var designs = lines.Skip(1);

        var sum = 0;
        var possible = 0;
        foreach (ReadOnlySpan<char> design in designs)
        {
            var solvedIndecies = new Dictionary<int, int>
            {
                { 0, 1 },
            };
            var todo = new HashSet<int>
            {
                0,
            };

            while (todo.Count > 0)
            {
                var idx = todo.Min();
                todo.Remove(idx);

                var span = design[idx..];
                foreach (var towel in towels)
                {
                    if (span.StartsWith(towel))
                    {
                        var target = idx + towel.Length;
                        ref var c = ref CollectionsMarshal.GetValueRefOrAddDefault(solvedIndecies, target, out _);
                        c += solvedIndecies[idx];

                        if (target < design.Length)
                        {
                            todo.Add(target);
                        }
                    }
                }
            }

            if (solvedIndecies.TryGetValue(design.Length, out var solutions))
            {
                sum += solutions;
                possible++;
            }
        }

        Console.WriteLine(sum);
        Console.WriteLine(possible);
    }
}