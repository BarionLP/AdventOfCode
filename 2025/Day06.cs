namespace AdventOfCode2025;

public static class Day06
{
    public static void RunExample() => Run("""
        123 328  51 64 
         45 64  387 23 
          6 98  215 314
        *   +   *   +  
        """);
    public static async Task Run() => Run(await InputHelper.GetInput(2025, 6));

    public static void Run(string input)
    {
        RunPart1(input);
        RunPart2(input);
    }

    public static void RunPart1(ReadOnlySpan<char> input)
    {
        var problems = new List<List<long>>();
        var operators = new List<char>();
        foreach (var lineRange in input.Split('\n'))
        {
            var line = input[lineRange].Trim();

            var idx = 0;
            foreach (var itemRange in line.Split(' '))
            {
                var item = line[itemRange].Trim();
                if (item.IsEmpty) continue;

                if (item is "*" or "+")
                {
                    operators.Add(item[0]);
                    continue;
                }

                if (problems.Count > idx)
                {
                    problems[idx].Add(long.Parse(item));
                }
                else
                {
                    problems.Add([long.Parse(item)]);
                }

                idx++;
            }
        }

        var sum = 0L;

        Debug.Assert(operators.Count == problems.Count);

        foreach (var (op, numbers) in operators.Zip(problems))
        {
            Func<long, long, long> aggregate = op switch
            {
                '*' => static (agg, value) => agg * value,
                '+' => static (agg, value) => agg + value,
                _ => throw new UnreachableException(),
            };

            var aggregator = op switch
            {
                '*' => 1L,
                '+' => 0L,
                _ => throw new UnreachableException(),
            };

            foreach (var number in numbers)
            {
                aggregator = aggregate(aggregator, number);
            }

            sum += aggregator;
        }

        Console.WriteLine($"Part 1: {sum}");
    }

    public static void RunPart2(string input)
    {
        var sheet = CharMap.CreateFromLines(input);

        var sum = 0L;
        var numbers = new List<long>();
        foreach (var x in ..sheet.Width)
        {
            var isBlank = true;
            numbers.Add(0);
            foreach (var y in ..sheet.Height)
            {
                var c = sheet[sheet.Width - x - 1, y];
                if (char.IsWhiteSpace(c)) continue;
                isBlank = false;
                if (c is '*' or '+')
                {
                    sum += Calc(c, numbers);
                    numbers.Clear();
                    break;
                }

                numbers[^1] *= 10;
                numbers[^1] += c - '0';

            }

            if (isBlank)
            {
                numbers.RemoveAt(numbers.Count - 1);
            }
        }

        Console.WriteLine($"Part 2: {sum}");

        static long Calc(char op, IEnumerable<long> numbers)
        {
            Func<long, long, long> aggregate = op switch
            {
                '*' => static (agg, value) => agg * value,
                '+' => static (agg, value) => agg + value,
                _ => throw new UnreachableException(),
            };

            var aggregator = op switch
            {
                '*' => 1L,
                '+' => 0L,
                _ => throw new UnreachableException(),
            };

            foreach (var number in numbers)
            {
                aggregator = aggregate(aggregator, number);
            }
            return aggregator;
        }

    }
}