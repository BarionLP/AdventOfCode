namespace AdventOfCode2023;

public static class Day03
{
    public static void Run(string input)
    {
        var lines = InputHelper.EnumerateLines(input);
        var scematic = new Scematic(string.Join("", lines), lines.First().Length);

        Console.WriteLine(scematic.EnumerateNumbersNextSymbols().Sum());
    }

    sealed class Scematic(string txt, int width)
    {
        private readonly string txt = txt;
        private readonly int width = width;
        private readonly int height = txt.Length / width;
        private char this[int x, int y] => txt[width * y + x];

        public IEnumerable<int> EnumerateNumbersNextSymbols()
        {
            var ptr = 0;
            while (ptr < txt.Length)
            {
                if (char.IsDigit(txt[ptr]))
                {
                    var start = ptr;

                    do
                    {
                        ptr++;
                    }
                    while (char.IsDigit(txt[ptr]));

                    if (IsNextSymbol(start..ptr))
                    {
                        yield return int.Parse(txt[start..ptr]);
                    }
                    continue;
                }

                ptr++;
            }
        }

        bool IsNextSymbol(Range range)
        {
            var start = range.Start.Value;
            var end = range.End.Value;
            var length = end - start;
            var (x, y) = GetPos(start);
            var result = IsSymbol(x - 1, y) || IsSymbol(x - 1, y - 1) || IsSymbol(x - 1, y + 1);
            for (int i = 0; i <= length; i++) // until one after last digit
            {
                result = result || IsSymbol(x + i, y + 1) || IsSymbol(x + i, y - 1);
            }
            result = result || IsSymbol(x + length, y);

            return result;
        }

        bool IsGear(int x, int y)
        {
            return this[x, y] is '*'
                && IsDigit(x + 1, y)
                && IsDigit(x, y + 1)
                && IsDigit(x - 1, y)
                && IsDigit(x, y - 1)
                && IsDigit(x + 1, y + 1)
                && IsDigit(x + 1, y - 1)
                && IsDigit(x - 1, y + 1)
                && IsDigit(x - 1, y - 1)
                ;
        }

        (int x, int y) GetPos(int flat) => (flat % width, flat / width);
        bool IsSymbol(int x, int y) => x > 0 && y > 0 && x < width && y < height && IsSymbol(this[x, y]);
        bool IsDigit(int x, int y) => x > 0 && y > 0 && x < width && y < height && char.IsDigit(this[x, y]);
        static bool IsSymbol(char c) => !char.IsDigit(c) && c != '.';
    }
}
