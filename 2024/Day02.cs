namespace AdventOfCode2024;

public static class Day02
{
    public static void Run(string reports)
    {
        Console.WriteLine(InputHelper.EnumerateLines(reports).Count(input =>
        {
            var report = new Report(input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse).ToArray());

            var errors = report.CountErrors();
            var isSafe = report.IsSafe();


            //Console.WriteLine($"{input} is {(isSafe ? "safe" : "danger")} ({errors})");
            return isSafe;
        }));
    }
}

public sealed class Report(int[] data)
{
    private readonly int[] data = data;
    public int this[int index] => data[index];

    public int Length => data.Length;


    public int CountErrors()
    {
        var errors = 0;
        var mode = GetMode();
        for (var i = 1; i < Length; i++)
        {
            if (!IsSafeIndex(i - 1, i, mode))
            {
                errors++;
            }
        }

        return errors;
    }

    public bool IsSafe()
    {
        var ignored = false;
        var mode = GetMode();
        if (mode == 0)
        {
            return false;
        }

        for (var i = 0; i < Length - 1; i++)
        {
            if (!IsSafeIndex(i, i + 1, mode))
            {
                if (ignored)
                {
                    return false;
                }

                if (i > 0)
                {
                    if (!IsSafeIndex(i - 1, i + 1, mode))
                    {
                        if (i >= Length - 2)
                        {
                            continue;
                        }
                        if (!IsSafeIndex(i, i + 2, mode))
                        {
                            return false;
                        }
                        i++;
                    }
                }
                else
                {
                    if (IsSafeIndex(i, i + 2, mode))
                    {
                        i++;
                    }
                }
                ignored = true;
            }
        }

        return true;
    }

    public int GetMode()
    {
        var pos = 0;
        var neg = 0;
        for (var i = 1; i < data.Length; i++)
        {
            var sign = Math.Sign(data[i - 1] - data[i]);
            if (sign == 1)
            {
                pos++;
            }
            else if (sign == -1)
            {
                neg++;
            }
        }

        return pos == neg ? 0 : pos > neg ? 1 : -1;
    }

    public bool IsSafeIndex(int left, int right, int mode) => IsSafe(data[left], data[right], mode);

    public static bool IsSafe(int left, int right, int mode)
    {
        var diff = left - right;
        return Math.Abs(diff) <= 3 && diff != 0 && Math.Sign(diff) == mode;
    }
}