using Ametrin.Optional;

namespace AdventOfCode2025;

public static class Day02
{
    public static void RunExample() => Run("11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124");
    public static async Task Run() => Run(await InputHelper.GetInput(2025, 2));

    public static void Run(ReadOnlySpan<char> input)
    {
        var sum1 = 0ul;
        var sum2 = 0ul;
        Span<char> buffer = stackalloc char[ulong.MaxValue.ToString().Length];
        foreach (var range in input.Split(','))
        {
            var span = input[range];
            var separator = span.IndexOf('-');
            Debug.Assert(separator > 0);
            var start = ulong.Parse(span[..separator]);
            var end = ulong.Parse(span[(separator + 1)..]);


            for (var number = start; number <= end; number++)
            {
                var txt = number.TryFormat(buffer, out var charsWritten) ? buffer[..charsWritten] : throw new UnreachableException();
                if (IsFake1(txt))
                {
                    sum1 += number;
                }
                if (IsFake2(txt))
                {
                    sum2 += number;
                }
            }
        }

        Console.WriteLine($"Part 1: {sum1}");
        Console.WriteLine($"Part 2: {sum2}");
    }

    private static bool IsFake1(ReadOnlySpan<char> number)
    {
        if (number.Length % 2 is 1) return false;

        var center = number.Length / 2;
        return number[..center].SequenceEqual(number[center..]);
    }

    private static bool IsFake2(ReadOnlySpan<char> number)
    {
        var center = (number.Length / 2) + 1;

        foreach (var length in 1..center)
        {
            if (Impl(number, length))
            {
                return true;
            }
        }

        return false;

        static bool Impl(ReadOnlySpan<char> number, int length)
        {
            if (number.Length % length is not 0) return false;
            var sequence = number[..length];
            var start = length;
            var end = 2 * length;
            while (end <= number.Length)
            {
                if (!sequence.SequenceEqual(number[start..end]))
                {
                    return false;
                }
                start += length;
                end += length;
            }

            return true;
        }
    }
}