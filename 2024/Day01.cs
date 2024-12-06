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

        var distance = left.Order().Zip(right.Order(), (a, b) => Math.Abs(a - b)).Sum();
        
        Console.WriteLine(distance);

        var similarity = right.Where(new HashSet<int>(left).Contains).Sum();
        Console.WriteLine(similarity);
    }
}
