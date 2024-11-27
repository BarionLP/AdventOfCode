namespace AdventOfCode.Util;

public static class InputHelper
{
    private static readonly string key = File.ReadAllText("SECRETS");
    public static async Task<string> GetInput(int year, int day)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", key);
        var response = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
        return await response.Content.ReadAsStringAsync();
    }
    public static async Task<IEnumerable<string>> GetInputAsLines(int year, int day) => (await GetInput(year, day)).Split("\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
}
