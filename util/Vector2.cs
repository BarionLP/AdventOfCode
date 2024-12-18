using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Util;

public readonly record struct Vector2(int X, int Y) : IComparable<Vector2>, ISpanParsable<Vector2>
{
    public static Vector2 Up { get; } = new(0, -1);
    public static Vector2 Down { get; } = new(0, 1);
    public static Vector2 Right { get; } = new(1, 0);
    public static Vector2 Left { get; } = new(-1, 0);
    public int MagnitudeSqr => X * X + Y * Y;

    public override string ToString() => $"({X}, {Y})";
    public int CompareTo(Vector2 other) => MagnitudeSqr.CompareTo(other.MagnitudeSqr);


    public static Vector2 operator +(Vector2 left, Vector2 right)
        => new(left.X + right.X, left.Y + right.Y);
    public static Vector2 operator -(Vector2 left, Vector2 right)
        => new(left.X - right.X, left.Y - right.Y);
    public static Vector2 operator *(Vector2 vector, int scalar)
        => new(vector.X * scalar, vector.Y * scalar);
    public static Vector2 operator *(int scalar, Vector2 vector)
        => new(vector.X * scalar, vector.Y * scalar);
    public static Vector2 operator /(Vector2 vector, int scalar)
        => new(vector.X / scalar, vector.Y / scalar);
    public static Vector2 operator /(int scalar, Vector2 vector)
        => new(vector.X / scalar, vector.Y / scalar);

    public static Vector2 operator %(Vector2 left, Vector2 right)
        => new(left.X % right.X, left.Y % right.Y);

    public static bool operator <(Vector2 left, Vector2 right) => left.CompareTo(right) < 0;
    public static bool operator >(Vector2 left, Vector2 right) => left.CompareTo(right) > 0;
    public static bool operator <=(Vector2 left, Vector2 right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Vector2 left, Vector2 right) => left.CompareTo(right) >= 0;

    public static implicit operator Vector2((int, int) tuple) => new(tuple.Item1, tuple.Item2);


    public static Vector2 Parse(string s) => Parse(s.AsSpan(), null);
    public static Vector2 Parse(string s, IFormatProvider? provider = null) => Parse(s.AsSpan(), provider);
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Vector2 result)
        => TryParse(s.AsSpan(), provider, out result);
    public static Vector2 Parse(ReadOnlySpan<char> s) => Parse(s, null);
    public static Vector2 Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var result))
        {
            return result;
        }

        throw new FormatException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Vector2 result)
    {
        result = (0, 0);
        if (s.StartsWith('('))
        {
            s = s[1..];
        }
        if (s.EndsWith(')'))
        {
            s = s[..^1];
        }
        var commaIndex = s.IndexOf(',');
        if (commaIndex < 0)
        {
            return false;
        }

        if (!int.TryParse(s[..commaIndex], out var x))
        {
            return false;
        }
        if (!int.TryParse(s[(commaIndex + 1)..], out var y))
        {
            return false;
        }

        result = (x, y);
        return true;
    }
}


public readonly record struct Vector2L(long X, long Y) : IComparable<Vector2L>
{
    public static Vector2 Up { get; } = new(0, -1);
    public static Vector2 Down { get; } = new(0, 1);
    public static Vector2 Right { get; } = new(1, 0);
    public static Vector2 Left { get; } = new(-1, 0);
    public long MagnitudeSqr => X * X + Y * Y;

    public override string ToString() => $"({X}, {Y})";
    public int CompareTo(Vector2L other) => MagnitudeSqr.CompareTo(other.MagnitudeSqr);

    public static Vector2L operator +(Vector2L left, Vector2L right)
        => new(left.X + right.X, left.Y + right.Y);
    public static Vector2L operator -(Vector2L left, Vector2L right)
        => new(left.X - right.X, left.Y - right.Y);
    public static Vector2L operator *(Vector2L vector, long scalar)
        => new(vector.X * scalar, vector.Y * scalar);
    public static Vector2L operator *(long scalar, Vector2L vector)
        => new(vector.X * scalar, vector.Y * scalar);

    public static Vector2L operator %(Vector2L left, Vector2L right)
        => new(left.X % right.X, left.Y % right.Y);

    public static bool operator <(Vector2L left, Vector2L right) => left.CompareTo(right) < 0;
    public static bool operator >(Vector2L left, Vector2L right) => left.CompareTo(right) > 0;
    public static bool operator <=(Vector2L left, Vector2L right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Vector2L left, Vector2L right) => left.CompareTo(right) >= 0;

    public static implicit operator Vector2L((long, long) tuple) => new(tuple.Item1, tuple.Item2);
}
