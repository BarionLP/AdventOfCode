namespace AdventOfCode2024;

public class Day01
{
    public static void Run(string input)
    {
        var left = new List<int>();
        var right = new List<int>();

        foreach (var line in InputHelper.EnumerateLines(input))
        {
            var tmp = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(tmp[0]));
            right.Add(int.Parse(tmp[1]));
        }

        left.Sort();
        right.Sort();

        var distance = 0;
        foreach (var (a, b) in left.Zip(right))
        {
            distance += Math.Abs(a - b);
        }
        
        Console.WriteLine(distance);

        var similarity = 0;
        foreach (var item in left)
        {
            similarity += item * right.Count(other => other == item);
        }
        Console.WriteLine(similarity);
    }
}
