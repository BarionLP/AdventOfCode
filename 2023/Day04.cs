using System.Collections.Frozen;

namespace AdventOfCode2023;

public static class Day04
{
    public static void Run(string input)
    {
        var cardCounter = new Dictionary<int, int>();
        var totalValue = 0;
        var totalCards = 0;
        foreach (var card in InputHelper.EnumerateLines(input))
        {
            var items = card.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1);
            var cardNumber = int.Parse(items.First()[..^1]);
            var cardsOwned = 1;
            if (cardCounter.TryGetValue(cardNumber, out var n))
            {
                cardsOwned += n;
            }

            var winningNumbers = items.Skip(1).TakeWhile(s => s != "|").ToFrozenSet();
            var winningCount = items.SkipWhile(s => s != "|").Skip(1).Count(winningNumbers.Contains);

            if (winningCount > 0)
            {
                totalValue += (int)Math.Pow(2, winningCount - 1);
                for (var i = 0; i < winningCount; i++)
                {
                    var other = cardNumber + i + 1;
                    var current = cardCounter.TryGetValue(other, out var o) ? o : 0;

                    cardCounter[other] = current + cardsOwned;
                }
            }

            totalCards += cardsOwned;
        }

        Console.WriteLine(totalValue);
        Console.WriteLine(totalCards);
    }
}