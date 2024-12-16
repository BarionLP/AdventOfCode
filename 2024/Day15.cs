namespace AdventOfCode2024;

public static class Day15
{
    public static void Run(string input)
    {
        var lines = InputHelper.EnumerateLines(input);
        var map = CharMap.CreateFromLines(lines.TakeWhile(l => l.StartsWith('#')));
        var instructions = string.Join("", lines.SkipWhile(l => l.StartsWith('#')));
        
        Part2(new CharMap(string.Join("", map.Select(c => c switch
        {
            '#' => "##",
            'O' => "[]",
            '.' => "..",
            '@' => "@.",
            _ => throw new UnreachableException(),
        })), map.Width * 2), instructions);
        
        //Part1(map, instructions);

        static void Part2(CharMap map, string instructions)
        {
            foreach (var instruction in instructions)
            {
                var robotPos = map.PositionOf('@');
                TryMove2(map, robotPos, GetVector(instruction));
            }

            Console.WriteLine(map.Index().Where(p => p.Item == '[').Select(p => map.IndexToPos(p.Index)).Select(p => p.Y * 100 + p.X).Sum());
        }

        static void Part1(CharMap map, string instructions)
        {
            foreach (var instruction in instructions)
            {
                var robotPos = map.PositionOf('@');
                TryMove(map, robotPos, GetVector(instruction));
            }

            Console.WriteLine(map.Index().Where(p => p.Item == 'O').Select(p => map.IndexToPos(p.Index)).Select(p => p.Y * 100 + p.X).Sum());
        }
    }

    private static bool TryMove(CharMap map, Vector2 pos, Vector2 dir)
    {
        if (map[pos] == '.')
        {
            return true;
        }

        if (map[pos] == '#')
        {
            return false;
        }

        if (TryMove(map, pos + dir, dir))
        {
            map[pos + dir] = map[pos];
            map[pos] = '.';
            return true;
        }

        return false;
    }

    private static bool CanMove(CharMap map, Vector2 pos, Vector2 dir)
    {
        if (map[pos] == '.')
        {
            return true;
        }

        if (map[pos] == '#')
        {
            return false;
        }

        if (map[pos] is '[' or ']')
        {
            var posOther = map[pos] is '[' ? pos + (1, 0) : pos - (1, 0);
            return CanMove(map, pos + dir, dir) && CanMove(map, posOther + dir, dir);
        }

        return CanMove(map, pos + dir, dir);
    }

    private static bool TryMove2(CharMap map, Vector2 pos, Vector2 dir)
    {
        if (dir.Y == 0)
        {
            return TryMove(map, pos, dir);
        }

        if (map[pos] == '.')
        {
            return true;
        }

        if (map[pos] == '#')
        {
            return false;
        }

        if (map[pos] is '[' or ']')
        {
            var posOther = map[pos] is '[' ? pos + (1, 0) : pos - (1, 0);
            if (!CanMove(map, pos + dir, dir) || !CanMove(map, posOther + dir, dir))
            {
                return false;
            }

            if (!TryMove2(map, posOther + dir, dir))
            {
                throw new UnreachableException();
            }
            if (!TryMove2(map, pos + dir, dir))
            {
                throw new UnreachableException();
            }

            map[posOther + dir] = map[posOther];
            map[pos + dir] = map[pos];
            map[posOther] = '.';
            map[pos] = '.';

            return true;
        }

        if (TryMove2(map, pos + dir, dir))
        {
            map[pos + dir] = map[pos];
            map[pos] = '.';
            return true;
        }

        return false;
    }

    private static Vector2 GetVector(char instruction) => instruction switch
    {
        '^' => (0, -1),
        '<' => (-1, 0),
        '>' => (1, 0),
        'v' => (0, 1),
        _ => throw new UnreachableException(),
    };
}