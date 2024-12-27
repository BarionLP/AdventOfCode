namespace AdventOfCode2024;

public static class Day22
{
    public static void Run(string input)
    {
        var buyers = InputHelper.EnumerateLines(input).Select(long.Parse);

        var sum = 0L;

        foreach (var buyer in buyers)
        {
            var number = buyer;
            foreach (var i in Enumerable.Range(0, 2000))
            {
                number = (number ^ (number * 64)) % 16777216;
                number = (number ^ (number / 32)) % 16777216;
                number = (number ^ (number * 2048)) % 16777216;
            }
            //Console.WriteLine(number);
            sum += number;
        }
        Console.WriteLine(sum);
    }
}