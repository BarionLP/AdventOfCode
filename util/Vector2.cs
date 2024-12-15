namespace AdventOfCode.Util;

public readonly record struct Vector2(int X, int Y) : IComparable<Vector2>
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
