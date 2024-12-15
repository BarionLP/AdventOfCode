public static class Day14
{
    public static void Run(string input)
    {
        var fieldSize = new Vector2(101, 103);
        var center = fieldSize / 2;
        
        var machines = InputHelper.EnumerateLines(input).Select(ParseMachine);
        var futureMachines = machines.Select(machine => (machine.position + 100 * machine.velocity) % fieldSize).Select(pos => new Vector2(pos.X < 0 ? fieldSize.X + pos.X : pos.X, pos.Y < 0 ? fieldSize.Y + pos.Y : pos.Y)).ToArray();

        //Console.WriteLine(string.Join('\n', futureMachines));

        var q1 = futureMachines.Count(pos => pos.X < center.X && pos.Y < center.Y);
        var q2 = futureMachines.Count(pos => pos.X > center.X && pos.Y < center.Y);
        var q3 = futureMachines.Count(pos => pos.X < center.X && pos.Y > center.Y);
        var q4 = futureMachines.Count(pos => pos.X > center.X && pos.Y > center.Y);
        Console.WriteLine(q1 * q2 * q3 * q4);
    }

    private static (Vector2 position, Vector2 velocity) ParseMachine(string line)
    {
        var span = line.AsSpan();
        var spaceIndex = span.IndexOf(' ');
        return (ParseVector(span[2..spaceIndex]), ParseVector(span[(spaceIndex + 3)..]));
    }

    private static Vector2 ParseVector(ReadOnlySpan<char> span)
    {
        var commaIndex = span.IndexOf(',');
        return new Vector2(int.Parse(span[..commaIndex]), int.Parse(span[(commaIndex + 1)..]));
    }
}