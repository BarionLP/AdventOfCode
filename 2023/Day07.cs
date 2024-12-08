using System.Diagnostics;

namespace AdventOfCode2023;

public static class Day07
{
    public static void Run(string input)
    {
        var enumerated = EnumerateInput(input).OrderBy(x => x.Hand, new HandComparer2()).Index();
        Console.WriteLine(enumerated.Sum(x => x.Item.Bid * (x.Index + 1)));
        // Console.WriteLine(string.Join("\n", enumerated));
    }

    private static IEnumerable<(string Hand, int Bid)> EnumerateInput(string input)
    {
        foreach (var line in InputHelper.EnumerateLines(input))
        {
            var parts = line.Split(' ');
            yield return (parts[0], int.Parse(parts[1]));
        }
    }

    private sealed class HandComparer2 : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);

            var xType = GetType(x);
            var yType = GetType(y);
            var diff = xType - yType;

            if (diff != 0)
            {
                return diff;
            }

            return CompareCardWise(x, y);

            static int CompareCardWise(string handA, string handB, int index = 0)
            {
                var diff = CompareCards(handA[index], handB[index]);
                if (diff == 0 && index < handA.Length - 1)
                {
                    return CompareCardWise(handA, handB, index + 1);
                }
                return diff;

                static int CompareCards(char x, char y)
                    => GetCardRank(x) - GetCardRank(y);

                static int GetCardRank(char card) => card switch
                {
                    'A' => 14,
                    'K' => 13,
                    'Q' => 12,
                    'J' => 1,
                    'T' => 10,
                    _ => int.Parse(card.ToString()),
                };
            }
        }

        private static int GetType(string hand)
        {
            var jokers = hand.Count(c => c == 'J');
            if (jokers == 5)
            {
                return 7;
            }
            var counts = hand.Where(c => c != 'J').CountBy(c => c).OrderByDescending(p => p.Value).ToArray();
            return (counts[0].Value + jokers) switch
            {
                5 => 7,
                4 => 6,
                3 when counts[1].Value is 2 => 5,
                3 => 4,
                2 when counts[1].Value is 2 => 3,
                2 => 2,
                1 => 1,
                _ => throw new UnreachableException(),
            };
        }
    }

    private sealed class HandComparer1 : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);

            var xType = GetType(x);
            var yType = GetType(y);
            var diff = xType - yType;

            if (diff != 0)
            {
                return diff;
            }

            return CompareCardWise(x, y);

            static int CompareCardWise(string handA, string handB, int index = 0)
            {
                var diff = CompareCards(handA[index], handB[index]);
                if (diff == 0 && index < handA.Length)
                {
                    return CompareCardWise(handA, handB, index + 1);
                }
                return diff;

                static int CompareCards(char x, char y)
                    => GetCardRank(x) - GetCardRank(y);

                static int GetCardRank(char card) => card switch
                {
                    'A' => 14,
                    'K' => 13,
                    'Q' => 12,
                    'J' => 11,
                    'T' => 10,
                    _ => int.Parse(card.ToString()),
                };
            }
        }

        private static int GetType(string hand)
        {
            var counts = hand.CountBy(c => c).OrderByDescending(p => p.Value).ToArray();
            return counts[0].Value switch
            {
                5 => 7,
                4 => 6,
                3 when counts[1].Value is 2 => 5,
                3 => 4,
                2 when counts[1].Value is 2 => 3,
                2 => 2,
                1 => 1,
                _ => throw new UnreachableException(),
            };
        }
    }
}