namespace AdventOfCode2024;

public static class Day09
{
    public static void Run(string input)
    {
        var drive = new Drive(input);

        //Part1(drive);
        var from = drive.Data.Length..drive.Data.Length;
        var last = int.MaxValue;

        while (true)
        {
            //Console.WriteLine(drive);
            if (NextData(from, drive, last) is not Range f)
            {
                break;
            }
            from = f;
            last = drive.Data[from.Start.Value];
            if (FindTarget(drive.Data.AsSpan()[..from.Start.Value], from.End.Value - from.Start.Value) is Range target)
            {
                drive.Move(from, target);
            }
        }

        Console.WriteLine(drive.Data.Select(i => (long)i).Index().Where(p => p.Item != -1).Sum(p => p.Index * p.Item));
        
        static Range? FindTarget(ReadOnlySpan<int> data, int count)
        {
            var start = 0;
            var end = 0;
            do
            {
                start = end;
                if (start >= data.Length)
                {
                    return null;
                }
                while (data[start] != -1)
                {
                    start++;
                    if (start >= data.Length)
                    {
                        return null;
                    }
                }
                end = start;
                while (end < data.Length && data[end] == -1)
                {
                    end++;
                }
            } while (end - start < count);

            return start..end;
        }

        static Range? NextData(Range range, Drive drive, int last)
        {
            var end = range.Start.Value - 1;
            while (drive.Data[end] == -1 || drive.Data[end] >= last)
            {
                end--;
                if (end < 0)
                {
                    return null;
                }
            }
            var data = drive.Data[end];
            var start = end;
            while (drive.Data[start] == data)
            {
                start--;
                if (start < 0)
                {
                    return null;
                }
            }
            return (start + 1)..(end + 1);
        }


        static void Part1(Drive drive)
        {
            var target = 0;
            var index = drive.Data.Length - 1;

            while (true)
            {
                MoveNextEmpty(ref target, drive);
                MoveNextData(ref index, drive);
                if (index <= target)
                {
                    break;
                }
                drive.Move(index, target);
            }

            static void MoveNextEmpty(ref int index, Drive drive)
            {
                while (drive.Data[index] != -1)
                {
                    index++;
                }
            }

            static void MoveNextData(ref int index, Drive drive)
            {
                while (drive.Data[index] == -1)
                {
                    index--;
                }
            }
        }
    }

    public sealed class Drive(string map)
    {
        public int[] Data { get; } = ConstructDrive(map);
        public void Move(Index from, Index to)
        {
            if (Data[to] != -1)
            {
                throw new InvalidOperationException();
            }

            if (Data[from] == -1)
            {
                throw new InvalidOperationException();
            }

            Data[to] = Data[from];
            Data[from] = -1;
        }

        public void Move(Range from, Range to)
        {
            var data = Data.AsSpan();
            data[from].CopyTo(data[to]);
            data[from].Fill(-1);
        }

        public override string ToString() => string.Join("", Data).Replace("-1", ".");

        private static int[] ConstructDrive(string map)
        {
            return Impl(map).ToArray();
            static IEnumerable<int> Impl(string map)
            {
                var index = 0;
                var empty = false;
                foreach (var c in map)
                {
                    if (c != '.' && !char.IsDigit(c))
                    {
                        continue;
                    }
                    var count = int.Parse(c.ToString());
                    if (empty)
                    {
                        for (var i = 0; i < count; i++)
                        {
                            yield return -1;
                        }
                        empty = false;
                    }
                    else
                    {
                        for (var i = 0; i < count; i++)
                        {
                            yield return index;
                        }
                        index++;
                        empty = true;
                    }
                }
            }
        }
    }
}