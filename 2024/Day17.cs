using System.Collections.Frozen;

namespace AdventOfCode2024;

public static class Day17p2
{
    private static readonly FrozenDictionary<int, Action<int>> InstructionMap = new Dictionary<int, Action<int>>
    {
        {0, adv},
        {1, bxl},
        {2, bst},
        {3, jnz},
        {4, bxc},
        {5, @out},
        {6, bdv},
        {7, cdv},
    }.ToFrozenDictionary();

    private static int A = 0;
    private static int B = 0;
    private static int C = 0;
    private static int InstructionPointer = 0;
    private static readonly List<int> Output = [];
    private static ImmutableArray<int> Instructions = [];

    public static void Run(string input)
    {
        var lines = InputHelper.EnumerateLines(input).ToArray();
        Output.Clear();
        A = 0;
        B = 0;
        C = 0;
        Instructions = lines[3].Split([',', ' '], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Skip(1).Select(int.Parse).ToImmutableArray();

        Output.Add(0);

        InstructionPointer = 2;
        while (InstructionPointer < Instructions.Length && InstructionPointer >= 0)
        {
            InstructionMap[Instructions[InstructionPointer]](Instructions[InstructionPointer + 1]);
            InstructionPointer -= 2;
        }

        Console.WriteLine(A);
    }

    private static void adv(int operand) => A <<= LiteralToCombo(operand);
    private static void bxl(int operand) => B ^= operand;
    private static void bst(int operand) => B = LiteralToCombo(operand) % 8;
    private static void jnz(int operand)
    {
        if (A != 0)
        {
            InstructionPointer = operand - 2; //jumps up 2 before next instruction
        }
    }

    private static void bxc(int operand) => B ^= C;
    private static void @out(int operand)
    {
        var result = Output[^1];
        if (operand > 4 && operand != result)
        {
            throw new UnreachableException();
        }
        SetComboFromLiteral(operand, result);
    }

    private static void bdv(int operand) => A = B << LiteralToCombo(operand);
    private static void cdv(int operand) => A = C << LiteralToCombo(operand);

    private static int LiteralToCombo(int operand) => operand switch
    {
        >= 0 and <= 3 => operand,
        4 => A,
        5 => B,
        6 => C,
        _ => throw new UnreachableException(),
    };

    private static int SetComboFromLiteral(int operand, int value) => operand switch
    {
        >= 0 and <= 3 => value,
        4 => A = value,
        5 => B = value,
        6 => C = value,
        _ => throw new UnreachableException(),
    };
}

public static class Day17p1
{
    private static readonly FrozenDictionary<int, Action<int>> InstructionMap = new Dictionary<int, Action<int>>
    {
        {0, adv},
        {1, bxl},
        {2, bst},
        {3, jnz},
        {4, bxc},
        {5, @out},
        {6, bdv},
        {7, cdv},
    }.ToFrozenDictionary();

    private static int A = 0;
    private static int B = 0;
    private static int C = 0;
    private static int InstructionPointer = 0;
    private static readonly List<int> Output = [];
    private static ImmutableArray<int> Instructions = [];

    public static void Run(string input)
    {
        var lines = InputHelper.EnumerateLines(input).ToArray();
        Output.Clear();
        A = int.Parse(lines[0].AsSpan(12));
        B = int.Parse(lines[1].AsSpan(12));
        C = int.Parse(lines[2].AsSpan(12));
        Instructions = lines[3].Split([',', ' '], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Skip(1).Select(int.Parse).ToImmutableArray();

        InstructionPointer = 0;
        while (InstructionPointer < Instructions.Length)
        {
            InstructionMap[Instructions[InstructionPointer]](Instructions[InstructionPointer + 1]);
            InstructionPointer += 2;
        }

        Console.WriteLine(string.Join(',', Output));
    }

    private static void adv(int operand) => A >>= LiteralToCombo(operand);
    private static void bxl(int operand) => B ^= operand;
    private static void bst(int operand) => B = LiteralToCombo(operand) % 8;
    private static void jnz(int operand)
    {
        if (A != 0)
        {
            InstructionPointer = operand - 2; //jumps up 2 before next instruction
        }
    }

    private static void bxc(int operand) => B ^= C;
    private static void @out(int operand) => Output.Add(LiteralToCombo(operand) % 8);
    private static void bdv(int operand) => B = A >> LiteralToCombo(operand);
    private static void cdv(int operand) => C = A >> LiteralToCombo(operand);

    private static int LiteralToCombo(int operand) => operand switch
    {
        >= 0 and <= 3 => operand,
        4 => A,
        5 => B,
        6 => C,
        _ => throw new UnreachableException(),
    };
}

// B = A % 8
// B = B ^ 2
// C = A >> B    //
// B = B ^ C     // 
// // A = A >> 3
// B = B ^ 7     // B = 5; 13; 21; 29; 37
// print (B % 8) // B = 2; 10; 18; 26; 34
// goto 0