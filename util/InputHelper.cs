namespace AdventOfCode.Util;

public static class InputHelper
{
    private static readonly string key = File.ReadAllText("SECRETS");

    public static Task<string> GetInput(DateTime date)
    {
        if (date.Month is not 12) throw new InvalidOperationException($"{date} has no puzzle");
        return GetInput(date.Year, date.Day);
    }

    public static async Task<string> GetInput(int year, int day)
    {
        var fileName = $"{year}-{day}";
        if (File.Exists(fileName))
        {
            return File.ReadAllText(fileName);
        }

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", key);
        var response = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
        var input = await response.Content.ReadAsStringAsync();
        File.WriteAllText(fileName, input);
        return input;
    }

    public static IEnumerable<string> EnumerateLines(string input) => input.Split("\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
}
