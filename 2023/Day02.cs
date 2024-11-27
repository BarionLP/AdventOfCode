namespace AdventOfCode2023;

public static class Day02
{
    public static void Run(IEnumerable<string> input)
    {
        int maxRed = 12, maxGreen = 13, maxBlue = 14;
        var sumIndecies = 0;
        var sumPowers = 0;

        foreach (var (index, game) in input.Index())
        {
            int red = 0, green = 0, blue = 0;
            foreach (var round in game.Split([';', ':'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Skip(1))
            {
                foreach (var group in round.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    var value = int.Parse(group.Split(' ')[0]);
                    if (group.Contains("red"))
                    {
                        if (value > red)
                        {
                            red = value;
                        }
                    }
                    else if (group.Contains("green"))
                    {
                        if (value > green)
                        {
                            green = value;
                        }
                    }
                    else if (group.Contains("blue"))
                    {
                        if (value > blue)
                        {
                            blue = value;
                        }
                    }
                }
            }

            sumPowers += red * green * blue;

            if (red <= maxRed && green <= maxGreen && blue <= maxBlue)
            {
                sumIndecies += index + 1; //index is zero based games are one based
            }
        }

        Console.WriteLine(sumIndecies);
        Console.WriteLine(sumPowers);
    }
}
