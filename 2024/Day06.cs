using System.Diagnostics;
using System.Text;

namespace AdventOfCode2024;

public static class Day06
{
    public static void Run(string input)
    {
        var lines = InputHelper.EnumerateLines(input);
        input = string.Join("", lines);
        
        var map = new Map(input, lines.First().Length);
        while (map.IsOnMap(map.GuardPos.X, map.GuardPos.Y))
        {
            map.MoveNext(true);
        }

        Console.WriteLine(map.CountVisitedPositions());

        map = new Map(input, lines.First().Length); //reset map
        var loopCount = 0;
        var known = new HashSet<((int, int), (int, int))>();
        var initalGuardPos = map.GuardPos;
        var initalFacing = map.GuardFacing;
        for (var i = 0; i < map.map.Length; i++)
        {
            if (map.map[i] == '#')
            {
                continue;
            }
            map.map[i] = '#';

            known.Clear();
            map.GuardPos = initalGuardPos;
            map.GuardFacing = initalFacing;

            while (map.IsOnMap(map.GuardPos.X, map.GuardPos.Y))
            {
                map.MoveNext();
                if (!known.Add((map.GuardPos, map.GuardFacing)))
                {
                    loopCount++;
                    break;
                }
            }

            map.map[i] = '.';
        }

        Console.WriteLine(loopCount);
    }

    private sealed class Map
    {
        internal readonly char[] map;

        public int Width { get; }
        public int Heigth { get; }

        public (int X, int Y) GuardPos;
        public (int X, int Y) GuardFacing;

        public char this[int x, int y]
        {
            get => IsOnMap(x, y) ? map[y * Width + x] : '.';
            set
            {
                if (IsOnMap(x, y))
                {
                    map[y * Width + x] = value;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private static readonly char[] GuardSymbols = ['^', 'v', '>', '<'];
        public Map(string map, int width)
        {
            this.map = map.ToCharArray();
            Width = width;
            Heigth = map.Length / width;
            var guardPos = map.IndexOfAny(GuardSymbols);
            GuardPos = (guardPos % Width, guardPos / Width);
            GuardFacing = map[guardPos] switch
            {
                '^' => (0, -1),
                'v' => (0, 1),
                '>' => (1, 0),
                '<' => (-1, 0),
                _ => throw new UnreachableException(),
            };
        }

        public void MoveNext(bool marks = false)
        {
            var (x, y) = GuardPos;
            while (IsOnMap(x, y) && this[x + GuardFacing.X, y + GuardFacing.Y] != '#')
            {
                if (marks)
                {
                    this[x, y] = 'X';
                }
                x += GuardFacing.X;
                y += GuardFacing.Y;
            }
            GuardPos = (x, y);
            RotateGuard();
        }

        public int CountVisitedPositions() => map.Count(c => c == 'X');
        public bool IsOnMap(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Heigth;

        public void RotateGuard()
        {
            GuardFacing = GuardFacing switch
            {
                (0, -1) => (1, 0),
                (1, 0) => (0, 1),
                (0, 1) => (-1, 0),
                (-1, 0) => (0, -1),
                _ => throw new UnreachableException(),
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < map.Length; i += Width)
            {
                sb.Append(map.AsSpan(i, Width));
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}