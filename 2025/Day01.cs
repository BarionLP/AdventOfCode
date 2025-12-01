namespace AdventOfCode2025;

public static class Day01
{
    public static void Run(string input)
    {
        var dial = 50;
        var count = 0;

        foreach (var instruction in InputHelper.EnumerateLines(input))
        {
            var direction = instruction[0];
            var amount = int.Parse(instruction.AsSpan(1..));
            count += amount / 100;
            amount %= 100;
            var dialNew = direction switch
            {
                'R' => dial + amount,
                'L' => dial - amount,
                _ => throw new UnreachableException(),
            };

            if (dialNew > 99)
            {
                dialNew -= 100;
                if (dial is not 0) count++;
            }
            else if (dialNew < 0)
            {
                dialNew += 100;
                if (dial is not 0) count++;
            }
            else if (dialNew is 0 && dial is not 0)
            {
                count++;
            }
            dial = dialNew;
        }

        Console.WriteLine(count);
    }
}