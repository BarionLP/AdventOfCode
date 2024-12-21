using AdventOfCode2024;

var input = await InputHelper.GetInput(2024, 21);

// Day21.Run(input);
Day21.Run("""
029A
980A
179A
456A
379A
""");


public static class Day21
{
    public static void Run(string input)
    {
        var codes = InputHelper.EnumerateLines(input);

        var sum = 0;
        foreach (var code in codes)
        {
            var numericPart = int.Parse(code.AsSpan(0, code.Length - 1));
            var first = TypeNumber(code);
            var second = TypeArrows(first);
            var instructions = TypeArrows(second);
            var shortestSequence = instructions.Count();
            Console.WriteLine(string.Join("", instructions));
            Console.WriteLine(shortestSequence);

            sum = checked(sum + (numericPart * shortestSequence));
        }

        Console.WriteLine(sum);
    }

    private static IEnumerable<char> TypeNumber(IEnumerable<char> code)
    {
        var startPos = GetNumberCoordinates('A');
        foreach (var digit in code)
        {
            var targetPos = GetNumberCoordinates(digit);
            foreach (var move in Move(startPos, targetPos))
            {
                yield return move;
            }
            yield return 'A';
            startPos = targetPos;
        }
    }

    private static IEnumerable<char> TypeArrows(IEnumerable<char> code)
    {
        var startPos = GetArrowCoordinates('A');
        foreach (var digit in code)
        {
            var targetPos = GetArrowCoordinates(digit);
            foreach (var move in Move(startPos, targetPos))
            {
                yield return move;
            }
            yield return 'A';
            startPos = targetPos;
        }
    }

    private static IEnumerable<char> Move(Vector2 from, Vector2 to)
    {
        var diff = to - from;
        var xSymbol = diff.X > 0 ? '>' : '<';
        var ySymbol = diff.Y > 0 ? 'v' : '^';
        if (to.X == 0)
        {
            return Enumerable.Repeat(ySymbol, int.Abs(diff.Y)).Concat(Enumerable.Repeat(xSymbol, int.Abs(diff.X)));
        }

        return Enumerable.Repeat(xSymbol, int.Abs(diff.X)).Concat(Enumerable.Repeat(ySymbol, int.Abs(diff.Y)));
    }

    private static Vector2 GetNumberCoordinates(char number) => number switch
    {
        '7' => (0, 0),
        '8' => (1, 0),
        '9' => (2, 0),
        '4' => (0, 1),
        '5' => (1, 1),
        '6' => (2, 1),
        '1' => (0, 2),
        '2' => (1, 2),
        '3' => (2, 2),
        '0' => (1, 3),
        'A' => (2, 3),
        _ => throw new UnreachableException(),
    };

    private static Vector2 GetArrowCoordinates(char number) => number switch
    {
        '^' => (1, 0),
        'A' => (2, 0),
        '<' => (0, 1),
        'v' => (1, 1),
        '>' => (2, 1),
        _ => throw new UnreachableException(),
    };
}